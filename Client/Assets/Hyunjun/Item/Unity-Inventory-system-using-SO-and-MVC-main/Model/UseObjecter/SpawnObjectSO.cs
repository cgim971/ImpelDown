using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpawnObjectSO : CharacterUseObjectSO
{
    public override void UseObject(GameObject character, GameObject useItem, float val)
    {
        Debug.Log(character.transform.position);
        GameObject item = Instantiate(useItem, character.transform.position, Quaternion.identity, null);
    }
}
