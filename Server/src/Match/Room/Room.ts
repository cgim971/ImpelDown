import SocketSession from "../../PlayerData/SocketSession";

interface PlayerDictionary {
    [key: number]: SocketSession;
}

export default class Room {
    private _hostSocket: SocketSession;

    private _playerMap: PlayerDictionary = {};
    private _ghostPlayerMap: PlayerDictionary = {};
    private _playerCount: number;
    private _roomIndex: number;

    private _isGameing: boolean;

    constructor(hostSocket: SocketSession, roomIndex: number) {
        this._hostSocket = hostSocket;

        this._playerMap = [];
        this._playerCount = 0;
        this._roomIndex = roomIndex;

        this._isGameing = false;

        this.joinRoom();
    }

    startGame(): void {
        if (this._isGameing == true) return;
        this._isGameing = true;
    }

    diePlayer(player: SocketSession): void {
        // 죽으면 유령이 됌
        for (let index in this._playerMap) {
            if (this._playerMap[index] == player) {
                this._ghostPlayerMap[index] = player;
                return;
            }
        }
    }

    endGame(): void {

        // 게임이 끝나면 그대로 들어옴
        for (let index in this._ghostPlayerMap) {
            if(this._ghostPlayerMap[index] != null){
                this._playerMap[index] = this._ghostPlayerMap[index];
            }
        }

        
    }

    joinRoom(player: SocketSession = this._hostSocket): void {
        if (this._isGameing == true) return;

        let playerIndex: number = 0;
        while (this._playerMap[playerIndex] != null) {
            playerIndex++;
        }
        this._playerMap[playerIndex] = player;
        this._playerCount += 1;
    }


    exitRoom(player: SocketSession): void {
        if (this._playerCount > 2) {
            // 플레이어가 2명
        }

        if (this._hostSocket == player) {
            // 호스트 변경
        }
        return;
    }

    getIsGaming(): boolean {
        return this._isGameing;
    }




}