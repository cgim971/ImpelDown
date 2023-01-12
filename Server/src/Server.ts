import Express, { Application } from 'express';
import { IncomingMessage } from 'http';
import WS, { RawData } from 'ws';
import PacketManager from './PacketManager';
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

socketServer.on("connection", (soc: WS, req: IncomingMessage) => {
    console.log("A");

    let session:SocketSession = new SocketSession(soc, 0, ()=>{

    });

    SessionManager.Instance.addSession(session, 0);
    console.log(SessionManager.Instance.count);
});