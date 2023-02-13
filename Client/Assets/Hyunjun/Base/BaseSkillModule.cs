using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Interfaces;

/// <summary>
/// 스킬 추상 클래스
/// </summary>
public abstract class BaseSkillModule : MonoBehaviour
{
    protected Player player;
    //스킬을 사용할 수 있나 없나
    protected bool _skillable = true;
    public bool Skillable { get => _skillable; set => _skillable = value; }
    //baseinput에서 구현한 인터페이스
    protected IInputProvider _inputProvider;

    protected virtual void Start()
    {
        player = GetComponent<Player>();
        _inputProvider = GetComponent<IInputProvider>();
    }

    //모든 스킬이 필요한 쿨타임
    protected IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(player._PlayerDataSO.activeCoolTime);
        Skillable = true;
    }

    //자식들이 구현해야할 스킬
    protected abstract void Skill();
}
