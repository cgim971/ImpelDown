import RoomManager from "../../RoomManager";
import SocketSession from "../../SocketSession";
import { impelDown } from "./../packet";
import { PacketHandler } from "./../PacketHandler";

export default class CDebugRoomHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cDebugRoom = impelDown.C_Debug_Room.deserialize(buffer);
        
        RoomManager.Instance.debugRoom(cDebugRoom.roomIndex);
    }
}