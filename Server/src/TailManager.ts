import Room from "./Match/Room/Room";
import SocketSession from "./PlayerData/SocketSession";

interface PlayerDictionary {
    [key: number]: SocketSession;
}

export default class TailManager {
    private _room: Room;
    private _playerMap: PlayerDictionary;


    constructor(room: Room) {
        this._room = room;
        this._playerMap = [];

    }


    init(): void {
        this._playerMap = this._room.getPlayerMap();
        let playerCount = this._room.getPlayerCount();
        let list: number[] = [];
        for (let i = 0; i < playerCount; i++) {
            list[i] = i;
        }
        let count: number = Math.floor(Math.random() * 200);

        while (count--) {
            let temp1: number = Math.floor(Math.random() * playerCount);
            let temp2: number = Math.floor(Math.random() * playerCount);
            let temp: number = list[temp1];

            list[temp1] = list[temp2];
            list[temp2] = temp;
        }

        // 꼬리 섞기
        let tailCount = 0;
        for(let index in this._playerMap){
            if(this._playerMap[index] != null){
                this._playerMap[index].getPlayerData().setTailIndex(list[tailCount++]);
            }
        }
    }

    // 잡거나 잡히면 리스트 갱신
    refreshList() : void{
    }
}