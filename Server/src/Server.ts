import Express, { Application } from 'express';
import { IncomingMessage } from 'http';
import WS, { RawData } from 'ws';
import { impelDown } from './packet/packet';
import PacketManager from './Game/Managers/PacketManager';
import SessionManager from './Game/Managers/SessionManager';
import PlayerSession from './Player/PlayerSocket';
import RoomManager from './Game/Room/RoomManager';
import MatchManager from './Game/Managers/MatchManager';
import MapDataManager from './Game/Managers/MapDataManager';

const App: Application = Express();

const httpServer = App.listen(8181, () => {
    console.log("Server is running on 8181 port");
});

const socketServer: WS.Server = new WS.Server({
    server: httpServer,
}, () => {
    console.log("Socket server is running on 8181 port");
});

PacketManager.Instance = new PacketManager();
SessionManager.Instance = new SessionManager();
RoomManager.Instance = new RoomManager();
MatchManager.Instance = new MatchManager();
MapDataManager.Instance = new MapDataManager();

let playerId: number = 0;
socketServer.on("connection", (soc: WS, req: IncomingMessage) => {
    const id: number = playerId;

    let session: PlayerSession = new PlayerSession(soc, id, () => {
        SessionManager.Instance.removeSession(id);
    });

    SessionManager.Instance.addSession(session, id);
    let playerInfo: impelDown.PlayerInfo = new impelDown.PlayerInfo({ playerId: id });
    let msg: impelDown.S_Init = new impelDown.S_Init({ playerInfo: playerInfo });
    session.SendData(msg.serialize(), impelDown.MSGID.S_INIT);
    playerId += 1;

    soc.on("message", (data: RawData, isBinary: boolean) => {
        if (isBinary == true) {
            session.receiveMsg(data);
        }
    });
});