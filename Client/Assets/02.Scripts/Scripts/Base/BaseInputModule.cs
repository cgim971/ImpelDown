using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Enums;
using Interfaces;

/// <summary>
/// 입력 추상 클래스
/// </summary>
public abstract class BaseInputModule : MonoBehaviour, IInputProvider
{
    //버튼 이름들
    protected const string catchButton = "Catch";
    protected const string skillButton = "Skill";

    //각종 입력 이벤트 움직임, 잡기, 스킬, 아이템 등등
    public UnityEvent OnMoveEvent = null;
    public UnityEvent OnCatchEvent = null;
    public UnityEvent OnSkillEvent = null;

    //입력했는지 체크해주는 HashSet, 여기에 _requestedActions.Contains(action)을 했을 때 존재하면 입력된거
    protected HashSet<InputAction> _requestedActions = new HashSet<InputAction>();

    //입력은 업데이트에서 처리
    protected virtual void Update()
    {
        CaptureInput();
        OnCatchEvent?.Invoke();
        OnSkillEvent?.Invoke();
    }
    //move는 rigidbody로 구현해서 fixedupdate에서 처리
    protected virtual void FixedUpdate()
    {
        OnMoveEvent?.Invoke();
    }
    //인터페이스 구현
    public float GetAxis(Axis axis)
    {
        return Input.GetAxisRaw(axis.ToUnityAxis());
    }
    //눌렀는지 체크해주는 함수
    public bool GetActionPressed(InputAction action)
    {
        return _requestedActions.Contains(action);
    }
    //각종 입력 처리 해주는 함수
    protected virtual void CaptureInput()
    {
        //눌렀을 때
        if (Input.GetButtonDown(catchButton))
        {
            Debug.Log("Q");
            _requestedActions.Add(InputAction.Catch);
        }
        //뗐을 때
        else
        {
            if (GetActionPressed(InputAction.Catch))
                _requestedActions.Remove(InputAction.Catch);
        }

        if (Input.GetButtonDown(skillButton))
        {
            Debug.Log("R");
            _requestedActions.Add(InputAction.Skill);
        }
        else
        {
            if (GetActionPressed(InputAction.Skill))
                _requestedActions.Remove(InputAction.Skill);
        }

    }
}
