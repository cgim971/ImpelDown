import { impelDown } from "../../packet/packet";
import MapData from "../MapData";

interface MapDataDictionary {
    [key: number]:MapData;
}

export default class MapDataManager {

    static Instance: MapDataManager;
    private _mapDataMap: MapDataDictionary;

    constructor() {
        this._mapDataMap = [];
        this.init();
    }

    getMapData(mapIndex: number): MapData {
        return this._mapDataMap[mapIndex];
    }

    init(): void {
        this._mapDataMap[0] = this.initForest();
    }

    initForest(): MapData {
        let mapData: MapData = new MapData(0, "Forest");
        let position: impelDown.Position;

        position = new impelDown.Position({ x: -5, y: 5 });
        mapData.addSpawnPosition(position);

        position = new impelDown.Position({ x: 0, y: 5 });
        mapData.addSpawnPosition(position);

        position = new impelDown.Position({ x: 5, y: 5 });
        mapData.addSpawnPosition(position);

        position = new impelDown.Position({ x: -5, y: 0 });
        mapData.addSpawnPosition(position);

        position = new impelDown.Position({ x: 5, y: 0 });
        mapData.addSpawnPosition(position);

        position = new impelDown.Position({ x: -5, y: -5 });
        mapData.addSpawnPosition(position);

        position = new impelDown.Position({ x: 0, y: -5 });
        mapData.addSpawnPosition(position);

        position = new impelDown.Position({ x: 5, y: -5 });
        mapData.addSpawnPosition(position);
        return mapData;
    }

  

}