import Express, { Application } from 'express';
import { IncomingMessage } from 'http';
import WS, { RawData } from 'ws';
import { impelDown } from './packet/packet';
import PacketManager from './PacketManager';
import RoomManager from './Room/RoomManager';
import SessionManager from './SessionManager';
import SocketSession from './SocketSession';

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

let playerId: number = 0;
socketServer.on("connection", (soc: WS, req: IncomingMessage) => {
    const id: number = playerId;

    let session: SocketSession = new SocketSession(soc, id, () => {
        
    });

    SessionManager.Instance.addSession(session, id);
    let playerInfo: impelDown.PlayerInfo = new impelDown.PlayerInfo({ playerId: id});
    let msg: impelDown.S_Init = new impelDown.S_Init({ playerInfo: playerInfo });
    session.SendData(msg.serialize(), impelDown.MSGID.S_INIT);
    playerId += 1;

    soc.on("message", (data: RawData, isBinary: boolean) => {
        if (isBinary == true) {
            session.receiveMsg(data);
        }
    });
});