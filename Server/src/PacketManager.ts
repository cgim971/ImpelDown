import CCreateRoomHandler from "./packet/CCreateRoomHandler";
import CEnterHandler from "./packet/CEnterHandler";
import CExitRoomHandler from "./packet/CExitRoomHandler";
import CJoinRoomHandler from "./packet/CJoinRoomHandler";
import CRefreshRoomHandler from "./packet/CRefreshRoomHandler";
import { impelDown } from "./packet/packet";
import { PacketHandler } from "./packet/PacketHandler";

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
        this.handlerMap[impelDown.MSGID.C_ENTER] = new CEnterHandler();
        this.handlerMap[impelDown.MSGID.C_CREATE_ROOM] = new CCreateRoomHandler();
        this.handlerMap[impelDown.MSGID.C_JOIN_ROOM] = new CJoinRoomHandler();
        this.handlerMap[impelDown.MSGID.C_EXIT_ROOM] = new CExitRoomHandler();
        this.handlerMap[impelDown.MSGID.C_REFRESH_ROOM] = new CRefreshRoomHandler();
    }
}