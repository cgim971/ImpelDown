import PlayerSocket from "../../Player/PlayerSocket";
import { impelDown } from "../../packet/packet";
import MapData from "../MapData";
import Room from "../Room/Room";
import MapDataManager from "./MapDataManager";

export default class MapManager {

    private _array: number[] = [];
    private _index: number = 0;
    private _room: Room;
    private _mapData: MapData;

    constructor(room: Room) {
        this._array = [];
        this._room = room;
        this._index = 0;
        this._mapData = MapDataManager.Instance.getMapData(this._room.getRoomInfo().roomIndex);
    }

    init() {
        let mapData: MapData = MapDataManager.Instance.getMapData(this._room.getRoomInfo().roomIndex);
        let positionCount: number = mapData.getPositionCount();
        for (let i = 0; i < positionCount; i++) {
            this._array[i] = i;
        }
        this.shuffle();

        for(let i = 0; i< positionCount; i++)
        {
            console.log(this._array[i]);
        }
    }

    shuffle() {
        for (let i = this._array.length - 1; i > 0; i--) {
            let j = Math.floor(Math.random() * (i + 1));
            [this._array[i], this._array[j]] = [this._array[j], this._array[i]];
        }
    }

    setPosition(player: PlayerSocket) {
        let playerPosData: impelDown.PlayerPosData = new impelDown.PlayerPosData({
            position: this._mapData.getPosition(this._array[this._index++]),
            scaleX: 1
        });
        player.getPlayerDataInfo().setPlayerPosData(playerPosData);
    }
}