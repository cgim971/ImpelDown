import { PacketHandler } from "./../PacketHandler";
import SocketSession from "../../PlayerData/SocketSession";
import SessionManager from "../../SessionManager";
import { impelDown } from "./../packet";
import RoomManager from "../../Match/Room/RoomManager";
import Room from "../../Match/Room/Room";

export default class CNinjaSkillHandler implements PacketHandler {
    handleMsg(session: SocketSession, buffer: Buffer): void {
        let cNinjaSkill: impelDown.C_Ninja_Skill = impelDown.C_Ninja_Skill.deserialize(buffer);
        let player: SocketSession = SessionManager.Instance.getSession(cNinjaSkill.playerId);
        
    }
}