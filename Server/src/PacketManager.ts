import CCreateRoomHandler from "./packet/room/CCreateRoomHandler";
import CDeleteRoomHandler from "./packet/room/CDeleteRoomHandler";
import CExitRoomHandler from "./packet/room/CExitRoomHandler";
import CJoinRoomHandler from "./packet/room/CJoinRoomHandler";
import { impelDown } from "./packet/packet";
import { PacketHandler } from "./packet/PacketHandler";
import CDebugRoomHandler from "./packet/room/CDebugRoomHandler";

interface HandlerDictionary {
    [key: number]: PacketHandler;
}

export default class PacketManager {
    static Instance: PacketManager;
    handlerMap: HandlerDictionary = {};;

    constructor() {
        console.log("Packet Manager initialize...");
        this.handlerMap = {};
        this.register();
    }

    register(): void {
        this.handlerMap[impelDown.MSGID.C_CREATE_ROOM] = new CCreateRoomHandler;
        this.handlerMap[impelDown.MSGID.C_JOIN_ROOM] = new CJoinRoomHandler;
        this.handlerMap[impelDown.MSGID.C_EXIT_ROOM] = new CExitRoomHandler;
        this.handlerMap[impelDown.MSGID.C_DELETE_ROOM] = new CDeleteRoomHandler;
        this.handlerMap[impelDown.MSGID.C_DEBUG_ROOM] =  new CDebugRoomHandler;
    }
}