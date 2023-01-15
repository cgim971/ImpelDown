import { impelDown } from "../packet/packet";
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
    createRoom(playerId: number, maximumPeople: number = 4): void {
        let room: Room = new Room(maximumPeople);
        let index: number = 0;
        this._count += 1;
        for (index = 0; index < this._count; index++) {
            if (this._roomMap[index] == null) {
                break;
            }
        }

        console.log("방 생성 - %d", index);

        this._roomMap[index] = room;
        this.joinRoom(index, playerId);

        let sCreateRoom: impelDown.S_Create_Room = new impelDown.S_Create_Room({ roomInfos: this.getRoomList(playerId) });
        SessionManager.Instance.broadCastMessage(sCreateRoom.serialize(), impelDown.MSGID.S_CREATE_ROOM, playerId, false);
    }

    // 방 입장
    joinRoom(roomId: number, playerId: number): void {
        this._roomMap[roomId].joinRoom(playerId);
        console.log("Join room [%d]", roomId);

        let roomInfo: impelDown.RoomInfo = this._roomMap[roomId].getRoomInfo(roomId, playerId);
        let sJoinRoom: impelDown.S_Join_Room = new impelDown.S_Join_Room({ roomInfo });
        SessionManager.Instance.broadCastMessage(sJoinRoom.serialize(), impelDown.MSGID.S_JOIN_ROOM, playerId, false);
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

    getRoomList(playerId: number): impelDown.RoomInfo[] {
        let list: impelDown.RoomInfo[] = [];

        for (let index in this._roomMap) {
            let room = this._roomMap[index];

            let roomInfo: impelDown.RoomInfo = this._roomMap[index].getRoomInfo(+index, playerId);
            list.push(roomInfo);
        }

        return list;
    }
}