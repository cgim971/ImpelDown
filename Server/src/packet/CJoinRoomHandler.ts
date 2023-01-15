import RoomManager from "../Room/RoomManager";
import SessionManager from "../SessionManager";
import SocketSession from "../SocketSession";
import { impelDown } from "./packet";
import { PacketHandler } from "./PacketHandler";

export default class CJoinRoomHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cJoinRoom = impelDown.C_Join_Room.deserialize(buffer);

        // let roomId: number = cJoinRoom.roomInfo.roomId;
        // let playerId: number = cJoinRoom.roomInfo.playerInfo.playerId;

        // RoomManager.Instance.joinRoom(roomId, playerId);
    }
} 