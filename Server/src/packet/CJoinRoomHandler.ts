import { PacketHandler } from "./PacketHandler";
import SocketSession from "../PlayerData/SocketSession";
import SessionManager from "../SessionManager";
import { impelDown } from "./packet";
import RoomManager from "../Match/Room/RoomManager";
export default class CJoinRoomHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cJoinRoom: impelDown.C_Join_Room = impelDown.C_Join_Room.deserialize(buffer);
        let player = SessionManager.Instance.getSession(cJoinRoom.playerId);
        let roomIndex: number = cJoinRoom.roomIndex;
        RoomManager.Instance.joinRoom(player, roomIndex);
    }
}