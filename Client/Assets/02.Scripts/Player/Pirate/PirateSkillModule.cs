using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateSkillModule : BaseSkillModule {
    public PiratePlayer PiratePlayer => _player as PiratePlayer;

    //���� ��ų�� ����
    [SerializeField]
    private Transform hook;
    [SerializeField]
    private LineRenderer line;
    //���콺 ����
    Vector2 mouseDir;
    //���� ������
    public bool isHookActive;
    //��Ÿ� ���ΰ�
    public bool isLineMax;
    //�÷��̾ ��ҳ�?
    public bool isAttach;

    public override void Init() {
        _player = GetComponent<PiratePlayer>();
        hook = transform.Find("Hook");
        line = hook.Find("Line").GetComponent<LineRenderer>();

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
            Debug.Log(mouseDir);
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
                _player.MoveModule.MoveAble = true;
            }
        }
    }

    public override void Skill() {
        if (!Input.GetKeyDown(KeyCode.Q) || !_player.SkillModule.IsSkillable)
            return;
        //��ų�� �� �� �� ������ ��
        _player.MoveModule.MoveAble = false;
        //��ų �ٷ� �� ���� ����
        _player.SkillModule.IsSkillable = false;
        //��ų ���� �ѱ�
        hook.gameObject.SetActive(true);
        //��Ÿ��
        StartCoroutine(CoolTime());
    }
}
