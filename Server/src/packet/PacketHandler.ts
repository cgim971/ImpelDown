import SocketSession from "../Player/SocketSession";

export interface PacketHandler{
    handleMsg(session:SocketSession, buffer:Buffer) : void;
}
