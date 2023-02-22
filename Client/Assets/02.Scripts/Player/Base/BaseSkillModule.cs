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

    protected bool _isSkillable;

    public abstract void Init();

    protected virtual IEnumerator CoolTime() {
        yield return new WaitForSeconds(_player.PlayerDataSO.ActiveCoolTime);
        _isSkillable = true;
    }

    public abstract void Skill();
}
