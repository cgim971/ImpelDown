import SocketSession from "../SocketSession";
import SessionManager from '../SessionManager';
import { impelDown } from "../packet/packet";
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

    refreshRoomList(): void {
        let sRefreshRoomList: impelDown.S_Refresh_Room_List = new impelDown.S_Refresh_Room_List({ roomInfos: this.getRoomList() });
        SessionManager.Instance.broadCastMessage(sRefreshRoomList.serialize(), impelDown.MSGID.S_REFRESH_ROOM_LIST);
    }

    createRoom(playerId: number, maxPeople: number): void {
        let hostSocket: SocketSession = SessionManager.Instance.getSession(playerId);

        if (hostSocket.getRoomIndex() != -1) {
            console.log("Already in the room");
            return;
        }

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

        this.refreshRoomList();
    }

    joinRoom(playerId: number, roomIndex: number): void {
        let player: SocketSession = SessionManager.Instance.getSession(playerId);
        if (player.getRoomIndex() != -1) {
            console.log("Already in the room");
            return;
        }

        let room: Room = this._roomMap[roomIndex];
        if (room == null) {
            console.log("No Room");

            let sRefreshRoomList: impelDown.S_Refresh_Room_List = new impelDown.S_Refresh_Room_List({ roomInfos: this.getRoomList() });
            player.SendData(sRefreshRoomList.serialize(), impelDown.MSGID.S_REFRESH_ROOM_LIST);
            return;
        }

        room.joinRoom(player);
        this.refreshRoomList();
    }

    exitRoom(playerId: number): void {
        let player: SocketSession = SessionManager.Instance.getSession(playerId);
        if (player.getRoomIndex() == -1) {
            console.log("Not already the room");
            return;
        }

        let room: Room = this._roomMap[player.getRoomIndex()];
        if (room == null) {
            console.log("No Room");

            let sRefreshRoomList: impelDown.S_Refresh_Room_List = new impelDown.S_Refresh_Room_List({ roomInfos: this.getRoomList() });
            player.SendData(sRefreshRoomList.serialize(), impelDown.MSGID.S_REFRESH_ROOM_LIST);
            return;
        }

        room.exitRoom(player);
        this.refreshRoomList();
    }

    deleteRoom(roomIndex: number): void {
        delete this._roomMap[roomIndex];
    }

    getRoomList(): impelDown.RoomInfo[] {
        console.log("__");
        let list: impelDown.RoomInfo[] = [];
        for (let index in this._roomMap) {
            let room: Room = this._roomMap[index];
            if (room != null) {
                let roomIndex: number = +index;
                list.push(new impelDown.RoomInfo({ playerId: room.getHostId(), roomIndex, maxPeople: room.getMaxPeople(), currentPeople: room.getCurrentPeopleCount(), playerInfos: room.getPlayerList() }));
                console.log(room.getHostId() + " \nRoom Index : " + roomIndex + " \nRoom People : " + room.getCurrentPeopleCount() + " / " + room.getMaxPeople() + "\nPlayerInfo : " + room.getPlayerList());
            }
        }
        console.log("__");
        return list;
    }
}