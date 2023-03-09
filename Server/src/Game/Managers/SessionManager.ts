import PlayerSocket from "../../Player/PlayerSocket";

export interface SessionDictionary {
    [key: number]: PlayerSocket;
}

export default class SessionManager {
    static Instance: SessionManager;
    private _sessionMap: SessionDictionary = {};
    private _count: number = 0;

    constructor() {
        this._sessionMap = {};
        this._count = 0;
    }

    addSession(session: PlayerSocket, id: number): void {
        this._sessionMap[id] = session;
        this._count += 1;
    }

    removeSession(id: number) {
        delete this._sessionMap[id];
        this._count -= 1;
    }

    getSessionMap():SessionDictionary{
        return this._sessionMap;
    }
    
    getSession(id: number): PlayerSocket {
        return this._sessionMap[id];
    }

    broadCastMessage(payload: Uint8Array, msgCode: number, senderId: number = 0, exceptSender: boolean = false): void {
        for (let index in this._sessionMap) {
            if (exceptSender == true && senderId == this._sessionMap[index].getPlayerId()) continue;
            this._sessionMap[index].SendData(payload, msgCode);
        }
    }

    getCount(): number {
        return this._count;
    }
}