import { impelDown } from "./packet/packet";
import SocketSession from "./SocketSession";

interface SessionDictionary {
    [key: number]: SocketSession;
}

export default class SessionManager {
    static Instance: SessionManager;
    sessionMap: SessionDictionary = {};
    count: number = 0;

    constructor() {
        this.sessionMap = {};
        this.count = 0;
    }

    addSession(session: SocketSession, id: number): void {
        this.sessionMap[id] = session;
        this.count += 1;
    }

    removeSession(id: number) {
        delete this.sessionMap[id];
        this.count -= 1;
    }

    getSession(id: number): SocketSession {
        return this.sessionMap[id];
    }

    broadCastMessage(payload: Uint8Array, msgCode: number, senderId: number = 0, exceptSender: boolean = false): void {
        for (let index in this.sessionMap) {
            if (exceptSender == true && senderId == this.sessionMap[index]._playerId) continue;

            this.sessionMap[index].SendData(payload, msgCode);
        }
    }
}