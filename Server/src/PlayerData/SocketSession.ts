import WebSocket, { RawData } from "ws";
import { impelDown } from "../packet/packet";
import PacketManager from "../PacketManager";
import RoomData from "./RoomData";
import PlayerData from "./PlayerData";


export default class SocketSession {
    private _socket: WebSocket;
    
    private _playerData :PlayerData;
    private _roomData : RoomData;
    
    constructor(socket: WebSocket, playerId: number, CloseCallback: Function) {
        this._socket = socket;
        
        this._playerData = new PlayerData(playerId, "");
        this._roomData = new RoomData();
        
        this._socket.on("close", () => {
            CloseCallback();
        });
    }

    getPlayerData(): PlayerData {
        return this._playerData;
    }
    getRoomData() :RoomData{
        return this._roomData;
    }

    getPlayerInfo(): impelDown.PlayerInfo{
        return this._playerData.getPlayerInfo();
    }
    
    getInt16FEFromBuffer(buffer: Buffer): number {
        return buffer.readInt16LE();
    }

    receiveMsg(data: RawData): void {
        let code: number = this.getInt16FEFromBuffer(data.slice(2, 4) as Buffer);
        PacketManager.Instance.getHandlerMap()[code].handleMsg(this, data.slice(4) as Buffer);
    }

    SendData(payload: Uint8Array, msgCode: number): void {
        let len: number = payload.length + 4;

        let lenBuffer: Uint8Array = new Uint8Array(2);
        new DataView(lenBuffer.buffer).setUint16(0, len, true);

        let msgCodeBuffer: Uint8Array = new Uint8Array(2);
        new DataView(msgCodeBuffer.buffer).setUint16(0, msgCode, true);

        let sendBuffer: Uint8Array = new Uint8Array(len);
        sendBuffer.set(lenBuffer, 0);
        sendBuffer.set(msgCodeBuffer, 2);
        sendBuffer.set(payload, 4);

        this._socket.send(sendBuffer);
    }
}