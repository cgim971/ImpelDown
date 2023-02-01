using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 해적 데이터 SO faintTime은 나중에 닿은 플레이어 기절할 때 쓰기 위해 넣어둠 
/// </summary>
[CreateAssetMenu(fileName = "PirateSO", menuName = "SO/Player/Pirate")]
public class PirateDataSO : BasePlayerDataSO
{
    public float faintTime;
}
