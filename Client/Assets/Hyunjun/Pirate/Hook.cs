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
        transform.position = collision.gameObject.transform.position;
        player.SkillModule.isAttach = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.gameObject.transform.position = transform.position;
    }
}
