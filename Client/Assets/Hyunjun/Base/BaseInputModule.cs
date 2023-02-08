using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Enums;
using Interfaces;

/// <summary>
/// �Է� �߻� Ŭ����
/// </summary>
public abstract class BaseInputModule : MonoBehaviour, IInputProvider
{
    //��ư �̸���
    protected const string catchButton = "Catch";
    protected const string skillButton = "Skill";

    //���� �Է� �̺�Ʈ ������, ���, ��ų, ������ ���
    public UnityEvent OnMoveEvent = null;
    public UnityEvent OnCatchEvent = null;
    public UnityEvent OnSkillEvent = null;

    //�Է��ߴ��� üũ���ִ� HashSet, ���⿡ _requestedActions.Contains(action)�� ���� �� �����ϸ� �ԷµȰ�
    protected HashSet<InputAction> _requestedActions = new HashSet<InputAction>();

    //�Է��� ������Ʈ���� ó��
    protected virtual void Update()
    {
        CaptureInput();
        OnCatchEvent?.Invoke();
        OnSkillEvent?.Invoke();
    }
    //move�� rigidbody�� �����ؼ� fixedupdate���� ó��
    protected virtual void FixedUpdate()
    {
        OnMoveEvent?.Invoke();
    }
    //�������̽� ����
    public float GetAxis(Axis axis)
    {
        return Input.GetAxisRaw(axis.ToUnityAxis());
    }
    //�������� üũ���ִ� �Լ�
    public bool GetActionPressed(InputAction action)
    {
        return _requestedActions.Contains(action);
    }
    //���� �Է� ó�� ���ִ� �Լ�
    protected virtual void CaptureInput()
    {
        //������ ��
        if (Input.GetButtonDown(catchButton))
        {
            Debug.Log("Q");
            _requestedActions.Add(InputAction.Catch);
        }
        //���� ��
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
