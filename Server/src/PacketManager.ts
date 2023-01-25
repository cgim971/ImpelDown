import CCreateRoomHandler from "./packet/CCreateRoomHandler";
import CEnterHandler from "./packet/CEnterHandler";
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
    }
}