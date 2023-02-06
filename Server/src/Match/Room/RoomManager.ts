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
            console.log("Error - Already In Room!");
            return;
        }

        let roomIndex: number = 0;
        while (this._roomMap[roomIndex] != null) {
            roomIndex++;
        }
        let room: Room = new Room(hostSocket, roomIndex, maxPeople);
        this._roomMap[roomIndex] = room;

        this.sRefreshRoomList(room, hostSocket);
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
        if (player.getRoomData().getIsRoom() == true) {
            console.log("Error - Already In Room!");
            return;
        }

        for (let index in this._roomMap) {
            if (this._roomMap[index].getIsGaming() == true || this._roomMap[index].getIsEmpty() == false) continue;
            let room: Room = this._roomMap[index];
            room.joinRoom(player);

            this.sRefreshRoomList(room, player);
            return;
        }

        console.log("No Room");
        this.createRoom(player, 8);
    }

    getRoom(roomIndex: number): Room | null {
        let room: Room = this._roomMap[roomIndex];
        if (room == null) {
            console.log("Error - No Room!");
            return null;
        }
        return room;
    }

    sRefreshRoomList(room: Room, player: SocketSession): void {
        let roomInfo: impelDown.RoomInfo = room.getRoomInfo();
        let sRefreshRoomList: impelDown.S_Refresh_RoomList = new impelDown.S_Refresh_RoomList({roomInfo});
        console.log(sRefreshRoomList.roomInfo);
        player.SendData(sRefreshRoomList.serialize(), impelDown.MSGID.S_REFRESH_ROOMLIST);
    }
}