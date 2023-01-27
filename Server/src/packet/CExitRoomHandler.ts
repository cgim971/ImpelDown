import RoomManager from "../Room/RoomManager";
import SessionManager from "../SessionManager";
import SocketSession from "../SocketSession";
import { PacketHandler } from "./PacketHandler";
import { impelDown } from "./packet";

export default class CExitRoomHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        
    }
}