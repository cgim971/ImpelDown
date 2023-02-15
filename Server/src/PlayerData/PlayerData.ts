import { impelDown } from "../packet/packet";

export default class PlayerData {
    private _playerId: number;
    private _playerName: string;
    private _characterIndex: number;
    private _positionInfo: impelDown.PositionInfo;
    private _tailIndex: number;

    constructor(playerId: number, playerName: string) {
        this._playerId = playerId;
        this._playerName = playerName;
        this._positionInfo = new impelDown.PositionInfo({ position: new impelDown.Position({ x: 0, y: 0 }), scaleX: 1 });
        this._characterIndex = -1;
        this._tailIndex = -1;
    }


    getPlayerInfo(): impelDown.PlayerInfo {
        return new impelDown.PlayerInfo({
            playerId: this._playerId,
            playerName: this._playerName,
            characterIndex: this._characterIndex,
            positionInfo: this._positionInfo,
            tailIndex: this._tailIndex
        });
    }


    getPlayerId(): number {
        return this._playerId;
    }

    getPositionInfo(): impelDown.PositionInfo {
        return this._positionInfo;
    }

    setPositionInfo(positionInfo: impelDown.PositionInfo): void {
        this._positionInfo = positionInfo;
    }

    getCharacterIndex(): number {
        return this._characterIndex;
    }

    setCharacterIndex(characterIndex: number = -1): void {
        this._characterIndex = characterIndex;
    }

    getTailIndex(): number {
        return this._tailIndex;
    }

    setTailIndex(tailIndex: number) {
        this._tailIndex = tailIndex;
    }
}