import SocketSession from "../PlayerData/SocketSession";
import RoomManager from "./Room/RoomManager";

export default class MatchManager {
    static Instance: MatchManager;

    // 매치를 찾는다.   
    matchMaking(player: SocketSession): void {
        // 랜덤으로 찾기
        RoomManager.Instance.matchMaking(player);
    }
}