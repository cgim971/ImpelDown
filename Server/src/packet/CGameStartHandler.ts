import { PacketHandler } from "./PacketHandler";
import SocketSession from "../PlayerData/SocketSession";
import SessionManager from "../SessionManager";
import { impelDown } from "./packet";
import RoomManager from "../Match/Room/RoomManager";
export default class CGameStartHandler implements PacketHandler {
    handleMsg(session:SocketSession, buffer:Buffer) : void{
        let cGameStart :impelDown.C_Game_Start = impelDown.C_Game_Start.deserialize(buffer);
        let player = SessionManager.Instance.getSession(cGameStart.playerId);
        RoomManager.Instance.gameStart(player);
    }
}