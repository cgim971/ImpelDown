import SocketSession from "../SocketSession";
import SessionManager from '../SessionManager';
import Room from "./Room";

interface RoomDictionary {
    [key: number]: Room;
}

export default class RoomManager {
    static Instance: RoomManager;

    private _roomMap: RoomDictionary = {};
    private _roomCount: number = 0;

    constructor() {
        this._roomMap = {};
        this._roomCount = 0;
    }

    createRoom(playerId: number, maxPeople: number) {
        let hostSocket: SocketSession = SessionManager.Instance.getSession(playerId);
        let room: Room = new Room(hostSocket, maxPeople);
        let index: number = 0;
        for (index = 0; index < this._roomCount + 1; index++) {
            if (this._roomMap[index] == null) {
                this._roomMap[index] = room;
                this._roomCount += 1;
                break;
            }
        }

        room.setRoomIndex(index);
        room.joinRoom(hostSocket);
    }

    joinRoom(playerId: number, roomIndex: number): void {
        let player: SocketSession = SessionManager.Instance.getSession(playerId);
        let room: Room = this._roomMap[roomIndex];

        room.joinRoom(player);
    }

    exitRoom(playerId: number, roomIndex: number): void {
        let player: SocketSession = SessionManager.Instance.getSession(playerId);
        let room: Room = this._roomMap[roomIndex];

        room.exitRoom(player);
    }

    deleteRoom(roomIndex: number): void {
        delete this._roomMap[roomIndex];
    }
}