import SocketSession from "../../../SocketSession";
import { PacketHandler } from "../../PacketHandler";
import { impelDown } from "../../packet";

export default class CSetRoomHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let msg = impelDown.C_SetRoom.deserialize(buffer);
        // let roomInfo: impelDown.RoomInfo = msg.roomInfo;
    }
}