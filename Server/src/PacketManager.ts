import CMatchMakingHandler from "./packet/CMatchMakingHandler";
import { impelDown } from "./packet/packet";
import { PacketHandler } from "./packet/PacketHandler";

interface HandlerDictionary {
    [key: number]: PacketHandler;
}

export default class PacketManager {
    static Instance: PacketManager;
    private _handlerMap: HandlerDictionary = {};;

    constructor() {
        console.log("Packet Manager initialize...");
        this._handlerMap = {};
        this.register();
    }

    getHandlerMap():HandlerDictionary{
        return this._handlerMap;
    }
    
    register(): void {
        this._handlerMap[impelDown.MSGID.C_MATCH_MAKING] = new CMatchMakingHandler();
    }
}