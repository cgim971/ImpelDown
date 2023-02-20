import { PacketHandler } from "./PacketHandler";
import SocketSession from "../PlayerData/SocketSession";
import { impelDown } from "./packet";
import SessionManager from "../SessionManager";

export default class CMoveHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cMove: impelDown.C_Move = impelDown.C_Move.deserialize(buffer);
        SessionManager.Instance.getSession(cMove.playerId).getPlayerData().setPositionInfo(cMove.positionInfo);
    }
}