using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Interfaces;

/// <summary>
/// ���� ��ų Ŭ����
/// </summary>
public class PirateSkillModule : BaseSkillModule
{
    //base �÷��̾ ���� �÷��̾�� �ٲٱ� �۾�
    PiratePlayer _Player => player as PiratePlayer;

    //���� ��ų�� ����
    public Transform hook;
    public LineRenderer line;
    //���콺 ����
    Vector2 mouseDir;
    //���� ������
    public bool isHookActive;
    //��Ÿ� ���ΰ�
    public bool isLineMax;
    //�÷��̾ ��ҳ�?
    public bool isAttach;

    protected override void Start()
    {
        base.Start();
        player._InputModule.OnSkillEvent.AddListener(Skill);
        //���� ��ų�� ���� ����
        line.positionCount = 2;
        line.endWidth = line.startWidth = 0.05f;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);
        line.useWorldSpace = true;
    }

    private void Update()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);

        //���� �������� ����üũ
        if (hook.gameObject.activeSelf && !isHookActive)
        {
            hook.position = transform.position;
            mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            isHookActive = true;
        }
        //���� ������ ��Ÿ� ���� �ƴϸ� �÷��̾�� ���� �ʾҴٸ� ���콺 �������� �̵�
        if (isHookActive && (!isLineMax && !isAttach))
        {
            hook.Translate(mouseDir.normalized * Time.deltaTime * 15f);

            if (Vector2.Distance(transform.position, hook.position) >= 5)
            {
                isLineMax = true;
            }
        }
        //�÷��̾�� ��ų� ��Ÿ� ���̶�� �ǵ��ƿ���
        else if (isHookActive && (isLineMax || isAttach))
        {
            hook.position = Vector2.MoveTowards(hook.position, transform.position, Time.deltaTime * 15);
            if (Vector2.Distance(transform.position, hook.position) < 0.1f)
            {
                isHookActive = false;
                isLineMax = false;
                isAttach = false;
                hook.gameObject.SetActive(false);
                _Player.MoveModule.Moveable = true;
            }
        }
    }

    protected override void Skill()
    {
        //��ų�� �����°� ��ų�� �����Ѱ� üũ
        if (_skillable == false || !_inputProvider.GetActionPressed(InputAction.Skill))
            return;
        //��ų�� �� �� �� ������ ��
        _Player.MoveModule.Moveable = false;
        //��ų �ٷ� �� ���� ����
        Skillable = false;
        //��ų ���� �ѱ�
        hook.gameObject.SetActive(true);
        //��Ÿ��
        StartCoroutine(CoolTime());
    }
}
