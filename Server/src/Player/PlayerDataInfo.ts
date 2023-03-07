import { impelDown } from "../packet/packet";


export default class PlayerDataInfo {

    private _playerInfo: impelDown.PlayerInfo;
    private _playerPosData: impelDown.PlayerPosData;
    private _tailIndex: number;
    private _playerState: impelDown.PlayerState;

    constructor(playerId: number) {
        {
            this._playerInfo = new impelDown.PlayerInfo({
                playerId: playerId,
                roomInfo: new impelDown.RoomInfo(),
                characterIndex: -1
            });
            this._playerPosData = new impelDown.PlayerPosData({
                position: new impelDown.Position({
                    x: 0,
                    y: 0
                }),
                flipX: false
            });
            this._tailIndex = -1;
            this._playerState = impelDown.PlayerState.PLAYER_NONE;
        }
    }

    getPlayerId(): number {
        return this._playerInfo.playerId;
    }

    getPlayerInfo(): impelDown.PlayerInfo {
        return this._playerInfo;
    }

    getPlayerPosition(): impelDown.PlayerPosData {
        return this._playerPosData;
    }

    getTailIndex(): number {
        return this._tailIndex;
    }

    getPlayerState(): impelDown.PlayerState {
        return this._playerState;
    }

    getPlayerData(): impelDown.PlayerData {
        return new impelDown.PlayerData({
            playerInfo: this._playerInfo,
            playerPosData: this._playerPosData,
            tailIndex: this._tailIndex,
            playerState: this._playerState
        });
    }
}