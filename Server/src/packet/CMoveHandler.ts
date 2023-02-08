import SessionManager from "../SessionManager";
import SocketSession from "../SocketSession";
import { PacketHandler } from "./PacketHandler";
import { impelDown } from "./packet";

export default class CMoveHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cMove = impelDown.C_Move.deserialize(buffer);

        SessionManager.Instance.getSession(cMove.playerAllData.playerData.playerId).setPosAndRot(cMove.playerAllData.posAndRot);
    }
}