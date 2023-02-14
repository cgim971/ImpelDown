using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Interfaces;
/// <summary>
/// Catch����� �ִ� �߻� Ŭ����
/// </summary>
public abstract class BaseCatchModule : MonoBehaviour
{
    protected Player player;
    private bool _catchable = true;
    public bool Catchable { get => _catchable; set => _catchable = value; }
    public bool catchActive = true;
    protected IInputProvider _inputProvider;

    private LayerMask _playerLayerMask;
    private Camera _mainCam = null;

    protected virtual void Start()
    {
        player = GetComponent<Player>();
        _inputProvider = GetComponent<IInputProvider>();
        player.InputModule.OnCatchEvent.AddListener(Catch);
        _playerLayerMask = LayerMask.GetMask("Player");
        _mainCam = Camera.main;
    }

    public void Catch()
    {
        if (_catchable == false||!_inputProvider.GetActionPressed(InputAction.Catch))
            return;
        catchActive = true;
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        Vector3 worldMousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        worldMousePos.z = 0;

        Vector3 delta = worldMousePos - transform.position;
        delta.Normalize();

        // ĳġ 
        //RaycastHit2D[] players = Physics2D.RaycastAll(transform.position, delta, 1.5f, _playerLayerMask);
        //foreach (RaycastHit2D player in players)
        //{
        //    int playerId = player.transform.GetComponent<PlayerController>().PlayerId;

        //    if (playerId == _playerId)
        //        continue;

        //    Debug.Log("PlayerId : " + _playerId);
        //    Debug.Log("TargetId : " + _targetId);

        //    if (playerId == _targetId)
        //    {
        //        C_Catch cCatch = new C_Catch
        //        {
        //            PlayerData = new PlayerData
        //            {
        //                PlayerId = _targetId,
        //                RoomIndex = RoomManager.Instance.RoomData.RoomIndex,
        //            }
        //        };

        //        NetworkManager.Instance.RegisterSend((ushort)MSGID.CCatch, cCatch);
        //    }
        //    return;
        //}
        //StartCoroutine(CatchTime());
    }

    IEnumerator CatchTime()
    {
        yield return new WaitForSeconds(0.05f);
        catchActive = false;
    }
}
