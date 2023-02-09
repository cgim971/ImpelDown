import { impelDown } from "../packet/packet";

export default class PlayerData {
    private _playerId: number;
    private _position: impelDown.Position;

    constructor(playerId: number) {
        this._playerId = playerId;
        this._position = new impelDown.Position();
    }

    getPlayerId(): number {
        return this._playerId;
    }

    getPosition(): impelDown.Position {
        return this._position;
    }

    setPosition(position: impelDown.Position): void {
        this._position = position;
    }
}