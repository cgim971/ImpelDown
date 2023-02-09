using Google.Protobuf;
using ImpelDown.Proto;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        _onRecv.Add((ushort)MSGID.SInit, MakePacket<S_Init>);
        _handlers.Add((ushort)MSGID.SInit, new SInitHandler());

        _onRecv.Add((ushort)MSGID.SRefreshRoomlist, MakePacket<S_Refresh_RoomList>);
        _handlers.Add((ushort)MSGID.SRefreshRoomlist, new SRefreshRoomListHandler());

        _onRecv.Add((ushort)MSGID.SJoinRoom, MakePacket<S_Join_Room>);
        _handlers.Add((ushort)MSGID.SJoinRoom, new SJoinRoomHandler());

        _onRecv.Add((ushort)MSGID.SExitRoom, MakePacket<S_Exit_Room>);
        _handlers.Add((ushort)MSGID.SExitRoom, new SExitRoomHandler());
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
