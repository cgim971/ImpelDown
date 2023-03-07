import SocketSession from "../../../SocketSession";
import { PacketHandler } from "../../PacketHandler";
import { impelDown } from "../../packet";

export default class CJoinRoomHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cSetRoom = impelDown.C_JoinRoom.deserialize(buffer);
    }
}