import RoomManager from "./RoomManager";
import SocketSession from "./SocketSession";

interface SessionDictionary {
    [key: number]: SocketSession;
}

export default class Room {

    private _roomIndex: number = 0;

    private _maximumPeople: number = 0;
    private _currentPeopleCount: number = 0;
    private _sessionMap: SessionDictionary = {};

    constructor(roomIndex: number, maximumPeople: number) {
        this._roomIndex = roomIndex;
        this._maximumPeople = maximumPeople;
        this._currentPeopleCount = 0;
        this._sessionMap = {};
    }

    joinRoom(session: SocketSession): void {
        let sessionIndex: number = 0;
        for (sessionIndex = 0; sessionIndex < this._maximumPeople + 1; sessionIndex++) {
            if (this._sessionMap[sessionIndex] == null) {
                this._sessionMap[sessionIndex] = session;
                session.setPlayerRoom(this._roomIndex, this._currentPeopleCount++);
                console.log("Join room [%d] - Player : %d", this._roomIndex, session.getPlayerId());
                return;
            }
        }

        if (sessionIndex == this._maximumPeople + 1) {
            console.log("Error - This room is full!");
            return;
        }
    }

    exitRoom(session: SocketSession): void {
        let playerId : number = session.getPlayerId();
        
        console.log("Exit room [%d] - Player : %d", this._roomIndex,playerId);
        delete this._sessionMap[playerId];
        
        if(--this._currentPeopleCount == 0){
            RoomManager.Instance.deleteRoom(playerId);
        }
        else{
            session.setPlayerRoom();
        }
    }

    deleteRoom(): void {
        let sessionIndex: number = 0;
        for (sessionIndex = 0; sessionIndex < this._maximumPeople; sessionIndex++) {
            let session: SocketSession = this._sessionMap[sessionIndex]
            if (session != null) {
                this.exitRoom(session);
            }
        }
    }

    debugRoom():void{
        let sessionIndex: number = 0;
        for (sessionIndex = 0; sessionIndex < this._maximumPeople; sessionIndex++) {
            let session: SocketSession = this._sessionMap[sessionIndex]
            if (session != null) {
                console.log(session.getPlayerId());
            }
        }
    }
} 