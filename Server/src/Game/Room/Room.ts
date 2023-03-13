import JobTimer from "../../JobTimer";
import PlayerSocket from "../../Player/PlayerSocket";
import { impelDown } from "../../packet/packet";
import MapManager from "../Managers/MapManager";
import SessionManager from "../Managers/SessionManager";
import TailManager from "../Managers/TailManager";
import RoomManager from "./RoomManager";


enum Result {
    NONE = 0,
    SUCCESS = 1,
    FAIL = 2,
    CLOSE = 3,
}

export default class Room {
    private _hostSocket: PlayerSocket;
    private _roomInfo: impelDown.RoomInfo;

    private _tailManager: TailManager = new TailManager();
    private _mapManager: MapManager = new MapManager();


    constructor(hostSocket: PlayerSocket, roomIndex: number) {
        this._hostSocket = hostSocket;

        this._roomInfo = new impelDown.RoomInfo({
            roomState: impelDown.RoomState.LOBBY,
            hostId: hostSocket.getPlayerId(),
            hostName: hostSocket.getPlayerName(),
            roomIndex: roomIndex,
            mapIndex: 0,
            currentPeople: 0,
            maxPeople: 8,
        });

        for (let index: number = 0; index < 8; index++) {
            this._roomInfo.roomDatas[index] = new impelDown.RoomData({ isLock: false, playerId: -1, playerName: "", isReady: false });
        }
        this.joinRoom(hostSocket);
    }


    startGame(): void {
        if (this._roomInfo.roomState == impelDown.RoomState.GAME) {
            return;
        }

        for (let index in this._roomInfo.roomDatas) {
            if (this._roomInfo.roomDatas[index].playerId == this._roomInfo.hostId)
                continue;

            if (this._roomInfo.roomDatas[index].playerId != -1) {
                if (this._roomInfo.roomDatas[index].isReady == false) {
                    return;
                }
            }
        }

        this._roomInfo.roomState = impelDown.RoomState.GAME;

        let sStart: impelDown.S_Start = new impelDown.S_Start({
            mapIndex: this._roomInfo.mapIndex,
            playerDatas: this.playerInitData()
        });
        this.broadCastMessage(sStart.serialize(), impelDown.MSGID.S_START);

        // 게임 시작 시 실행
        this.gameTimer.startTimer();
    }

    private gameTimer: JobTimer = new JobTimer(40, () => {
        // 게임 중에 계속 실행
        let sPlayerList: impelDown.S_PlayerList = new impelDown.S_PlayerList({ playerInGameDatas: this.playerGameDatas() });
        this.broadCastMessage(sPlayerList.serialize(), impelDown.MSGID.S_PLAYERLIST);
    });

    private playerInitData(): impelDown.PlayerInitData[] {
        let list: impelDown.PlayerInitData[] = [];

        for (let index in this._roomInfo.roomDatas) {
            if (this._roomInfo.roomDatas[index].playerId == -1) continue;

            let player: PlayerSocket = SessionManager.Instance.getSession(this._roomInfo.roomDatas[index].playerId);
            let data: impelDown.PlayerInitData = new impelDown.PlayerInitData({
                playerId: player.getPlayerId(),
                playerName: player.getPlayerName(),
                characterIndex: player.getPlayerDataInfo().getPlayerCharacterIndex(),
                playerPosData: new impelDown.PlayerPosData({ position: new impelDown.Position({ x: 0, y: 0 }), scaleX: 1 }),
                tailIndex: player.getPlayerDataInfo().getTailIndex(),
                playerState: player.getPlayerDataInfo().getPlayerState()
            });

            list.push(data);
        }

        return list;
    }

    private playerGameDatas(): impelDown.PlayerInGameData[] {
        let list: impelDown.PlayerInGameData[] = [];
        for (let index in this._roomInfo.roomDatas) {
            if (this._roomInfo.roomDatas[index].playerId == -1) continue;
            
            let player : PlayerSocket = SessionManager.Instance.getSession(this._roomInfo.roomDatas[index].playerId);
            let data: impelDown.PlayerInGameData = new impelDown.PlayerInGameData({
               playerId:player.getPlayerId(),
               playerNmae:player.getPlayerName(),
               playerPosData:player.getPlayerDataInfo().getPlayerPosition(),
               tailIndex:player.getPlayerDataInfo().getTailIndex(),
               playerState:player.getPlayerDataInfo().getPlayerState(), 
            });
            list.push(data);
        }

        return list;

    }

    joinRoom(player: PlayerSocket): void {
        if (this._roomInfo.roomState == impelDown.RoomState.GAME) return;
        if (this.isEmpty() == false) return;

        let playerIndex: number = 0;
        while (playerIndex < 8) {
            let result: Result = this.setPlayer(playerIndex, player);
            if (result == Result.SUCCESS)
                break;
            else if (result == Result.FAIL) {
                playerIndex++;
            }
            else if (result == Result.CLOSE) {
                return;
            }
        }

        let sJoinRoom: impelDown.S_JoinRoom = new impelDown.S_JoinRoom({});
        player.SendData(sJoinRoom.serialize(), impelDown.MSGID.S_JOIN_ROOM);

        this.sRefreshRoom();
    }


    exitRoom(player: PlayerSocket): void {
        let playerId = player.getPlayerId();

        if (this._roomInfo.currentPeople > 1) {
            // 호스트 변경
            if (this._hostSocket == player) {
                for (let index in this._roomInfo.roomDatas) {
                    let roomPlayer: impelDown.RoomData = this._roomInfo.roomDatas[index];
                    if (roomPlayer.playerId != -1 && playerId != roomPlayer.playerId) {
                        this._hostSocket = SessionManager.Instance.getSession(roomPlayer.playerId);
                        this._roomInfo.hostId = roomPlayer.playerId;
                        this._roomInfo.hostName = roomPlayer.playerName;
                        this._roomInfo.roomDatas[index].isReady = false;
                        break;
                    }
                }
            }
        }

        for (let index in this._roomInfo.roomDatas) {
            if (this._roomInfo.roomDatas[index].playerId == playerId) {
                this._roomInfo.roomDatas[index] = new impelDown.RoomData({ isLock: false, playerId: -1, playerName: "", isReady: false });
                this._roomInfo.currentPeople -= 1;
                player.getPlayerDataInfo().setRoomIndex();

                let sExitRoom: impelDown.S_ExitRoom = new impelDown.S_ExitRoom({});
                player.SendData(sExitRoom.serialize(), impelDown.MSGID.S_EXIT_ROOM);
                break;
            }
        }

        if (this._roomInfo.currentPeople == 0) {
            // 방삭제
            RoomManager.Instance.deleteRoom(this._roomInfo.roomIndex);
        }
        this.sRefreshRoom();
        return;
    }


    setReady(playerId: number, isReady: boolean) {
        for (let index in this._roomInfo.roomDatas) {
            if (this._roomInfo.roomDatas[index].playerId == playerId) {
                this._roomInfo.roomDatas[index].isReady = isReady;
                break;
            }
        }

        let sIsReady: impelDown.S_IsReady = new impelDown.S_IsReady({
            roomInfo: this.getRoomInfo()
        });
        this.broadCastMessage(sIsReady.serialize(), impelDown.MSGID.S_ISREADY);
    }

    sRefreshRoom(): void {
        let sRefreshRoom: impelDown.S_RefreshRoom = new impelDown.S_RefreshRoom({
            roomInfo: this.getRoomInfo()
        });
        this.broadCastMessage(sRefreshRoom.serialize(), impelDown.MSGID.S_REFRESH_ROOM);
    }

    setLock(index: number, isLock: boolean = false): void {
        if (this._roomInfo.roomDatas[index].playerId != -1) {
            return;
        }

        this._roomInfo.roomDatas[index].isLock = isLock;
        let sIsLock: impelDown.S_IsLock = new impelDown.S_IsLock({
            roomInfo: this.getRoomInfo()
        });
        this.broadCastMessage(sIsLock.serialize(), impelDown.MSGID.S_ISLOCK);
    }

    setPlayer(index: number, player: PlayerSocket): Result {
        if (index >= 8) return Result.CLOSE;

        if (this._roomInfo.roomDatas[index].isLock == true)
            return Result.FAIL;

        if (this._roomInfo.roomDatas[index].playerId != -1)
            return Result.FAIL;

        this._roomInfo.roomDatas[index].playerId = player.getPlayerId();
        this._roomInfo.roomDatas[index].playerName = player.getPlayerName();
        this._roomInfo.currentPeople += 1;
        player.getPlayerDataInfo().setRoomInIndex(index);
        player.getPlayerDataInfo().setRoomIndex(this._roomInfo.roomIndex);
        return Result.SUCCESS;
    }

    getRoomInfo(): impelDown.RoomInfo {
        return this._roomInfo;
    }

    isEmpty(): boolean {
        for (let index: number = 0; index < 8; index++) {
            if (this._roomInfo.roomDatas[index].isLock == false) {
                if (this._roomInfo.roomDatas[index].playerId == -1) {
                    return true;
                }
            }
        }

        return false;
    }

    broadCastMessage(payload: Uint8Array, msgCode: number): void {
        for (let index in this._roomInfo.roomDatas) {
            if (this._roomInfo.roomDatas[index].playerId != -1) {
                let player: PlayerSocket = SessionManager.Instance.getSession(this._roomInfo.roomDatas[index].playerId);
                player.SendData(payload, msgCode);
            }
        }
    }
}