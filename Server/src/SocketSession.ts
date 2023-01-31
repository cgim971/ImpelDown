import WebSocket, { RawData } from "ws";
import { impelDown } from "./packet/packet";
import PacketManager from "./PacketManager";


export default class SocketSession {

    private _socket: WebSocket;
    private _playerId: number;

    private _room: boolean;
    private _roomIndex: number;

    private _posAndRot :impelDown.PosAndRot;

    constructor(socket: WebSocket, playerId: number, CloseCallback: Function) {
        this._socket = socket;
        this._playerId = playerId;
        this._room = false;
        this._roomIndex = -1;
        this._posAndRot = new impelDown.PosAndRot();

        this._socket.on("close", () => {
            CloseCallback();
        });
    }

    getPlayerId(): number {
        return this._playerId;
    }

    setRoom(room: boolean): void {
        this._room = room;
    }

    getRoom(): boolean {
        return this._room;
    }

    setRoomIndex(roomIndex:number):void{
        this._roomIndex = roomIndex;
    }

    getRoomIndex():number{
        return this._roomIndex;
    }

    getInt16FEFromBuffer(buffer: Buffer): number {
        return buffer.readInt16LE();
    }

    receiveMsg(data: RawData): void {
        let code: number = this.getInt16FEFromBuffer(data.slice(2, 4) as Buffer);
        PacketManager.Instance.handlerMap[code].handleMsg(this, data.slice(4) as Buffer);
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


    setPosAndRot( posAndRot:impelDown.PosAndRot):void{
        this._posAndRot = posAndRot;
    }
    getPosAndRot():impelDown.PosAndRot{
        return this._posAndRot;
    }
}