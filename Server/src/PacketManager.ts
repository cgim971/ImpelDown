import CExitRoomHandler from "./packet/CExitRoomHandler";
import CCreateRoomHandler from "./packet/CCreateRoomHandler";
import CJoinRoomHandler from "./packet/CJoinRoomHandler";
import CMatchMakingHandler from "./packet/CMatchMakingHandler";
import CRefreshRoomListHandler from "./packet/CRefreshRoomListHandler";
import { impelDown } from "./packet/packet";
import { PacketHandler } from "./packet/PacketHandler";
import CGameStartHandler from "./packet/CGameStartHandler";
import CMoveHandler from "./packet/CMoveHandler";

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

    getHandlerMap(): HandlerDictionary {
        return this._handlerMap;
    }

    register(): void {
        this._handlerMap[impelDown.MSGID.C_MATCH_MAKING] = new CMatchMakingHandler();
        this._handlerMap[impelDown.MSGID.C_CREATE_ROOM] = new CCreateRoomHandler();
        this._handlerMap[impelDown.MSGID.C_JOIN_ROOM] = new CJoinRoomHandler();
        this._handlerMap[impelDown.MSGID.C_EXIT_ROOM] = new CExitRoomHandler();
        this._handlerMap[impelDown.MSGID.C_REFRESH_ROOMLIST] = new CRefreshRoomListHandler();



        this._handlerMap[impelDown.MSGID.C_GAME_START] = new CGameStartHandler();
        this._handlerMap[impelDown.MSGID.C_MOVE] = new CMoveHandler();
    }
}