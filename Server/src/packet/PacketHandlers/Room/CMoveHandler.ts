import SessionManager from "../../../Game/Managers/SessionManager";
import PlayerSocket from "../../../Player/PlayerSocket";
import SocketSession from "../../../Player/SocketSession";
import { PacketHandler } from "../../PacketHandler";
import { impelDown } from "../../packet";

export default class CMoveHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let msg = impelDown.C_Move.deserialize(buffer);
        let player: PlayerSocket = SessionManager.Instance.getSession(msg.playerId);
        player.getPlayerDataInfo().setPlayerPosData(msg.playerPosData);
    }
}