import { PacketHandler } from "./PacketHandler";
import SocketSession from "../PlayerData/SocketSession";
import SessionManager from "../SessionManager";
import { impelDown } from "./packet";
import RoomManager from "../Match/Room/RoomManager";

export default class CCatchHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cCatch: impelDown.C_Catch = impelDown.C_Catch.deserialize(buffer);
        console.log(cCatch.playerId);
        console.log(cCatch.beCatchedPlayerId);
    }
}