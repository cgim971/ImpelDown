using Google.Protobuf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketQueue {

    #region Property
    public static PacketQueue Instance => _instance;
    public int Count => _packetQueue.Count;

    #endregion


    private static PacketQueue _instance = new PacketQueue();


    private Queue<PacketMessage> _packetQueue = new Queue<PacketMessage>();
    private object _lock = new object();

    public void Push(ushort id, IMessage packet) {
        lock (_lock) {
            _packetQueue.Enqueue(new PacketMessage(id, packet));
        }
    }

    public PacketMessage Pop() {
        lock (_lock) {
            if (Count == 0)
                return null;

            return _packetQueue.Dequeue();
        }
    }

    public List<PacketMessage> PopAll() {
        List<PacketMessage> list = new List<PacketMessage>();

        lock (_lock) {
            while(Count > 0) {
                list.Add(_packetQueue.Dequeue());
            }
        }

        return list;
    }

}
