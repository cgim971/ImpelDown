import JobTimer from "../../JobTimer";
import SocketSession from "../../PlayerData/SocketSession";
import SessionManager from "../../SessionManager";
import TailManager from "../TailManager";
import { impelDown } from "../../packet/packet";
import RoomManager from "./RoomManager";
import MapManager from "../MapManager";

interface PlayerDictionary {
    [key: number]: SocketSession;
}

export default class Room {
    private _hostSocket: SocketSession;

    private _playerMap: PlayerDictionary = {}; // 모든 플레이어
    private _playerCount: number; // 현재 플레이어 수
    private _catchedPlayerCount: number; // 잡힌 플레이어 수
    private _roomIndex: number; // 방 번호
    private _maxPeople: number; // 최대 인원
    private _mapIndex: number; // 맵의 번호

    private _isGameing: boolean; // 게임중인지

    private _tailManager: TailManager;
    private _mapManager: MapManager;

    constructor(hostSocket: SocketSession, roomIndex: number, maxPeople: number) {
        this._hostSocket = hostSocket;

        this._playerMap = [];

        this._playerCount = 0;
        this._catchedPlayerCount = 0;
        this._roomIndex = roomIndex;
        this._maxPeople = maxPeople;
        this._mapIndex = 0;

        this._isGameing = false;

        this._tailManager = new TailManager(this);
        this._mapManager = new MapManager(this);

        this.joinRoom();
    }

    gameStart(): void {
        if (this._isGameing == true) return;
        this._isGameing = true;

        this._tailManager.init();
        this._mapManager.init();

        // 플레이어들의 상태를 죽지 않은 1로 설정
        for (let index in this._playerMap) {
            if (this._playerMap[index] != null) {
                this._playerMap[index].getPlayerData().setPlayerState();
            }
        }

        let sGameStart: impelDown.S_Game_Start = new impelDown.S_Game_Start({ roomInfo: this.getRoomPlayersInfo() })
        this.broadCastMessage(sGameStart.serialize(), impelDown.MSGID.S_GAME_START);

        this.gameTimer.startTimer();
    }

    private gameTimer: JobTimer = new JobTimer(40, () => {
        this.sPlayerList();
    });

    catchPlayer(player: SocketSession, beCatchedPlayer: SocketSession) {
        let playerTargetTailIndex: number = player.getPlayerData().getTargetTailIndex();
        let beCatchedPlayerTailIndex: number = beCatchedPlayer.getPlayerData().getTailIndex();

        if (playerTargetTailIndex == beCatchedPlayerTailIndex) {
            console.log("Catch");

            // 꼬리 갱신
            this._tailManager.refreshTargetTail(player, beCatchedPlayer.getPlayerData().getTargetTailIndex());
            this.diePlayer(beCatchedPlayer);

            let sCatch: impelDown.S_Catch = new impelDown.S_Catch({ catchingPlayerInfo: player.getPlayerInfo(), playerInfos: this.getRoomPlayersInfo().playerInfos });
            this.broadCastMessage(sCatch.serialize(), impelDown.MSGID.S_CATCH);
        }
        else {
            console.log("False");
        }
    }


    checkGameResult(): void {
        // 1명마나 살앗따
        if (this._playerCount - 1 == this._catchedPlayerCount) {
            for (let index in this._playerMap) {
                let player: SocketSession = this._playerMap[index];
                if (player.getPlayerData().getPlayerState() == impelDown.PlayerState.ALIVE) {
                    this.gameTimer.stopTimer();

                    let sGameEnd: impelDown.S_Game_End = new impelDown.S_Game_End({ playerId: player.getPlayerData().getPlayerId() })
                    this.broadCastMessage(sGameEnd.serialize(), impelDown.MSGID.S_GAME_END);
                    break;
                }
            }
        }
    }

    diePlayer(player: SocketSession): void {
        player.getPlayerData().setPlayerState(impelDown.PlayerState.GHOST);
        this._catchedPlayerCount += 1;

        this.checkGameResult();
    }

    gameExit(player: SocketSession) {
        // let playerState: impelDown.PlayerState = player.getPlayerData().getPlayerState();
        // let p: SocketSession | null = null;
        // switch (playerState) {
        //     case impelDown.PlayerState.ALIVE:
        //         let pIndex: SocketSession | null = this._tailManager.getPlayer(player.getPlayerData().getTailIndex());

        //         if (pIndex == null) break;
        //         p = pIndex;
        //         this._tailManager.refreshTargetTail(p, player.getPlayerData().getTargetTailIndex() - 1);
        //         break;
        //     case impelDown.PlayerState.GHOST:
        //         this._catchedPlayerCount -= 1;
        //         break;
        // }

        // if (this._playerCount > 1) {
        //     // 플레이어가 2명 이상이면
        //     if (this._hostSocket == player) {
        //         // 호스트 변경
        //         for (let index in this._playerMap) {
        //             if (this._playerMap[index] != player) {
        //                 this._hostSocket = this._playerMap[index];
        //                 break;
        //             }
        //         }
        //     }
        // }

        // for (let index in this._playerMap) {
        //     if (this._playerMap[index] == player) {
        //         delete this._playerMap[index];
        //         this._playerCount -= 1;
        //         player.getRoomData().setRoomIndex();
        //         player.getPlayerData().setCharacterIndex();

        //         if (p == null) break;
        //         let sGameExit: impelDown.S_Game_Exit = new impelDown.S_Game_Exit({ playerId: player.getPlayerData().getPlayerId(), playerInfo: p.getPlayerInfo() });

        //         player.SendData(sGameExit.serialize(), impelDown.MSGID.S_GAME_EXIT);
        //         this.broadCastMessage(sGameExit.serialize(), impelDown.MSGID.S_GAME_EXIT);

        //         break;
        //     }
        // }

        // this.sPlayerList();

        // this.checkGameResult();
    }

    endGame(): void {

    }

    joinRoom(player: SocketSession = this._hostSocket): void {
        if (this._isGameing == true) return;
        if (!this.getIsEmpty()) return;

        let playerIndex: number = 0;
        while (this._playerMap[playerIndex] != null) {
            playerIndex++;
        }
        this._playerMap[playerIndex] = player;
        this._playerCount += 1;
        player.getRoomData().setRoomIndex(this._roomIndex);
        player.getPlayerData().setCharacterIndex(5);

        let sJoinRoom: impelDown.S_Join_Room = new impelDown.S_Join_Room();
        player.SendData(sJoinRoom.serialize(), impelDown.MSGID.S_JOIN_ROOM);

        this.sRefreshRoom();
        return;
    }


    // 게임 시작 전에 나가기
    exitRoom(player: SocketSession): void {
        if (this._playerCount > 1) {
            // 플레이어가 2명
            if (this._hostSocket == player) {
                // 호스트 변경
                for (let index in this._playerMap) {
                    if (this._playerMap[index] != player) {
                        this._hostSocket = this._playerMap[index];
                        break;
                    }
                }
            }
        }

        for (let index in this._playerMap) {
            if (this._playerMap[index] == player) {
                delete this._playerMap[index];
                this._playerCount -= 1;
                player.getRoomData().setRoomIndex();
                player.getPlayerData().setCharacterIndex();

                let sExitRoom: impelDown.S_Exit_Room = new impelDown.S_Exit_Room({});
                player.SendData(sExitRoom.serialize(), impelDown.MSGID.S_EXIT_ROOM);
                break;
            }
        }

        // 게임 중이 아니고 플레이어가 없으면 방 삭제
        if (this._playerCount == 0 && this._isGameing == false) {
            RoomManager.Instance.deleteRoom(this._roomIndex);
        }

        this.sRefreshRoom();
        return;
    }

    getIsGaming(): boolean {
        return this._isGameing;
    }

    getIsEmpty(): boolean {
        return this._maxPeople > this._playerCount;
    }

    getRoomIndex(): number {
        return this._roomIndex;
    }

    getHostSocket(): SocketSession {
        return this._hostSocket;
    }

    setMapIndex(mapIndex: number): void {
        this._mapIndex = mapIndex;
    }
    getMapIndex(): number {
        return this._mapIndex;
    }


    getPlayerMap(): PlayerDictionary {
        return this._playerMap;
    }

    getPlayerCount(): number {
        return this._playerCount;
    }

    getRoomInfo(): impelDown.RoomInfo {
        return new impelDown.RoomInfo({
            roomIndex: this._roomIndex,
            hostPlayer: this._hostSocket.getPlayerInfo(),
            maxPeople: this._maxPeople,
            currentPeople: this._playerCount,
            mapIndex: this._mapIndex
        });
    }

    sRefreshRoom(): void {
        let sRefreshRoom: impelDown.S_Refresh_Room = new impelDown.S_Refresh_Room({ roomInfo: this.getRoomPlayersInfo() });
        this.broadCastMessage(sRefreshRoom.serialize(), impelDown.MSGID.S_REFRESH_ROOM);
    }

    sPlayerList(): void {
        let list: impelDown.PlayerInfo[] = [];
        for (let index in this._playerMap) {
            list.push(this._playerMap[index].getPlayerInfo());
        }
        let data: impelDown.S_PlayerList = new impelDown.S_PlayerList({ playerInfos: list });
        this.broadCastMessage(data.serialize(), impelDown.MSGID.S_PLAYERLIST);
    }

    getRoomPlayersInfo(): impelDown.RoomInfo {
        let roomInfo: impelDown.RoomInfo;
        roomInfo = this.getRoomInfo();

        let list: impelDown.PlayerInfo[] = [];
        for (let index in this._playerMap) {
            list.push(this._playerMap[index].getPlayerInfo());
        }
        roomInfo.playerInfos = list;
        return roomInfo;
    }

    broadCastMessage(payload: Uint8Array, msgCode: number): void {
        for (let index in this._playerMap) {
            this._playerMap[index].SendData(payload, msgCode);
        }
    }
}