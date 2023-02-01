using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Interfaces;
/// <summary>
/// Catch기능이 있는 추상 클래스 (아직 기능은 안 만듬)
/// </summary>
public abstract class BaseCatchModule : MonoBehaviour
{
    protected Player player;
    private bool _catchable = true;
    public bool Catchable { get => _catchable; set => _catchable = value; }
    protected IInputProvider _inputProvider;

    protected virtual void Start()
    {
        player = GetComponent<Player>();
        _inputProvider = GetComponent<IInputProvider>();
        player._InputModule.OnCatchEvent.AddListener(Catch);
    }

    public void Catch()
    {
        if (_catchable == false||!_inputProvider.GetActionPressed(InputAction.Catch))
            return;
        Debug.Log("Catch");
    }
}
