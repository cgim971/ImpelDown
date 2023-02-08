import RoomManager from "../Room/RoomManager";
import SessionManager from "../SessionManager";
import SocketSession from "../SocketSession";
import { PacketHandler } from "./PacketHandler";
import { impelDown } from "./packet";

export default class CCatchHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cCatch = impelDown.C_Catch.deserialize(buffer);
        RoomManager.Instance.getRoom(cCatch.playerData.roomIndex).die(cCatch.playerData.playerId);
    }
}