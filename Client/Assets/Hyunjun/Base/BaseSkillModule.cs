using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Interfaces;

/// <summary>
/// ��ų �߻� Ŭ����
/// </summary>
public abstract class BaseSkillModule : MonoBehaviour
{
    protected Player player;
    //��ų�� ����� �� �ֳ� ����
    protected bool _skillable = true;
    public bool Skillable { get => _skillable; set => _skillable = value; }
    //baseinput���� ������ �������̽�
    protected IInputProvider _inputProvider;

    protected virtual void Start()
    {
        player = GetComponent<Player>();
        _inputProvider = GetComponent<IInputProvider>();
    }

    //��� ��ų�� �ʿ��� ��Ÿ��
    protected IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(player.PlayerDataSO.activeCoolTime);
        Skillable = true;
    }

    //�ڽĵ��� �����ؾ��� ��ų
    protected abstract void Skill();
}
