using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Interfaces;

/// <summary>
/// 해적 스킬 클래스
/// </summary>
public class PirateSkillModule : BaseSkillModule
{
    //base 플레이어를 해적 플레이어로 바꾸기 작업
    PiratePlayer _Player => player as PiratePlayer;

    //해적 스킬용 무기
    public Transform hook;
    public LineRenderer line;
    //마우스 방향
    Vector2 mouseDir;
    //훅이 켜졌나
    public bool isHookActive;
    //사거리 끝인가
    public bool isLineMax;
    //플레이어가 닿았나?
    public bool isAttach;

    protected override void Start()
    {
        base.Start();
        player.InputModule.OnSkillEvent.AddListener(Skill);
        //각종 스킬을 위한 세팅
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
                _Player.MoveModule.Moveable = true;
            }
        }
    }

    protected override void Skill()
    {
        //스킬을 눌럿는가 스킬이 가능한가 체크
        if (_skillable == false || !_inputProvider.GetActionPressed(InputAction.Skill))
            return;
        //스킬을 쓸 때 못 움직일 때
        _Player.MoveModule.Moveable = false;
        //스킬 바로 못 쓰게 막기
        Skillable = false;
        //스킬 무기 켜기
        hook.gameObject.SetActive(true);
        //쿨타임
        StartCoroutine(CoolTime());
    }
}
