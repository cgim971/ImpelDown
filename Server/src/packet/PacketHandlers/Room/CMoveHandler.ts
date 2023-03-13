import SessionManager from "../../../Game/Managers/SessionManager";
import Room from "../../../Game/Room/Room";
import RoomManager from "../../../Game/Room/RoomManager";
import PlayerSocket from "../../../Player/PlayerSocket";
import SocketSession from "../../../Player/SocketSession";
import { PacketHandler } from "../../PacketHandler";
import { impelDown } from "../../packet";

export default class CMoveHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let msg = impelDown.C_Move.deserialize(buffer);
        let player : PlayerSocket = SessionManager.Instance.getSession(msg.playerId);
        let room: Room = RoomManager.Instance.getRoom(player.getPlayerDataInfo().getRoomIndex());
        
    }
}