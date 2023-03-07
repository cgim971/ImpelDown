using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatSpeedModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        //Player speed = character.GetComponent<Player>();
        //if (speed != null)
        //{
        //    speed.AddSpeed();
        //}
    }
}
