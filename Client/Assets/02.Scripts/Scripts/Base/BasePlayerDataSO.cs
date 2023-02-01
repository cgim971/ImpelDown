using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 기본 SO
/// </summary>
public class BasePlayerDataSO : ScriptableObject
{
    //기본 이동 속도
    public float speed = 5f;
    //스킬 쿨타임
    public float activeCoolTime = 5f;
}
