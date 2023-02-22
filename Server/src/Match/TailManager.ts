import Room from "./Room/Room";
import SocketSession from "../PlayerData/SocketSession";
import { impelDown } from "../packet/packet";

interface PlayerDictionary {
    [key: number]: SocketSession;
}

export default class TailManager {
    private _room: Room;
    private _playerMap: PlayerDictionary;
    private _playerIndexMap: PlayerDictionary;
    private _playerCount: number;

    constructor(room: Room) {
        this._room = room;
        this._playerMap = [];
        this._playerIndexMap = [];
        this._playerCount = 0;
    }


    init(): void {
        this._playerMap = this._room.getPlayerMap();
        this._playerCount = this._room.getPlayerCount();
        let list: number[] = [];
        for (let i = 0; i < this._playerCount; i++) {
            list[i] = i;
        }
        let count: number = Math.floor(Math.random() * 2000);

        // 꼬리 섞기
        while (count--) {
            let temp1: number = Math.floor(Math.random() * this._playerCount);
            let temp2: number = Math.floor(Math.random() * this._playerCount);

            let temp: number = list[temp1];
            list[temp1] = list[temp2];
            list[temp2] = temp;
        }

        let tailCount = 0;
        for (let index in this._playerMap) {
            if (this._playerMap[index] != null) {
                this._playerMap[index].getPlayerData().setTailIndex(list[tailCount]);
                this._playerMap[index].getPlayerData().setTargetTailIndex(list[tailCount] == 0 ? this._playerCount - 1 : list[tailCount] - 1);
                tailCount++;
            }
        }

        for (let index in this._playerMap) {
            if (this._playerMap[index] != null)
                this._playerIndexMap[this._playerMap[index].getPlayerData().getTailIndex()] = this._playerMap[index];
        }
    }

    refreshTargetTail(player: SocketSession, tailIndex: number): void {
        player?.getPlayerData().setTargetTailIndex(tailIndex);
    }

    // getPlayer(tailIndex: number): SocketSession | null {


    //     for (let index = tailIndex - 1; index >= 0; index--) {
    //         let player: SocketSession = this._playerIndexMap[index];
    //         if (player.getPlayerData().getPlayerState() != impelDown.PlayerState.GHOST) {
    //             return player;
    //         }
    //     }
    //     for (let index = this._playerCount; index > tailIndex; index--) {
    //         let player: SocketSession = this._playerIndexMap[index];
    //         if (player.getPlayerData().getPlayerState() != impelDown.PlayerState.GHOST) {
    //             return player;
    //         }
    //     }
    //     return null;
    // }

}