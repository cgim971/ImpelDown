import SocketSession from "../SocketSession";
import { impelDown } from "../packet/packet";
import WebSocket, { RawData } from "ws";
import PlayerDataInfo from "./PlayerDataInfo";


export default class PlayerSocket extends SocketSession {

    private _playerDataInfo: PlayerDataInfo;

    constructor(socket: WebSocket, playerId: number, CloseCallback: Function) {
        super(socket, CloseCallback);
        this._playerDataInfo = new PlayerDataInfo(playerId);
    }

    public getPlayerId(): number {
        return this._playerDataInfo.getPlayerId();
    }

    public getPlayerName(): string {
        return this._playerDataInfo.getPlayerName();
    }

    public getPlayerDataInfo(): PlayerDataInfo {
        return this._playerDataInfo;
    }


}