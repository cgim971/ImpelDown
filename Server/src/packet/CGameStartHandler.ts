import RoomManager from "../Room/RoomManager";
import SessionManager from "../SessionManager";
import SocketSession from "../SocketSession";
import { PacketHandler } from "./PacketHandler";
import { impelDown } from "./packet";

export default class CGameStartHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        console.log("Start");
        let cGameStart = impelDown.C_Game_Start.deserialize(buffer);

        RoomManager.Instance.startGame(cGameStart.playerData.roomIndex, cGameStart.mapData.mapIndex);
    }
}