using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketManager : MonoBehaviour {

    private Dictionary<ushort, Action<ArraySegment<byte>, ushort>> _onRecv = new Dictionary<ushort, Action<ArraySegment<byte>, ushort>>();
    private Dictionary<ushort, IPacketHandler> _handlers = new Dictionary<ushort, IPacketHandler>();

    public PacketManager() {
        _onRecv = new Dictionary<ushort, Action<ArraySegment<byte>, ushort>>();
        _handlers = new Dictionary<ushort, IPacketHandler>();

        Register();
    }

    private void Register() {

    }

    public IPacketHandler GetPacketHandler(ushort id) {
        IPacketHandler handler = null;
        if (_handlers.TryGetValue(id, out handler)) {
            return handler;
        }

        return null;
    }

    public int OnRecvPacket(ArraySegment<byte> buffer) {
        ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
        ushort code = BitConverter.ToUInt16(buffer.Array, buffer.Offset + 2);

        if (_onRecv.ContainsKey(code)) {
            _onRecv[code].Invoke(buffer, code);
            return size;
        }
        else {
            Debug.LogError($"There is no packet handler for this packet : {code}, {size}");
            return 0;
        }
    }

    private void MakePacket<T>(ArraySegment<byte> buffer, ushort id) where T : IMessage, new() {
        T packet = new T();
        packet.MergeFrom(buffer.Array, buffer.Offset + 4, buffer.Count - 4);

        PacketQueue.Instance.Push(id, packet);
    }

}
