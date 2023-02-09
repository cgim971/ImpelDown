import { PacketHandler } from "./PacketHandler";
import SocketSession from "../PlayerData/SocketSession";
import { impelDown } from "./packet";
import RoomManager from "../Match/Room/RoomManager";
export default class CRefreshRoomList implements PacketHandler {
    handleMsg(session:SocketSession, buffer:Buffer) : void{
        let cRefreshRoomList :impelDown.C_Refresh_RoomList = impelDown.C_Refresh_RoomList.deserialize(buffer);
        RoomManager.Instance.sRefreshRoomList();
    }
}