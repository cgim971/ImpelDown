import SessionManager from "../../SessionManager";
import SocketSession from "../../PlayerData/SocketSession";
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

    createRoom(hostSocket: SocketSession) {
        if (hostSocket.getRoomData().getIsRoom() == true) {
            console.log("Error - Already In Room!");
            return;
        }

        let roomIndex: number = 0;
        while (this._roomMap[roomIndex] != null) {
            roomIndex++;
        }
        let room: Room = new Room(hostSocket, roomIndex);
        this._roomMap[roomIndex] = room;
    }

    deleteRoom(roomIndex: number): void {
        // 사람이 한 명도 없으면 방 삭제
        let room: Room = this._roomMap[roomIndex];
        if (room == null) {
            console.log("Error - No Room!");
            return;
        }

        delete this._roomMap[roomIndex];
    }

    // 방찾기
    matchMaking(player: SocketSession): void {
        for (let index in this._roomMap) {
            if (this._roomMap[index].getIsGaming() == true) continue;
            this._roomMap[index].joinRoom(player);
            return;
        }

        console.log("No Room");
        this.createRoom(player);
    }



    getRoom(roomIndex: number): Room | null {
        let room: Room = this._roomMap[roomIndex];
        if (room == null) {
            console.log("Error - No Room!");
            return null;
        }
        return room;
    }
}