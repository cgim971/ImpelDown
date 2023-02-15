import { PacketHandler } from "./PacketHandler";
import SocketSession from "../PlayerData/SocketSession";
import SessionManager from "../SessionManager";
import { impelDown } from "./packet";
import RoomManager from "../Match/Room/RoomManager";
import Room from "../Match/Room/Room";

export default class CCatchHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cCatch: impelDown.C_Catch = impelDown.C_Catch.deserialize(buffer);
        let player: SocketSession = SessionManager.Instance.getSession(cCatch.playerId);
        let beCatchedPlayer: SocketSession = SessionManager.Instance.getSession(cCatch.beCatchedPlayerId);
        let room: Room | null = RoomManager.Instance.getRoom(player.getRoomData().getRoomIndex());
        room?.catchPlayer(player, beCatchedPlayer);
    }
}