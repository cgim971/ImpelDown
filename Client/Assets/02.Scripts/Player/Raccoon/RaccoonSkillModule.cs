using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonSkillModule : BaseSkillModule
{
    public RaccoonPlayer RaccoonPlayer => _player as RaccoonPlayer;

    private GameObject _skillLight;

    public override void Init()
    {
        _player = GetComponent<RaccoonPlayer>();
        _skillLight = transform.Find("Light").gameObject;
    }

    public override void Skill()
    {
        if (!Input.GetKeyDown(KeyCode.Q) || !_player.SkillModule.IsSkillable)
            return;
        RaccoonPlayer.SkillModule.IsSkillable = false;
        _skillLight.SetActive(true);
        RaccoonPlayer.MoveModule.Speed = RaccoonPlayer.RaccoonDataSO.SKillSpeed;
        StartCoroutine(RunTime());
        StartCoroutine(CoolTime());
    }

    IEnumerator RunTime()
    {
        yield return new WaitForSeconds(RaccoonPlayer.RaccoonDataSO.SkillRunTime);
        RaccoonPlayer.MoveModule.Speed = Player.PlayerDataSO.Speed;
        _skillLight.SetActive(false);
    }
}
