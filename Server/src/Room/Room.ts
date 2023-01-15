import RoomSocketSession from "./RoomSocketSession";
import SessionManager, { SessionDictionary } from "../SessionManager";
import RoomManager from "./RoomManager";
import { impelDown } from "../packet/packet";

export default class Room {
    // 게임 참여 가능 수
    private _manager: RoomSocketSession;

    private _maximumPeople: number = 0;
    private _playerMap: SessionDictionary = {};
    private _count: number = 0;

    constructor(roomIndex: number = -1, maximumPeople: number = 4) {
        this._manager = new RoomSocketSession();

        this._maximumPeople = maximumPeople;
        this._playerMap = {};
        this._count = 0;
    }

    broadCastMessage(payload: Uint8Array, msgCode: number, roomId: number = 0): void {
        for (let index in this._playerMap) {
            this._playerMap[index].SendData(payload, msgCode);
        }
    }

    joinRoom(playerId: number): void {
        if (this._maximumPeople <= this._count + 1) {
            console.log("Aleady Full!");
            return;
        }

        this._count += 1;

        for (let index: number = 0; index < this._count; index++) {
            if (this._playerMap[index] == null) {
                this._playerMap[index] = SessionManager.Instance.getSession(playerId);
                SessionManager.Instance.getSession(playerId).setPlayerRoom(RoomManager.Instance.getRoomIndex(this), index);
                break;
            }
        }

        // let playerInfo: impelDown.PlayerInfo = new impelDown.PlayerInfo({ playerId });
        // let roomInfo: impelDown.RoomInfo = new impelDown.RoomInfo({ playerInfo, maximumPeople: this._maximumPeople, currentPeople: this._count, playerInfos: this.getPlayerList() });
        // let sJoinRoom: impelDown.S_Join_Room = new impelDown.S_Join_Room({ roomInfo });
        // this.broadCastMessage(sJoinRoom.serialize(), impelDown.MSGID.S_JOIN_ROOM);
        // 생성 룸 매니저
        // 그 친구를 통해 내용을 전달 받는다
    }

    exitRoom(playerId: number): void {
        let roomIndex = SessionManager.Instance.getSession(playerId).getPlayerRoomIndex();
        if (this._count == 0) {
            console.log("No Room!");
            return;
        }

        delete this._playerMap[playerId];

        if (--this._count == 0) {
            RoomManager.Instance.deleteRoom(roomIndex);
        }
    }

    startGame(): void {

    }

    endGame(): void {

    }

    getPlayerList(): impelDown.PlayerInfo[] {
        let list: impelDown.PlayerInfo[] = [];

        for (let idx in this._playerMap) {
            let player = this._playerMap[idx];
            list.push(new impelDown.PlayerInfo({ playerId: player.getPlayerId() }));
        }

        return list;
    }

    getRoomMaximumCount(): number {
        return this._maximumPeople;
    }

    getPlayerCount(): number {
        return this._count;
    }

    getRoomInfo(roomId: number, playerId: number): impelDown.RoomInfo {
        let playerInfo: impelDown.PlayerInfo = new impelDown.PlayerInfo({ playerId });
        let roomInfo: impelDown.RoomInfo = new impelDown.RoomInfo({ roomId, playerInfo, maximumPeople: this._maximumPeople, currentPeople: this._count, playerInfos: this.getPlayerList() });
        return roomInfo;
    }
}