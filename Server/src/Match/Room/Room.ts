import SocketSession from "../../PlayerData/SocketSession";
import SessionManager from "../../SessionManager";
import { impelDown } from "../../packet/packet";
import RoomManager from "./RoomManager";

interface PlayerDictionary {
    [key: number]: SocketSession;
}

export default class Room {
    private _hostSocket: SocketSession;

    private _playerMap: PlayerDictionary = {};
    private _playerCount: number;
    private _roomIndex: number;
    private _maxPeople: number;

    private _isGameing: boolean;

    constructor(hostSocket: SocketSession, roomIndex: number, maxPeople: number) {
        this._hostSocket = hostSocket;

        this._playerMap = [];
        this._playerCount = 0;
        this._roomIndex = roomIndex;
        this._maxPeople = maxPeople;

        this._isGameing = false;

        this.joinRoom();
    }

    startGame(): void {
        if (this._isGameing == true) return;
        this._isGameing = true;
    }

    diePlayer(player: SocketSession): void {
        for (let index in this._playerMap) {
            if (this._playerMap[index] == player) {
                delete this._playerMap[index];
                this._playerCount -= 1;
                return;
            }
        }
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

                let sExitRoom: impelDown.S_Exit_Room = new impelDown.S_Exit_Room({});
                player.SendData(sExitRoom.serialize(), impelDown.MSGID.S_EXIT_ROOM);
                break;
            }
        }

        if (this._playerCount == 0) {
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

    getRoomInfo(): impelDown.RoomInfo {
        return new impelDown.RoomInfo({
            roomIndex: this._roomIndex,
            hostPlayer: this._hostSocket.getPlayerInfo(),
            maxPeople: this._maxPeople,
            currentPeople: this._playerCount
        });
    }

    sRefreshRoom(): void {
        let sRefreshRoom: impelDown.S_Refresh_Room = new impelDown.S_Refresh_Room({ roomInfo: this.getRoomPlayersInfo() });
        this.broadCastMessage(sRefreshRoom.serialize(), impelDown.MSGID.S_REFRESH_ROOM);
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