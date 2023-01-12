import RoomManager from "../RoomManager";
import SocketSession from "../SocketSession";
import { impelDown } from "./packet";
import { PacketHandler } from "./PacketHandler";

export default class CCreateRoomHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cCreateRoom = impelDown.C_Create_Room.deserialize(buffer);
        
        RoomManager.Instance.createRoom(cCreateRoom._maximumPeople, cCreateRoom.playerInfo.playerId);
    }
}