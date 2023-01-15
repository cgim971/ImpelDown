import RoomManager from "../Room/RoomManager";
import SessionManager from "../SessionManager";
import SocketSession from "../SocketSession";
import { impelDown } from "./packet";
import { PacketHandler } from "./PacketHandler";

export default class CCreateRoomHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cCreateRoom = impelDown.C_Create_Room.deserialize(buffer);

        let { playerInfo, maximumPeople } = cCreateRoom;

        console.log("방 생성 -");
        RoomManager.Instance.createRoom(playerInfo.playerId, maximumPeople);
    }
}