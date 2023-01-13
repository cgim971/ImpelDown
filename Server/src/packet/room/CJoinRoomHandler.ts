import SocketSession from "../../SocketSession";
import { PacketHandler } from "./../PacketHandler";
import { impelDown } from "./../packet";
import RoomManager from "../../RoomManager";

export default class CJoinRoomHandler implements PacketHandler{
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cJoinRoom = impelDown.C_Join_Room.deserialize(buffer);
        
        RoomManager.Instance.joinRoom(cJoinRoom.roomIndex, cJoinRoom.playerInfo.playerId);
    }
}