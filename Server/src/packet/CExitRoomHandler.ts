import { PacketHandler } from "./PacketHandler";
import SocketSession from "../PlayerData/SocketSession";
import SessionManager from "../SessionManager";
import { impelDown } from "./packet";
import RoomManager from "../Match/Room/RoomManager";
export default class CExitRoomHandler implements PacketHandler {
    handleMsg(session:SocketSession, buffer:Buffer) : void{
        let cExitRoom :impelDown.C_Exit_Room = impelDown.C_Exit_Room.deserialize(buffer);
        let player = SessionManager.Instance.getSession(cExitRoom.playerId);
        RoomManager.Instance.exitRoom(player);
    }
}