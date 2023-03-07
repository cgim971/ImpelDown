using Google.Protobuf;
using ImpelDown.Proto;

public class SJoinRoomHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_JoinRoom msg = packet as S_JoinRoom;
    }
}
