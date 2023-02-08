import { PacketHandler } from "./PacketHandler";
import SocketSession from "../PlayerData/SocketSession";
import SessionManager from "../SessionManager";
import { impelDown } from "./packet";
import RoomManager from "../Match/Room/RoomManager";

export default class CCreateRoomHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cCreateRoom: impelDown.C_Create_Room = impelDown.C_Create_Room.deserialize(buffer);
        let player = SessionManager.Instance.getSession(cCreateRoom.playerId);
        let maxPeople = cCreateRoom.maxPeople;
        RoomManager.Instance.createRoom(player, maxPeople);
    }
}