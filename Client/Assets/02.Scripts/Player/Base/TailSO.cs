using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TailSO : ScriptableObject
{
    public Tail[] allTails;
}

[System.Serializable]
public struct Tail
{
    public ETailName tailName;
    public GameObject[] colorTail;
}