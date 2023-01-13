import SocketSession from "../../SocketSession";
import { PacketHandler } from "./../PacketHandler";
import { impelDown } from "./../packet";
import RoomManager from "../../RoomManager";

export default class CExitRoomHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cExitRoom = impelDown.C_Exit_Room.deserialize(buffer);

        RoomManager.Instance.exitRoom(cExitRoom.playerInfo.playerId);
    }
}