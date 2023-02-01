using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 여기도 해적 인풋 클래스랑 동일
/// </summary>
public class PirateMoveModule : BaseMoveModule
{
    //이건 baseplayer를 pirtateplayer로 바꿔주는 거 <- 지금은 쓸데 없어서 지워도 됨 예시로 넣어둠
    PiratePlayer Player => player as PiratePlayer;

    protected override void Start()
    {
        base.Start();
    }
}
