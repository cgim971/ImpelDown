import { PacketHandler } from "./PacketHandler";
import SocketSession from "../PlayerData/SocketSession";
import { impelDown } from "./packet";
import SessionManager from "../SessionManager";
import RoomManager from "../Match/Room/RoomManager";
import Room from "../Match/Room/Room";

export default class CQuiteHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cQuite: impelDown.C_Quit = impelDown.C_Quit.deserialize(buffer);
        let player: SocketSession = SessionManager.Instance.getSession(cQuite.playerId);

        console.log(cQuite.playerId);

        // 방에 들어가 있따.
        if (player.getRoomData().getIsRoom() == true) {
            let room: Room | null = RoomManager.Instance.getRoom(player.getRoomData().getRoomIndex());

            if (room?.getIsGaming()) {
                room?.gameQuitRoom(player);
            }
            else {
                room?.exitRoom(player);
            }

            SessionManager.Instance.removeSession(cQuite.playerId);
        }
        // 방에 안 들어감
        else {
            SessionManager.Instance.removeSession(cQuite.playerId);
        }
    }
}