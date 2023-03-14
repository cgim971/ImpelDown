using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseInputModule : MonoBehaviour {
    #region Property
    public BasePlayer Player => _player;
    #endregion

    protected BasePlayer _player;

    public abstract void Init();

    public void InputMove() {
        _player.MoveModule.Move();
    }

    public void InputCatch() {
        _player.CatchModule.Catch();
    }

    public void InputSkill() {
        _player.SkillModule.Skill();
    }

    public void InputItem()
    {
        _player.ItemModule.UseItem();
    }
}
