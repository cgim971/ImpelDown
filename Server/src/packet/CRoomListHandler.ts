import RoomManager from "../Room/RoomManager";
import SessionManager from "../SessionManager";
import SocketSession from "../SocketSession";
import { impelDown } from "./packet";
import { PacketHandler } from "./PacketHandler";

export default class CRoomListHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        RoomManager.Instance.sendRoomList();
    }
}