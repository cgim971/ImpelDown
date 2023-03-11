using Google.Protobuf;
using ImpelDown.Proto;

public class SRefreshRoomHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_RefreshRoom msg = packet as S_RefreshRoom;
        MatchManager.Instance.RoomInPanelUI.RefreshRoomData(msg.RoomInfo);
    }
}
