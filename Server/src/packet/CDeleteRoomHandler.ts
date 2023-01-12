import RoomManager from "../RoomManager";
import SocketSession from "../SocketSession";
import { impelDown } from "./packet";
import { PacketHandler } from "./PacketHandler";

export default class CDeleteRoomHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cDeleteRoom = impelDown.C_Delete_Room.deserialize(buffer);

        RoomManager.Instance.deleteRoom(cDeleteRoom.roomIndex);
    }
}   