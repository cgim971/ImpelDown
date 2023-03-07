import { impelDown } from "../packet/packet";

export default class RoomDataInfo {
    private _roomState: impelDown.RoomState;
    private _hostId: number;
    private _roomIndex: number;
    private _mapIndex: number;
    private _currentPeople: number;
    private _maxPeople: number;

    constructor() {
        this._roomState = impelDown.RoomState.ROOM_NONE;
        this._hostId = -1;
        this._roomIndex = -1;
        this._mapIndex = -1;
        this._currentPeople = -1;
        this._maxPeople = -1;
    }

    getRoomState(): impelDown.RoomState {
        return this._roomState;
    }

    getHostId(): number {
        return this._hostId;
    }

    getRoomIndex(): number {
        return this._roomIndex;
    }

    setRoomIndex(roomIndex:number):void{
        this._roomIndex = roomIndex;
    }

    getMapIndex(): number {
        return this._mapIndex;
    }

    getCurrentPeople(): number {
        return this._currentPeople;
    }

    getMaxPeople(): number {
        return this._maxPeople;
    }

    getRoomInfo():impelDown.RoomInfo {
        return new impelDown.RoomInfo({ 
            roomState : this._roomState,
            hostId:this._hostId,
            roomIndex:this._roomIndex,
            mapIndex:this._mapIndex,
            currentPeople:this._currentPeople,
            maxPeople:this._maxPeople
        });
    }
}