import { impelDown } from "../packet/packet";

export default class PlayerData {
    private _playerId: number;
    private _playerName : string;
    private _characterIndex: number;
    private _position: impelDown.Position;

    constructor(playerId: number, playerName:string) {
        this._playerId = playerId;
        this._playerName = playerName;
        this._position = new impelDown.Position();
        this._characterIndex = -1;
    }

    
    getPlayerInfo(): impelDown.PlayerInfo{
        return new impelDown.PlayerInfo({
            playerId : this._playerId,
            playerName: this._playerName,
            characterIndex: this._characterIndex,
            position : this._position
        });
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

    getCharacterIndex(): number {
        return this._characterIndex;
    }

    setCharacterIndex(characterIndex: number = -1): void {
        this._characterIndex = characterIndex;
    }
}