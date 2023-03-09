import SessionManager from "../../Game/Managers/SessionManager";
import PlayerSocket from "../../Player/PlayerSocket";
import SocketSession from "../../SocketSession";
import { PacketHandler } from "../PacketHandler";
import { impelDown } from "../packet";

export default class CPlayerHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let msg = impelDown.C_Player.deserialize(buffer);
        let player: PlayerSocket = SessionManager.Instance.getSession(msg.playerId);
        player.getPlayerDataInfo().setPlayerName(msg.playerName);
        let data: impelDown.S_Player = new impelDown.S_Player({ playerInfo: player.getPlayerDataInfo().getPlayerInfo() })
        player.SendData(data.serialize(), impelDown.MSGID.S_PLAYER);
    }
}