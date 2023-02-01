using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public struct PositionData {
    public Vector3 pos;
    public float rot;
}

public class Util {
    public static PositionData ChangePositionInfo(PosAndRot info) {
        PositionData data = new PositionData {
            pos = new Vector3(info.X, info.Y, 0),
            rot = info.Rot,
        };
        return data;
    }
}