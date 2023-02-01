import JobTimer from "../JobTimer";
import { SessionDictionary } from "../SessionManager";
import SocketSession from "../SocketSession";
import { impelDown } from "../packet/packet";
import RoomManager from "./RoomManager";

export default class Room {
    private _hostSocket: SocketSession;
    private _maxPeople: number;
    private _currentPeople: number = 0;
    private _roomIndex: number = 0;

    private _sessionMap: SessionDictionary = {};

    private _playGame: boolean = false;

    private _moveTimer = new JobTimer(40, () => {
        let list: impelDown.PlayerAllData[] = [];
        for (let index in this._sessionMap) {
            if (this._sessionMap[index] != null) {
                let playerData: impelDown.PlayerData = new impelDown.PlayerData({ playerId: this._sessionMap[index].getPlayerId() });
                let posAndRot: impelDown.PosAndRot = this._sessionMap[index].getPosAndRot();
                list.push(new impelDown.PlayerAllData({ playerData, posAndRot }));
            }
        }

        let data = new impelDown.S_Player_List({ playerAllData: list });
        this.broadCastMessage(data.serialize(), impelDown.MSGID.S_PLAYER_LIST);
    });

    constructor(hostSocket: SocketSession, maxPeople: number) {
        this._hostSocket = hostSocket;
        this._maxPeople = maxPeople;
        this._currentPeople = 0;
        this._roomIndex = 0;

        this._sessionMap = {};

        this._playGame = false;
    }

    playGame(mapIndex: number): void {
        if (this._playGame == true) {
            console.log("Error - Already Start!");
            return;
        }
        this._playGame = true;

        let mapData: impelDown.MapData = new impelDown.MapData({ mapIndex: mapIndex });
        let sGameStart: impelDown.S_Game_Start = new impelDown.S_Game_Start({ mapData: mapData, playerAllDatas: this.getPlayerAllDataList() });
        this.broadCastMessage(sGameStart.serialize(), impelDown.MSGID.S_GAME_START);

        this._moveTimer.startTimer();
    }

    setRoomIndex(roomIndex: number): void {
        this._roomIndex = roomIndex;
    }

    joinRoom(player: SocketSession) {
        if (this._maxPeople <= this._currentPeople) {
            console.log("The room is full!");
            RoomManager.Instance.refreshRoomList();
            return;
        }

        for (let index: number = 0; index < this._maxPeople; index++) {
            if (this._sessionMap[index] == null) {
                this._sessionMap[index] = player;
                player.setRoom(true);
                player.setRoomIndex(this._roomIndex);
                this._currentPeople += 1;
                break;
            }
        }

        let roomData: impelDown.RoomData = new impelDown.RoomData({ hostId: this.getHostId(), roomIndex: this._roomIndex, maxPeople: this._maxPeople, currentPeople: this._currentPeople, playerDatas: this.getPlayerList() });
        let sJoinRoom = new impelDown.S_Join_Room({ roomData });
        player.SendData(sJoinRoom.serialize(), impelDown.MSGID.S_JOIN_ROOM);
    }

    exitRoom(player: SocketSession) {
        player.setRoom(false);
        player.setRoomIndex(-1);

        if (this._currentPeople > 1) {
            if (player == this._hostSocket) {
                for (let index: number = 0; index < this._maxPeople; index++) {
                    if (this._sessionMap[index] != player) {
                        this._hostSocket = this._sessionMap[index];
                        break;
                    }
                }
            }

            for (let index: number = 0; index < this._maxPeople; index++) {
                if (this._sessionMap[index] == player) {
                    delete this._sessionMap[index];
                    this._currentPeople -= 1;
                    break;
                }
            }

            let sExitRoom: impelDown.S_Exit_Room = new impelDown.S_Exit_Room();
            player.SendData(sExitRoom.serialize(), impelDown.MSGID.S_EXIT_ROOM);
        }
        else {
            // 방 삭제 - 방에 사람이 없을 때만 삭제
            RoomManager.Instance.deleteRoom(this._roomIndex);
        }

        let sExitRoom = new impelDown.S_Exit_Room();
        player.SendData(sExitRoom.serialize(), impelDown.MSGID.S_EXIT_ROOM);
    }

    broadCastMessage(payload: Uint8Array, msgCode: number): void {
        for (let index in this._sessionMap) {
            this._sessionMap[index].SendData(payload, msgCode);
        }
    }

    getHostId(): number {
        return this._hostSocket.getPlayerId();
    }

    getMaxPeople(): number {
        return this._maxPeople;
    }

    getCurrentPeopleCount(): number {
        return this._currentPeople;
    }

    getPlayerList(): impelDown.PlayerData[] {
        let list: impelDown.PlayerData[] = [];
        for (let index in this._sessionMap) {
            if (this._sessionMap[index] != null) {
                list.push(new impelDown.PlayerData({ playerId: this._sessionMap[index].getPlayerId() }));
            }
        }

        return list;
    }

    getPlayerAllDataList(): impelDown.PlayerAllData[] {
        let list: impelDown.PlayerAllData[] = [];
        for (let index in this._sessionMap) {
            if (this._sessionMap[index] != null) {
                let playerData: impelDown.PlayerData = new impelDown.PlayerData({ playerId: this._sessionMap[index].getPlayerId() , playerCharacterIndex: this._sessionMap[index].getCharacterIndex()});
                let posAndRot: impelDown.PosAndRot = this._sessionMap[index].getPosAndRot();
                list.push(new impelDown.PlayerAllData({ playerData: playerData, posAndRot: posAndRot }));
            }
        }

        return list;
    }
}
