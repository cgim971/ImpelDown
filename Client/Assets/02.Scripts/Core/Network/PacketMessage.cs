using Google.Protobuf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketMessage {

    #region Property
    public ushort Id => _id;
    public IMessage Message => _message;

    #endregion

    private ushort _id;
    private IMessage _message;

    public PacketMessage(ushort id, IMessage message) {
        _id = id;
        _message = message;
    }
}
