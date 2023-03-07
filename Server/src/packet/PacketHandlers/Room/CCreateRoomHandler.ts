import SocketSession from "../../../SocketSession";
import { PacketHandler } from "../../PacketHandler";
import { impelDown } from "../../packet";

export default class CCreateRoomHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cSetRoom = impelDown.C_CreateRoom.deserialize(buffer);
    }
}