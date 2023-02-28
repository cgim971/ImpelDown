using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaSkillModule : BaseSkillModule {
    public NinjaPlayer NinjaPlayer => _player as NinjaPlayer;


    private bool isDashing;

    private Vector3 mouseDir;

    public override void Init() {
        _player = GetComponent<NinjaPlayer>();
    }

    public override void Skill() {
        if (Skillable() == false)
            return;

        if (isDashing == true)
            return;

        isDashing = true;
        mouseDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        StartCoroutine(CoolTime());

        // °íÃÄ
        //C_Ninja_Skill cNinjaSkill = new C_Ninja_Skill { PlayerId = GameManager.Instance.PlayerId };
        //NetworkManager.Instance.RegisterSend((ushort)MSGID.SNinjaSkill, cNinjaSkill);
    }

    private void FixedUpdate() {
        if (isDashing) {
            _player.Rigidbody.MovePosition(transform.position + mouseDir * NinjaPlayer.NinjaDataSO.dashAmount);
            isDashing = false;
        }
    }
}
