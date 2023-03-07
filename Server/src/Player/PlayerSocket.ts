import SocketSession from "../SocketSession";
import { impelDown } from "../packet/packet";
import WebSocket, { RawData } from "ws";
import PlayerDataInfo from "./PlayerDataInfo";
import RoomDataInfo from "./RoomDataInfo";


export default class PlayerSocket extends SocketSession {

    private _playerData: impelDown.PlayerData;
    private _roomInfo: impelDown.RoomInfo;

    private _playerDataInfo: PlayerDataInfo;
    private _roomDataInfo: RoomDataInfo;

    constructor(socket: WebSocket, playerId: number, CloseCallback: Function) {
        super(socket, CloseCallback);
        this._playerDataInfo = new PlayerDataInfo(playerId);
        this._playerData = this._playerDataInfo.getPlayerData();

        this._roomDataInfo = new RoomDataInfo();
        this._roomInfo = this._roomDataInfo.getRoomInfo();
    }

    public getPlayerId(): number {
        return this._playerDataInfo.getPlayerId();
    }

    public getPlayerDataInfo(): PlayerDataInfo {
        return this._playerDataInfo;
    }

    public getRoomDataInfo():RoomDataInfo{
        return this._roomDataInfo;
    }
}