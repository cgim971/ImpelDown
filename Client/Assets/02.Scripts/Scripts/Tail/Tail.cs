using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//꼬리 추상 클래스 아직 구현은 안했는데 나중에 패시브 능력 넣어둘 예정
public abstract class Tail : MonoBehaviour
{
    
}

[System.Serializable]
public enum TailState
{
    None,
    Carret
}
