import { impelDown } from "./packet/packet";
import SocketSession from "./PlayerData/SocketSession";

interface SessionDictionary {
    [key: number]: SocketSession;
}

export default class SessionManager {
    static Instance: SessionManager;
    private _sessionMap: SessionDictionary = {};
    private _socketSessionCount: number = 0;

    constructor() {
        this._sessionMap = {};
        this._socketSessionCount = 0;
    }

    addSession(session: SocketSession, id: number): void {
        this._sessionMap[id] = session;
        this._socketSessionCount += 1;
    }

    removeSession(id: number) {
        delete this._sessionMap[id];
        this._socketSessionCount -= 1;
    }

    getSession(id: number): SocketSession {
        return this._sessionMap[id];
    }

    broadCastMessage(payload: Uint8Array, msgCode: number, senderId: number = 0, exceptSender: boolean = false): void {
        for (let index in this._sessionMap) {
            if (exceptSender == true && senderId == this._sessionMap[index].getPlayerData().getPlayerId()) continue;

            this._sessionMap[index].SendData(payload, msgCode);
        }
    }

    getSessionMap(): SessionDictionary {
        return this._sessionMap;
    }

    getSocketSessionCount(): number {
        return this._socketSessionCount;
    }
}