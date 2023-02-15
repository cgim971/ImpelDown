using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PositionData {
    public Vector2 pos;
    public float scaleX;
}


public class Util {
    public static PositionData ChangePosition(PositionInfo positionInfo) {
        return new PositionData {
            pos = new Vector2(positionInfo.Position.X, positionInfo.Position.Y),
            scaleX = positionInfo.ScaleX
        };
    }

    public static PositionInfo ChangePosition(PositionData positionData) {
        return new PositionInfo {
            Position = new Position {
                X = positionData.pos.x,
                Y = positionData.pos.y
            },
            ScaleX = positionData.scaleX
        };
    }
}
