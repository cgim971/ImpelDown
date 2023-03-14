using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterUseObjectSO : ScriptableObject
{
    public abstract void UseObject(GameObject character, GameObject useItem, float val);
}
