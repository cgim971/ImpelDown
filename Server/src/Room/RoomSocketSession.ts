import WebSocket, { RawData } from "ws";
import PacketManager from "./../PacketManager";
import RoomManager from "./RoomManager";

export default class RoomSocketSession {

    private _socket: WebSocket;

    constructor() {
        this._socket = new WebSocket("ws://172.31.3.19:8181");
    }

    getSocket(): WebSocket {
        return this._socket;
    }

    // 게임 시작 같은 방 설정 부분 전달 받음
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