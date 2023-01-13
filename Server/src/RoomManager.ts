import Room from './Room';
import SessionManager from './SessionManager';
import SocketSession from './SocketSession';

interface RoomDictionary {
    [key: number]: Room;
}

export default class RoomManager {

    static Instance: RoomManager;
    private _roomMap: RoomDictionary = {};
    private _count: number = 0;

    constructor() {
        this._count = 0;
        this._roomMap = [];
    }

    createRoom(maximumPeople: number = 8, playerId: number): void {
        if (SessionManager.Instance.getSession(playerId).getPlayerRoomIndex() == -1) {
            for (let roomIndex: number = 0; roomIndex < this._count + 1; roomIndex++) {
                if (this._roomMap[roomIndex] == null) {
                    let room: Room = new Room(roomIndex, maximumPeople);
                    this._roomMap[roomIndex] = room;
                    console.log("Create Room [%d] - room maximumPeople [%d]", roomIndex, maximumPeople);
                    this._count += 1;

                    this.joinRoom(roomIndex, playerId);
                    break;
                }
            }
        }
        else {
            console.log("Error - Already create Room!");
        }
    }

    joinRoom(roomIndex: number, playerId: number): void {
        let session: SocketSession = SessionManager.Instance.getSession(playerId);
        if (session == null) {
            console.log("Error - Player missing!");
            return;
        }

        if (this._roomMap[roomIndex] == null) {
            console.log("Error - Room missing!");
            return;
        }
        this._roomMap[roomIndex].joinRoom(session);
    }

    exitRoom(playerId: number): void {
        let session: SocketSession = SessionManager.Instance.getSession(playerId);
        if (session == null) {
            console.log("Error - Player missing!");
            return;
        }
        let roomIndex: number = session.getPlayerRoomIndex();

        if (this._roomMap[roomIndex] == null) {
            console.log("Error - Room missing!");
            return;
        }
        this._roomMap[roomIndex].exitRoom(session);
    }

    deleteRoom(playerId: number): void {
        let roomIndex: number = SessionManager.Instance.getSession(playerId).getPlayerRoomIndex();
        let room: Room = this._roomMap[roomIndex];
        if (room == null) return;

        console.log("Starting Delete Room [%d]", roomIndex);
        room.deleteRoom();

        this._count -= 1;
        delete this._roomMap[roomIndex];
        console.log("Finish Delete Room [%d]", roomIndex);
    }

    debugRoom(roomIndex: number): void {
        this._roomMap[roomIndex].debugRoom();
    }
}
