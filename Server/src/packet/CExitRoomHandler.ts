import RoomManager from "../Room/RoomManager";
import SessionManager from "../SessionManager";
import SocketSession from "../SocketSession";
import { impelDown } from "./packet";
import { PacketHandler } from "./PacketHandler";

export default class CExitRoomHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cExitRoom = impelDown.C_Exit_Room.deserialize(buffer);

        RoomManager.Instance.exitRoom(cExitRoom.playerInfo.playerId);
    }
}