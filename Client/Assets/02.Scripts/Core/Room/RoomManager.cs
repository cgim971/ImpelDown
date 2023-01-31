using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    #region Property
    public static RoomManager Instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<RoomManager>();

            return _instance;
        }
        set => _instance = value;
    }

    public RoomData RoomData { get => _roomData; set => _roomData = value; }

    #endregion
    private static RoomManager _instance = null;

    private RoomData _roomData = null;

    public void Init() { }


}
