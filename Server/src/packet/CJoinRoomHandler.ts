import RoomManager from "../Room/RoomManager";
import SessionManager from "../SessionManager";
import SocketSession from "../SocketSession";
import { PacketHandler } from "./PacketHandler";
import { impelDown } from "./packet";

export default class CJoinRoomHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cJoinRoom = impelDown.C_Create_Room.deserialize(buffer);
        let playerId: number = cJoinRoom.roomData.playerId;
        let roomIndex: number = cJoinRoom.roomData.roomIndex;

        RoomManager.Instance.joinRoom(playerId, roomIndex);
    }
}