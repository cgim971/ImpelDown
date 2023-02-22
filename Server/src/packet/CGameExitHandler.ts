// import { PacketHandler } from "./PacketHandler";
// import SocketSession from "../PlayerData/SocketSession";
// import SessionManager from "../SessionManager";
// import { impelDown } from "./packet";
// import RoomManager from "../Match/Room/RoomManager";
// import Room from "../Match/Room/Room";
// export default class CGameExitHandler implements PacketHandler {
//     handleMsg(session:SocketSession, buffer:Buffer) : void{
//         let cGameExit :impelDown.C_Game_Exit = impelDown.C_Game_Exit.deserialize(buffer);
//         let player = SessionManager.Instance.getSession(cGameExit.playerId);
//         let room:Room | null = RoomManager.Instance.getRoom(player.getRoomData().getRoomIndex());
//         room?.gameExit(player);
//     }
// }