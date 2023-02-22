using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaSkillModule : BaseSkillModule
{
    public NinjaPlayer NinjaPlayer => _player as NinjaPlayer;

    public float dashAmount = 50f;
    private bool isDashing;

    private Vector3 mouseDir;

    public override void Init()
    {
        _player = GetComponent<NinjaPlayer>();
    }

    public override void Skill()
    {
        if (Input.GetKey(KeyCode.R) == false)
            return;
        mouseDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        isDashing = true;
        StartCoroutine(CoolTime());
        Debug.Log(mouseDir);
    }

    private void FixedUpdate()
    {
        if(isDashing)
        {
            _player.Rigidbody.MovePosition(transform.position + mouseDir * dashAmount);
            Debug.Log("?");
            isDashing = false;
        }
    }
}
