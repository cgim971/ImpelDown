import { impelDown } from "./packet/packet";

export interface PositionDictionary {
    [key: number]: impelDown.Position;
}

export default class MapData {
    private _mapIndex: number;
    private _mapName: string;
    private _positionMap: PositionDictionary;
    private _positionCount: number;

    constructor(mapIndex: number, mapName: string) {
        this._mapIndex = mapIndex;
        this._mapName = mapName;

        this._positionMap = [];
        this._positionCount = 0;
    }

    getMapIndex(): number {
        return this._mapIndex;
    }

    addSpawnPosition(position: impelDown.Position) {
        this._positionMap[this._positionCount++] = position;
    }

    getPositionCount(): number {
        return this._positionCount;
    }
    getPosition(id: number): impelDown.Position {
        return this._positionMap[id];
    }
}