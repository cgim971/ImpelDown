import SessionManager from "../../../Game/Managers/SessionManager";
import RoomManager from "../../../Game/Room/RoomManager";
import PlayerSocket from "../../../Player/PlayerSocket";
import SocketSession from "../../../SocketSession";
import { PacketHandler } from "../../PacketHandler";
import { impelDown } from "../../packet";

export default class CMatchMakingHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let msg = impelDown.C_MatchMaking.deserialize(buffer);
        let player: PlayerSocket = SessionManager.Instance.getSession(msg.playerId);
        RoomManager.Instance.matchMaking(player);
    }
}