using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 무기에 껴두는 클래스
/// </summary>
public class Hook : MonoBehaviour
{
    PiratePlayer player;

    private void Start()
    {
        player = GetComponentInParent<PiratePlayer>();
    }

    //플레이어가 닿으면 끌려가는 단순한 구조 나중에는 여기에 닿은 플레이어 움직임이랑 스킬 같은거 못 쓰게 막을 예정 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = collision.gameObject.transform.position;
        player.SkillModule.isAttach = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.gameObject.transform.position = transform.position;
    }
}
