import SocketSession from "../PlayerData/SocketSession";

export interface PacketHandler{
    handleMsg(session:SocketSession, buffer:Buffer) : void;
}
