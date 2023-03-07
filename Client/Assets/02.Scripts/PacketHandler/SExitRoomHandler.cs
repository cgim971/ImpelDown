using Google.Protobuf;
using ImpelDown.Proto;

public class SExitRoomHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_ExitRoom msg = packet as S_ExitRoom;

    }
}
