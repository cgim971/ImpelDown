using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkillModule : MonoBehaviour {
    #region Property
    public BasePlayer Player => _player;
    public bool IsSkillable {
        get => _isSkillable;
        set => _isSkillable = value;
    }
    #endregion

    protected BasePlayer _player;

    protected bool _isSkillable = true;

    public abstract void Init();

    protected virtual IEnumerator CoolTime() {
        yield return new WaitForSeconds(_player.PlayerDataSO.ActiveCoolTime);
        _isSkillable = true;
    }

    // ��ų Q
    public abstract void Skill();

    protected bool Skillable() {
        if (Input.GetKeyDown(KeyCode.Q) == false || _isSkillable == false)
            return false; 

        if (_player.PlayerState == ImpelDown.Proto.PlayerState.Ghost)
            return false;

        return true;
    }
}
