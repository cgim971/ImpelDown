using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatSpeedModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val, float runtime)
    {
        character.GetComponent<BasePlayer>().MoveModule.SpeedItem(val, runtime);
    }
}