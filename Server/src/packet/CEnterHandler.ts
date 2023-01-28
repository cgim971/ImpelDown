import SessionManager from "../SessionManager";
import SocketSession from "../SocketSession";
import { PacketHandler } from "./PacketHandler";
import { impelDown } from "./packet";

export default class CEnterHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cEnter = impelDown.C_Enter.deserialize(buffer);
        
        
    }
}