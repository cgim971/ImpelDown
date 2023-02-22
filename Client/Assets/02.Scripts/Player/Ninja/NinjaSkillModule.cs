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
        if (Input.GetKeyDown(KeyCode.Q) == false || isDashing == true || _isSkillable == false)
            return;

        if (_player.PlayerState == ImpelDown.Proto.PlayerState.Ghost)
            return;

        isDashing = true;
        mouseDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        StartCoroutine(CoolTime());
    }

    private void FixedUpdate() {
        if (isDashing) {
            _player.Rigidbody.MovePosition(transform.position + mouseDir *  NinjaPlayer.NinjaDataSO.dashAmount);
            isDashing = false;
        }
    }
}
