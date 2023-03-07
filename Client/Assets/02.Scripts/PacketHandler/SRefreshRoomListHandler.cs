using Google.Protobuf;
using ImpelDown.Proto;

public class SRefreshRoomListHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_RefreshRoomList msg = packet as S_RefreshRoomList;
    }
}
