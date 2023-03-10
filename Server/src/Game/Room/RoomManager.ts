import PlayerSocket from "../../Player/PlayerSocket";
import { impelDown } from "../../packet/packet";
import SessionManager, { SessionDictionary } from "../Managers/SessionManager";
import Room from "./Room";

interface RoomDictionary {
    [key: number]: Room;
}

export default class RoomManager {

    static Instance: RoomManager;
    private _roomMap: RoomDictionary;

    constructor() {
        this._roomMap = [];
    }

    getRoom(index: number): Room {
        return this._roomMap[index];
    }


    public createRoom(hostSocket: PlayerSocket): void {
        if (hostSocket.getPlayerDataInfo().getRoomIndex() != -1) return;

        let roomIndex: number = 0;
        while (this._roomMap[roomIndex] != null) {
            roomIndex++;
        }

        let room: Room = new Room(hostSocket, roomIndex);
        this._roomMap[roomIndex] = room;
    }

    public joinRoom(player: PlayerSocket, roomIndex: number) {
        let room: Room = this._roomMap[roomIndex];
        room.joinRoom(player);
    }

    public exitRoom(player: PlayerSocket, roomIndex: number) {
        let room : Room  = this._roomMap[roomIndex];
        room.exitRoom(player);
    }

    public matchMaking(player: PlayerSocket): void {
        for (let index in this._roomMap) {
            if (this._roomMap[index] != null) {
                if (this._roomMap[index].isEmpty() == true) {
                    this._roomMap[index].joinRoom(player);
                    return;
                }
            }
        }

        this.createRoom(player);
    }

    getRoomInfos(): impelDown.RoomInfo[] {
        let list: impelDown.RoomInfo[] = [];

        for (let index in this._roomMap) {
            if (this._roomMap[index] != null) {
                list.push(this._roomMap[index].getRoomInfo());
            }
        }

        return list;
    }

    broadCastMessage(payload: Uint8Array, msgCode: number): void {
        let sessionMap: SessionDictionary = SessionManager.Instance.getSessionMap();

        for (let index in sessionMap) {
            if (sessionMap[index].getPlayerDataInfo().getRoomIndex() == -1)
                sessionMap[index].SendData(payload, msgCode);
        }
    }

}