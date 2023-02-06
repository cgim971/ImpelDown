import { PacketHandler } from "./PacketHandler";
import SocketSession from "../PlayerData/SocketSession";
import MatchManager from "../Match/MatchManager";
import SessionManager from "../SessionManager";
import { impelDown } from "./packet";
export default class CMatchMakingHandler implements PacketHandler {
    handleMsg(session:SocketSession, buffer:Buffer) : void{
        let cMatchMaking :impelDown.C_Match_Making = impelDown.C_Match_Making.deserialize(buffer);
        let player = SessionManager.Instance.getSession(cMatchMaking.playerId);
        MatchManager.Instance.matchMaking(player);
    }
}