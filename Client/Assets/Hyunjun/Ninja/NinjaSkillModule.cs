using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Interfaces;

public class NinjaSkillModule : BaseSkillModule
{
    NinjaPlayer _Player => player as NinjaPlayer;

    public float dashAmount = 50f;
    private bool isDashing;

    private Vector3 mouseDir;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player._InputModule.OnSkillEvent.AddListener(Skill);
    }

    private void FixedUpdate()
    {
        if(isDashing)
        {
            _Player.Rb.MovePosition(transform.position + mouseDir * dashAmount);
            Debug.Log("?");
            isDashing = false;
        }
    }

    protected override void Skill()
    {
        if (_skillable == false || !_inputProvider.GetActionPressed(InputAction.Skill))
            return;
        _skillable = false;
        mouseDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        isDashing = true;
        Debug.Log(mouseDir);
    }
}
