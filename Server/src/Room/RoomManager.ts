import SessionManager from "../SessionManager";
import SocketSession from "../SocketSession";
import Room from "./Room";

interface RoomDictionary {
    [key: number]: Room;
}

export default class RoomManager {

    static Instance: RoomManager;
    private _roomMap: RoomDictionary = {};
    private _count: number = 0;

    constructor() {
        this._roomMap = {};
        this._count = 0;
    }

    broadCastMessage(payload: Uint8Array, msgCode: number, roomId: number = 0): void {
        let room: Room = this._roomMap[roomId];
        if (room != null) {
            this._roomMap[roomId].broadCastMessage(payload, msgCode);
            return;
        }
        else {
            console.log("No Room!");
            return;
        }
    }

    // 방 생성 생성한 사람은 방 입장도 함
    createRoom(maximumPeople: number = 4, playerId: number): void {
        let room: Room = new Room(maximumPeople);
        let index: number = 0;
        this._count += 1;
        for (index = 0; index < this._count; index++) {
            if (this._roomMap[index] == null) {
                break;
            }
        }
        this._roomMap[index] = room;
        this.joinRoom(index, playerId);
        return;
    }

    // 방 입장
    joinRoom(roomIndex: number, playerId: number): void {
        this._roomMap[roomIndex].joinRoom(playerId);
        console.log("Join room [%d]", roomIndex);
    }

    // 방 나가기
    exitRoom(playerId: number): void {
        let roomIndex: number = SessionManager.Instance.getSession(playerId).getPlayerRoomIndex();
        this._roomMap[roomIndex].exitRoom(playerId);
    }

    // 방 삭제
    deleteRoom(roomIndex: number): void {
        delete this._roomMap[roomIndex];
    }

    // 방 번호 
    getRoomIndex(room: Room): number {
        let count: number = this._count;
        let index: number = 0;
        while (count > 0) {
            if (this._roomMap[index] != null) {
                count -= 1;

                if (this._roomMap[index] == room) {
                    return index;
                }
            }
            index++;
        }

        return -1;
    }
}