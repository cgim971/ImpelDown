using Google.Protobuf;
using ImpelDown.Proto;

public class SSetRoomHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_SetRoom msg = packet as S_SetRoom;
    }
}
