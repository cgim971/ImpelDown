import MapData from "./MapData";
import { impelDown } from "./packet/packet";

interface MapDataDictionary {
    [key: number]: MapData;
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
        this._mapDataMap[1] = this.initLaboratory();
        this._mapDataMap[2] = this.initMoonBase();
        this._mapDataMap[3] = this.initCity();
        this._mapDataMap[4] = this.initOcean();
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

    initLaboratory(): MapData {
        let mapData: MapData = new MapData(1, "Laboratory");
        let position: impelDown.Position = new impelDown.Position();
        mapData.addSpawnPosition(position);
        return mapData;
    }

    initMoonBase(): MapData {
        let mapData: MapData = new MapData(2, "MoonBase");
        let position: impelDown.Position = new impelDown.Position();
        mapData.addSpawnPosition(position);
        return mapData;
    }

    initCity(): MapData {
        let mapData: MapData = new MapData(3, "City");
        let position: impelDown.Position = new impelDown.Position();
        mapData.addSpawnPosition(position);
        return mapData;
    }

    initOcean(): MapData {
        let mapData: MapData = new MapData(4, "Ocean");
        let position: impelDown.Position = new impelDown.Position();
        mapData.addSpawnPosition(position);
        return mapData;
    }

}