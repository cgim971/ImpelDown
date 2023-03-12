using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PositionData {
    public Vector2 pos;
    public float scaleX;
}


public class Util {
    public static PositionData ChangePosition(PlayerPosData playerPosData) {
        return new PositionData {
            pos = new Vector2(playerPosData.Position.X, playerPosData.Position.Y),
            scaleX = playerPosData.ScaleX
        };
    }

    public static PlayerPosData ChangePosition(PositionData positionData) {
        return new PlayerPosData {
            Position = new Position {
                X = positionData.pos.x,
                Y = positionData.pos.y
            },
            ScaleX = positionData.scaleX
        };
    }
}
