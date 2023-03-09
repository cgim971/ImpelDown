import { impelDown } from "../../packet/packet";
import { PacketHandler } from "../../packet/PacketHandler";
import CPlayerHandler from "../../packet/PacketHandlers/CPlayerHandler";
import CCreateRoomHandler from "../../packet/PacketHandlers/Room/CCreateRoomHandler";
import CExitRoomHandler from "../../packet/PacketHandlers/Room/CExitRoomHandler";
import CJoinRoomHandler from "../../packet/PacketHandlers/Room/CJoinRoomHandler";
import CMatchMakingHandler from "../../packet/PacketHandlers/Room/CMatchMakingHandler";
import CSetRoomHandler from "../../packet/PacketHandlers/Room/CSetRoomHandler";

interface HandlerDictionary {
    [key: number]: PacketHandler;
}

export default class PacketManager {
    static Instance: PacketManager;
    private handlerMap: HandlerDictionary = {};;

    constructor() {
        console.log("Packet Manager initialize...");
        this.handlerMap = {};
        this.register();
    }

    getHandlerMap(): HandlerDictionary {
        return this.handlerMap;
    }

    register(): void {
        this.handlerMap[impelDown.MSGID.C_PLAYER] = new CPlayerHandler();

        this.handlerMap[impelDown.MSGID.C_CREATE_ROOM] = new CCreateRoomHandler();
        this.handlerMap[impelDown.MSGID.C_JOIN_ROOM] = new CJoinRoomHandler();
        this.handlerMap[impelDown.MSGID.C_EXIT_ROOM] = new CExitRoomHandler();
        this.handlerMap[impelDown.MSGID.C_MATCH_MAKING] = new CMatchMakingHandler();
        this.handlerMap[impelDown.MSGID.C_SET_ROOM] = new CSetRoomHandler();
    }
}