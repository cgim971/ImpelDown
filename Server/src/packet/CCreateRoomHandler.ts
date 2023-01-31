import RoomManager from "../Room/RoomManager";
import SessionManager from "../SessionManager";
import SocketSession from "../SocketSession";
import { PacketHandler } from "./PacketHandler";
import { impelDown } from "./packet";

export default class CCreateRoomHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cCreateRoom = impelDown.C_Create_Room.deserialize(buffer);
        let playerId: number = cCreateRoom.roomData.playerId;
        let maxPeople: number = cCreateRoom.roomData.maxPeople;

        RoomManager.Instance.createRoom(playerId, maxPeople);
    }
}