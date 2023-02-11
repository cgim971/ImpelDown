import SessionManager from "../../SessionManager";
import SocketSession from "../../PlayerData/SocketSession";
import Room from "./Room";
import { impelDown } from "../../packet/packet";

interface RoomDictionary {
    [key: number]: Room;
}

export default class RoomManager {
    static Instance: RoomManager;
    private _roomMap: RoomDictionary;

    constructor() {
        this._roomMap = [];
    }

    createRoom(hostSocket: SocketSession, maxPeople: number) {
        if (hostSocket.getRoomData().getIsRoom() == true) {
            console.log("Error_CreateRoom - Already In Room!");
            return;
        }

        let roomIndex: number = 0;
        while (this._roomMap[roomIndex] != null) {
            roomIndex++;
        }
        let room: Room = new Room(hostSocket, roomIndex, maxPeople);
        this._roomMap[roomIndex] = room;

        this.sRefreshRoomList();
    }

    joinRoom(player: SocketSession, roomIndex: number): void {
        let room: Room = this._roomMap[roomIndex];
        if (room == null) {
            console.log("Error_JoinRoom - No Room!");
            return;
        }

        room.joinRoom(player);
        this.sRefreshRoomList();
        return;
    }

    exitRoom(player: SocketSession): void {
        let room: Room | null = null;
        for (let index in this._roomMap) {
            if (this._roomMap[index].getRoomIndex() == player.getRoomData().getRoomIndex()) {
                room = this._roomMap[index];
                break;
            }
        }

        if(room == null){
            console.log("Error_ExitRoom - No Room!");
            return;
        }

        room.exitRoom(player);
        this.sRefreshRoomList();
        return;
    }

    deleteRoom(roomIndex: number): void {
        // 사람이 한 명도 없으면 방 삭제
        let room: Room = this._roomMap[roomIndex];
        if (room == null) {
            console.log("Error - No Room!");
            return;
        }

        delete this._roomMap[roomIndex];
        this.sRefreshRoomList();
        return;
    }

    // 방찾기
    matchMaking(player: SocketSession): void {
        if (player.getRoomData().getIsRoom() == true) {
            console.log("Error - Already In Room!");
            return;
        }

        for (let index in this._roomMap) {
            if (this._roomMap[index].getIsGaming() == true || this._roomMap[index].getIsEmpty() == false) continue;
            let room: Room = this._roomMap[index];
            room.joinRoom(player);

            this.sRefreshRoomList();
            return;
        }

        console.log("No Room");
        this.createRoom(player, 8);
        return;
    }

    getRoom(roomIndex: number): Room | null {
        let room: Room = this._roomMap[roomIndex];
        if (room == null) {
            console.log("Error - No Room!");
            return null;
        }
        return room;
    }

    sRefreshRoomList(): void {

        let roomInfos: impelDown.RoomInfo[] = [];

        for (let index in this._roomMap) {
            let room: Room = this._roomMap[index];
            if (room != null) {
                roomInfos.push(room.getRoomInfo());
            }
        }

        let sRefreshRoomList: impelDown.S_Refresh_RoomList = new impelDown.S_Refresh_RoomList({ roomInfos });
        this.broadCastMessage(sRefreshRoomList.serialize(), impelDown.MSGID.S_REFRESH_ROOMLIST);
    }

    broadCastMessage(payload: Uint8Array, msgCode: number, senderId: number = 0, exceptSender: boolean = false): void {
        let sessionMap = SessionManager.Instance.getSessionMap();
        for (let index in sessionMap) {
            // 방에 들어간 사람 제외하고 갱신
            if (sessionMap[index].getRoomData().getIsRoom() == true) continue;
            sessionMap[index].SendData(payload, msgCode);
        }
    }
}