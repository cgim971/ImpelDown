using Google.Protobuf;
using ImpelDown.Proto;

public class SCreateRoomHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_CreateRoom msg = packet as S_CreateRoom;


    }
}
