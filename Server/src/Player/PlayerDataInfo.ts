import { impelDown } from "../packet/packet";


export default class PlayerDataInfo {

    private _playerInfo: impelDown.PlayerInfo;
    private _playerId: number;
    private _playerName: string;
    private _tailIndex: number;
    private _playerState: impelDown.PlayerState;

    constructor(playerId: number) {
        this._playerId = playerId;
        this._playerName = "";
        this._playerInfo = new impelDown.PlayerInfo({
            playerId: this._playerId,
            playerName: this._playerName,
            roomIndex: -1,
            playerPosData: new impelDown.PlayerPosData({
                position: new impelDown.Position({
                    x: 0,
                    y: 0
                }),
                scaleX: 1
            }),
            characterIndex: -1,
            roomInIndex: -1
        });
        this._tailIndex = -1;
        this._playerState = impelDown.PlayerState.PLAYER_NONE;
    }

    getPlayerId(): number {
        return this._playerInfo.playerId;
    }
    getPlayerName(): string {
        return this._playerInfo.playerName;
    }

    getPlayerInfo(): impelDown.PlayerInfo {
        return this._playerInfo;
    }

    getPlayerPosition(): impelDown.PlayerPosData {
        return this._playerInfo.playerPosData;
    }

    getTailIndex(): number {
        return this._tailIndex;
    }

    getPlayerState(): impelDown.PlayerState {
        return this._playerState;
    }

    getPlayerData(): impelDown.PlayerInGameData {
        return new impelDown.PlayerInGameData({
            playerId: this._playerId,
            playerNmae: this._playerName,
            playerPosData: this._playerInfo.playerPosData,
            tailIndex: this._tailIndex,
            playerState: this._playerState
        });
    }
    getRoomIndex(): number {
        return this._playerInfo.roomIndex;
    }
    getPlayerCharacterIndex(): number {
        return this._playerInfo.characterIndex;
    }

    setPlayerName(playerName: string): void {
        this._playerInfo.playerName = playerName;
    }
    setPlayerCharacterIndex(characterIndex: number): void {
        this._playerInfo.characterIndex = characterIndex;
    }
    setRoomInIndex(index: number = -1): void {
        this._playerInfo.roomInIndex = index;
    }
    setRoomIndex(index: number = -1): void {
        this._playerInfo.roomIndex = index;
    }
    setPlayerPosData(playerPosData: impelDown.PlayerPosData): void {
        this._playerInfo.playerPosData = playerPosData;
    }
    setTailIndex(tailIndex:number){
        this._tailIndex = tailIndex;
    }

}