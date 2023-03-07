import JobTimer from "../../JobTimer";
import PlayerSocket from "../../Player/PlayerSocket";
import { SessionDictionary } from "../Managers/SessionManager";
import { impelDown } from "../../packet/packet";
import MapManager from "../Managers/MapManager";
import TailManager from "../Managers/TailManager";

export default class Room {
    private _hostSocket: PlayerSocket;
    private _roomInfo: impelDown.RoomInfo;

    // 방에 들어온 플레이어 정보 저장
    private _playerMap: SessionDictionary = {};
    private _tailManager: TailManager = new TailManager();
    private _mapManager: MapManager = new MapManager();


    constructor(hostSocket: PlayerSocket, roomIndex: number, maxPeople: number) {
        this._hostSocket = hostSocket;

        this._roomInfo = new impelDown.RoomInfo({
            roomState: impelDown.RoomState.LOBBY,
            hostId: hostSocket.getPlayerId(),
            roomIndex: roomIndex,
            mapIndex: 0,
            currentPeople: 0,
            maxPeople: maxPeople
        });

        this.joinRoom(hostSocket);
    }


    startGame(): void {
        if (this._roomInfo.roomState == impelDown.RoomState.GAME) {
            return;
        }

        // 게임 시작 시 실행
        this.gameTimer.startTimer();
    }

    private gameTimer: JobTimer = new JobTimer(40, () => {
        // 게임 중에 계속 실행
    });


    joinRoom(player: PlayerSocket): void {
        if (this._roomInfo.roomState == impelDown.RoomState.GAME) return;
        if(this.isEmpty() == false) return;
 
        let playerIndex: number = 0;
        while (this._playerMap[playerIndex] != null) {
            playerIndex++;
        }

        this._playerMap[playerIndex] = player;
        this._roomInfo.currentPeople += 1;
        player.getRoomDataInfo().setRoomIndex(this._roomInfo.roomIndex);
    }


    getRoomInfo(): impelDown.RoomInfo {
        return this._roomInfo;
    }

    isEmpty() : boolean{
        return this._roomInfo.maxPeople >  this._roomInfo.currentPeople;
    }
    
}