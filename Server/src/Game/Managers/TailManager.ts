import PlayerSocket from "../../Player/PlayerSocket";
import SessionManager from "./SessionManager";

interface TailDictionary {
    [key: number]: number;
}
export default class TailManager {

    private _array: number[] = [];
    private _tailMap: TailDictionary = [];
    private _index: number = 0;

    constructor() {
        this._array = [];
        this._tailMap = [];
        this._index = 0;
    }

    init(currentPeople: number) {
        for (let i: number = 0; i < currentPeople; i++) {
            this._array.push(i);
        }

        this.shuffle();
    }

    shuffle() {
        for (let i = this._array.length - 1; i > 0; i--) {
            let j = Math.floor(Math.random() * (i + 1));
            [this._array[i], this._array[j]] = [this._array[j], this._array[i]];
        }
    }

    setArray(player: PlayerSocket) {
        this._tailMap[player.getPlayerId()] = this._array[this._index];
        player.getPlayerDataInfo().setTailIndex(this._array[this._index]);
        
        this._index++;
    }
}