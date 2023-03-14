using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkillModule : MonoBehaviour {
    #region Property
    public BasePlayer Player => _player;
    #endregion

    protected BasePlayer _player;

    protected bool _isSkillable = true;

    public abstract void Init();

    protected virtual IEnumerator CoolTime() {
        yield return new WaitForSeconds(_player.PlayerDataSO.ActiveCoolTime);
        Player.InputModule.Skillable = true;
    }

    // ½ºÅ³ Q
    public abstract void Skill();

    protected bool Skillable() {
        if (Input.GetKeyDown(KeyCode.Q) == false || Player.InputModule.Skillable == false)
            return false;

        if (!Input.GetMouseButtonDown(0))
            return false;

        if (_player.PlayerState == ImpelDown.Proto.PlayerState.Ghost)
            return false;

        return true;
    }
}
