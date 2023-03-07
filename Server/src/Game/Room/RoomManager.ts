import PlayerSocket from "../../Player/PlayerSocket";
import { impelDown } from "../../packet/packet";
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


    public createRoom(hostSocket: PlayerSocket, maxPeople: number): void {
        if (hostSocket.getRoomDataInfo().getRoomState() != impelDown.RoomState.ROOM_NONE) {
            return;
        }

        let roomIndex: number = 0;
        while (this._roomMap[roomIndex] != null) {
            roomIndex++;
        }

        let room: Room = new Room(hostSocket, roomIndex, maxPeople);
        this._roomMap[roomIndex] = room;
    }

    public joinRoom(player: PlayerSocket, roomIndex: number) {
        let room: Room = this._roomMap[roomIndex];
        room.joinRoom(player);
    }



}