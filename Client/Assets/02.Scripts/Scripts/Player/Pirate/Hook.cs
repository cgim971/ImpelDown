using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���⿡ ���δ� Ŭ����
/// </summary>
public class Hook : MonoBehaviour
{
    PiratePlayer player;

    private void Start()
    {
        player = GetComponentInParent<PiratePlayer>();
    }

    //�÷��̾ ������ �������� �ܼ��� ���� ���߿��� ���⿡ ���� �÷��̾� �������̶� ��ų ������ �� ���� ���� ���� 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.SkillModule.isAttach = true;
        collision.gameObject.transform.position = transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.gameObject.transform.position = transform.position;
    }
}
