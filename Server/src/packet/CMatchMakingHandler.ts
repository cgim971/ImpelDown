import { PacketHandler } from "./PacketHandler";
import SocketSession from "../PlayerData/SocketSession";
export default class CMatchMakingHandler implements PacketHandler {
    handleMsg(session:SocketSession, buffer:Buffer) : void{
        
    }
}