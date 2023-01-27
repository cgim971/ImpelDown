import { SessionDictionary } from "../SessionManager";
import SocketSession from "../SocketSession";
import { impelDown } from "../packet/packet";
import RoomManager from "./RoomManager";

export default class Room {
    private _hostSocket: SocketSession;
    private _maxPeople: number;
    private _currentPeople: number = 0;
    private _roomIndex: number = 0;

    private _sessionMap: SessionDictionary = {};


    constructor(hostSocket: SocketSession, maxPeople: number) {
        this._hostSocket = hostSocket;
        this._maxPeople = maxPeople;
        this._currentPeople = 0;
        this._roomIndex = 0;

        this._sessionMap = {};
    }

    setRoomIndex(roomIndex: number): void {
        this._roomIndex = roomIndex;
    }

    joinRoom(player: SocketSession) {
        if (this._maxPeople <= this._currentPeople) {
            console.log("The room is full!");
            RoomManager.Instance.refreshRoomList();
            return;
        }

        for (let index: number = 0; index < this._maxPeople; index++) {
            if (this._sessionMap[index] == null) {
                this._sessionMap[index] = player;
                player.setRoom(true);
                player.setRoomIndex(this._roomIndex);
                this._currentPeople += 1;
                break;
            }
        }

        let roomInfo = new impelDown.RoomInfo({ playerId: this._hostSocket.getPlayerId(), roomIndex: this._roomIndex, maxPeople: this._maxPeople, currentPeople: this._currentPeople, playerInfos: this.getPlayerList() });
        let sRefreshRoom = new impelDown.S_Refresh_Room({ roomInfo: roomInfo });

        this.broadCastMessage(sRefreshRoom.serialize(), impelDown.MSGID.S_REFRESH_ROOM);
    }

    exitRoom(player: SocketSession) {
        player.setRoom(false);
        player.setRoomIndex(-1);

        if (this._currentPeople > 1) {
            if (player == this._hostSocket) {
                for (let index: number = 0; index < this._maxPeople; index++) {
                    if (this._sessionMap[index] != player) {
                        this._hostSocket = this._sessionMap[index];
                        break;
                    }
                }
            }

            for (let index: number = 0; index < this._maxPeople; index++) {
                if (this._sessionMap[index] == player) {
                    delete this._sessionMap[index];
                    this._currentPeople -= 1;
                    break;
                }
            }
        }
        else {
            // 방 삭제 - 방에 사람이 없을 때만 삭제
            RoomManager.Instance.deleteRoom(this._roomIndex);
        }
    }

    broadCastMessage(payload: Uint8Array, msgCode: number, senderId: number = 0, exceptSender: boolean = false): void {
        for (let index in this._sessionMap) {
            if (exceptSender == true && senderId == this._sessionMap[index].getPlayerId()) continue;

            this._sessionMap[index].SendData(payload, msgCode);
        }
    }

    getHostId(): number {
        return this._hostSocket.getPlayerId();
    }

    getMaxPeople(): number {
        return this._maxPeople;
    }

    getCurrentPeopleCount(): number {
        return this._currentPeople;
    }

    getPlayerList(): impelDown.PlayerInfo[] {
        let list: impelDown.PlayerInfo[] = [];
        for (let index in this._sessionMap) {
            if (this._sessionMap[index] != null) {
                list.push(new impelDown.PlayerInfo({ playerId: this._sessionMap[index].getPlayerId() }));
            }
        }

        return list;
    }
}