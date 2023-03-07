import WebSocket, { RawData } from "ws";
import { impelDown } from "./packet/packet";
import PacketManager from "./Game/Managers/PacketManager";


export default class SocketSession {

    protected _socket: WebSocket;

    constructor(socket: WebSocket, CloseCallback: Function) {
        this._socket = socket;

        this._socket.on("close", () => {
            CloseCallback();
        });
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