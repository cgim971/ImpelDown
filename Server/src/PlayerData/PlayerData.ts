import { impelDown } from "../packet/packet";

export default class PlayerData {
    private _playerId: number;
    private _playerName: string;
    private _characterIndex: number;
    private _positionInfo: impelDown.PositionInfo;
    private _tailIndex: number;
    private _targetTailIndex: number;
    private _playerState: impelDown.PlayerState;    // 1 이면 한 번도 잡히지 않음, 2면 잡히고 30초, 3이면 유령

    constructor(playerId: number, playerName: string) {
        this._playerId = playerId;
        this._playerName = playerName;
        this._positionInfo = new impelDown.PositionInfo({ position: new impelDown.Position({ x: 0, y: 0 }), scaleX: 1 });
        this._characterIndex = -1;
        this._tailIndex = -1;
        this._targetTailIndex = -1;
        this._playerState = impelDown.PlayerState.NONE;
    }


    getPlayerInfo(): impelDown.PlayerInfo {
        return new impelDown.PlayerInfo({
            playerId: this._playerId,
            playerName: this._playerName,
            characterIndex: this._characterIndex,
            positionInfo: this._positionInfo,
            tailIndex: this._tailIndex,
            targetTailIndex: this._targetTailIndex,
            playerState: this._playerState
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

    setPosition(position: impelDown.Position) {
        this._positionInfo.position = position;
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

    getTargetTailIndex(): number {
        return this._targetTailIndex;
    }

    setTargetTailIndex(targetTailIndex: number) {
        this._targetTailIndex = targetTailIndex;
    }

    setPlayerState(playerState: impelDown.PlayerState = impelDown.PlayerState.ALIVE) {
        this._playerState = playerState;
    }

    getPlayerState(): impelDown.PlayerState {
        return this._playerState;
    }
}