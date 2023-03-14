using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseInputModule : MonoBehaviour {
    #region Property
    public BasePlayer Player => _player;
    public bool Moveable { get { return _moveable; } set { _moveable = value; } }
    public bool Skillable { get { return _skillable; } set { _skillable = value; } }
    public bool Catchable { get { return _catchable; } set { _catchable = value; } }
    public bool Itemable { get { return _itemable; } set { _itemable = value; } }
    #endregion

    protected bool _moveable = true;
    protected bool _skillable = true;
    protected bool _catchable = true;
    protected bool _itemable = true;

    protected BasePlayer _player;

    public abstract void Init();

    public void InputMove() {
        _player.MoveModule.Move();
    }

    public void InputCatch() {
        if (Catchable)
            _player.CatchModule.Catch();
    }

    public void InputSkill() {
        if (Skillable)
            _player.SkillModule.Skill();
    }

    public void InputItem()
    {
        if (Itemable)
            _player.ItemModule.UseItem();
    }

    public void ReverseAble()
    {
        Debug.Log("Reverse");
        _moveable = _moveable ? false : true;
        _skillable = _skillable ? false : true;
        _catchable = _catchable ? false : true;
        _itemable = _itemable ? false : true;
    }
}
