using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateSkillModule : BaseSkillModule {
    public PiratePlayer PiratePlayer => _player as PiratePlayer;

    //해적 스킬용 무기
    [SerializeField]
    private Transform hook;
    [SerializeField]
    private LineRenderer line;
    //마우스 방향
    Vector2 mouseDir;
    //훅이 켜졌나
    public bool isHookActive;
    //사거리 끝인가
    public bool isLineMax;
    //플레이어가 닿았나?
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

        //훅이 켜졌는지 이중체크
        if (hook.gameObject.activeSelf && !isHookActive)
        {
            hook.position = transform.position;
            mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            isHookActive = true;
            Debug.Log(mouseDir);
        }
        //훅이 켜지고 사거리 끝이 아니며 플레이어와 닿지 않았다면 마우스 방향으로 이동
        if (isHookActive && (!isLineMax && !isAttach))
        {
            hook.Translate(mouseDir.normalized * Time.deltaTime * 15f);

            if (Vector2.Distance(transform.position, hook.position) >= 5)
            {
                isLineMax = true;
            }
        }
        //플레이어와 닿거나 사거리 끝이라면 되돌아오기
        else if (isHookActive && (isLineMax || isAttach))
        {
            hook.position = Vector2.MoveTowards(hook.position, transform.position, Time.deltaTime * 15);
            if (Vector2.Distance(transform.position, hook.position) < 0.1f)
            {
                isHookActive = false;
                isLineMax = false;
                isAttach = false;
                hook.gameObject.SetActive(false);
                PiratePlayer.InputModule.Moveable = true;
            }
        }
    }

    public override void Skill() {
        if (!Input.GetKeyDown(KeyCode.Q) || !Player.InputModule.Skillable)
            return;
        //스킬을 쓸 때 못 움직일 때
        PiratePlayer.InputModule.Moveable = false;
        //스킬 바로 못 쓰게 막기
        Player.InputModule.Skillable = false;
        //스킬 무기 켜기
        hook.gameObject.SetActive(true);
        //쿨타임
        StartCoroutine(CoolTime());
    }
}
