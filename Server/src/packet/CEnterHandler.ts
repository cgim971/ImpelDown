import SessionManager from "../SessionManager";
import SocketSession from "../SocketSession";
import { impelDown } from "./packet";
import { PacketHandler } from "./PacketHandler";

export default class CEnterHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cEnter = impelDown.C_Enter.deserialize(buffer);

        let playerSocket: SocketSession = SessionManager.Instance.getSession(cEnter.playerInfo.playerId);
        playerSocket.SendData(cEnter.serialize(), impelDown.MSGID.S_ENTER);
    }
}