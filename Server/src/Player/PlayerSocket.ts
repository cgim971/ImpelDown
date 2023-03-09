import SocketSession from "../SocketSession";
import { impelDown } from "../packet/packet";
import WebSocket, { RawData } from "ws";
import PlayerDataInfo from "./PlayerDataInfo";


export default class PlayerSocket extends SocketSession {

    private _playerData: impelDown.PlayerInGameData;

    private _playerDataInfo: PlayerDataInfo;

    constructor(socket: WebSocket, playerId: number, CloseCallback: Function) {
        super(socket, CloseCallback);
        this._playerDataInfo = new PlayerDataInfo(playerId);
        this._playerData = this._playerDataInfo.getPlayerData();

    }

    public getPlayerId(): number {
        return this._playerDataInfo.getPlayerId();
    }

    public getPlayerDataInfo(): PlayerDataInfo {
        return this._playerDataInfo;
    }

 
}