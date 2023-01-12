using Google.Protobuf;
using ImpelDown.Proto;

public interface IPacketHandler {
    public void Process(IMessage packet);
}
