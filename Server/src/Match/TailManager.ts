import Room from "./Room/Room";
import SocketSession from "../PlayerData/SocketSession";
import { impelDown } from "../packet/packet";

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
        let count: number = Math.floor(Math.random() * 2000);

        // 꼬리 섞기
        while (count--) {
            let temp1: number = Math.floor(Math.random() * playerCount);
            let temp2: number = Math.floor(Math.random() * playerCount);

            let temp: number = list[temp1];
            list[temp1] = list[temp2];
            list[temp2] = temp;
        }

        let tailCount = 0;
        for (let index in this._playerMap) {
            if (this._playerMap[index] != null) {
                this._playerMap[index].getPlayerData().setTailIndex(list[tailCount]);
                this._playerMap[index].getPlayerData().setTargetTailIndex(list[tailCount] == 0 ? playerCount - 1 : list[tailCount] - 1);
                tailCount++;
            }
        }
    }

    refreshTargetTail(player: SocketSession, tailIndex: number): void {
        player?.getPlayerData().setTargetTailIndex(tailIndex);
    }

    getPlayer(tailIndex: number): SocketSession | null {
        for (let index in this._playerMap) {
            let player: SocketSession = this._playerMap[index];
            if (player.getPlayerData().getTailIndex() < tailIndex && tailIndex < player.getPlayerData().getTargetTailIndex() && player.getPlayerData().getPlayerState() != impelDown.PlayerState.GHOST) {
                return player;
            }
        }
        return null;
    }

}