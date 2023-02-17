import MapData from "../MapData";
import MapDataManager from "../MapDataManager";
import SocketSession from "../PlayerData/SocketSession";
import { impelDown } from "../packet/packet";
import Room from "./Room/Room";

interface PlayerDictionary {
    [key: number]: SocketSession;
}

export default class MapManager {

    private _room: Room;
    private _mapData: MapData;
    private _playerMap: PlayerDictionary;

    constructor(room: Room) {
        this._room = room;
        this._mapData = MapDataManager.Instance.getMapData(room.getMapIndex());
        this._playerMap = [];
    }

    init() {
        this._mapData = MapDataManager.Instance.getMapData(this._room.getMapIndex());
        this._playerMap = this._room.getPlayerMap();
        let positionCount: number = this._mapData.getPositionCount();

        let list: number[] = [];
        for (let i = 0; i < positionCount; i++) {
            list[i] = i;
        }
        let count: number = Math.floor(Math.random() * 2000);

        while (count--) {
            let temp1: number = Math.floor(Math.random() * positionCount);
            let temp2: number = Math.floor(Math.random() * positionCount);

            let temp: number = list[temp1];
            list[temp1] = list[temp2];
            list[temp2] = temp;
        }

        let positionCnt = 0;
        for (let index in this._playerMap) {
            if (this._playerMap[index] != null) {
                let position: impelDown.Position = this._mapData.getPosition(list[positionCnt++]);
                this._playerMap[index].getPlayerData().setPosition(position);
            }
        }
    }


}