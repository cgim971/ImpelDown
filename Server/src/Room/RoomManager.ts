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
        let plaeyrSocket: SocketSession = SessionManager.Instance.getSession(playerId);
        if (plaeyrSocket.getPlayerRoomIndex() != -1) {
            console.log("Aleready Join room!");
            return;
        }

        let roomId: number = 0;
        this._count += 1;
        for (roomId = 0; roomId < this._count; roomId++) {
            if (this._roomMap[roomId] == null) {
                break;
            }
        }
        let room: Room = new Room(roomId, maximumPeople, playerId);
        console.log("방 생성 - %d", roomId);

        this._roomMap[roomId] = room;
        this.joinRoom(roomId, playerId);

        this.sendRoomList();

        // let roomInfo: impelDown.RoomInfo = this._roomMap[roomId].getRoomInfo();
        // let sCreateRoom: impelDown.S_Create_Room = new impelDown.S_Create_Room({ roomInfo: roomInfo });
        // SessionManager.Instance.broadCastMessage(sCreateRoom.serialize(), impelDown.MSGID.S_CREATE_ROOM, playerId, false);
    }

    // 방 입장
    joinRoom(roomId: number, playerId: number): void {
        this._roomMap[roomId].joinRoom(playerId);
        console.log("Join room [%d]", roomId);

        // let roomInfo: impelDown.RoomInfo = this._roomMap[roomId].getRoomInfo();
        // let sJoinRoom: impelDown.S_Join_Room = new impelDown.S_Join_Room({ roomInfo });
        // SessionManager.Instance.broadCastMessage(sJoinRoom.serialize(), impelDown.MSGID.S_JOIN_ROOM, playerId, false);
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


    // getRoomList(playerId: number): impelDown.RoomInfo[] {
    //     let list: impelDown.RoomInfo[] = [];

    //     for (let index in this._roomMap) {
    //         let room = this._roomMap[index];

    //         let roomInfo: impelDown.RoomInfo = this._roomMap[index].getRoomInfo(playerId);
    //         list.push(roomInfo);
    //     }

    //     return list;
    // }

    sendRoomList() {
        let list: impelDown.RoomInfo[] = [];

        for (let index in this._roomMap) {
            let room = this._roomMap[index];
            let roomInfo: impelDown.RoomInfo = room.getRoomInfo();
            list.push(roomInfo);
        }

        let sRoomList: impelDown.S_RoomList = new impelDown.S_RoomList({ roomInfos: list });
        SessionManager.Instance.broadCastMessage(sRoomList.serialize(), impelDown.MSGID.S_ROOMLIST);
    }
}