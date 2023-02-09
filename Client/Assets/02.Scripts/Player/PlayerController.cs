using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour {

    public bool IsPlayer => _isPlayer;
    private bool _isPlayer = false;


    public int PlayerId => _playerId;
    private int _playerId;



    public Rigidbody2D Rigidbody => _rigidbody;
    private Rigidbody2D _rigidbody = null;


    public PlayerMove PlayerMove => _playerMove;
    private PlayerMove _playerMove;
    public PlayerCatch PlayerCatch => _playerCatch;
    private PlayerCatch _playerCatch;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerMove = gameObject.AddComponent<PlayerMove>();
        _playerCatch = gameObject.AddComponent<PlayerCatch>();

        _playerMove.Init(this);
    }

    public void Init(bool isPlayer, int playerId) {
        _isPlayer = isPlayer;
        _playerId = playerId;

        if (_isPlayer == true) {
            StartCoroutine(SendPositionAndRotation());
        }
    }


    private void Update() {
        if (_isPlayer == true) {
            PlayAnimation();
            _playerMove.InputCheck();
        }
    }

    public void PlayAnimation() {

    }

    public void SetAnimation() {

    }

    public void SetPositionData(PositionData positionData, bool isImmediate = false) {
        _playerMove.SetPositionData(positionData.pos, isImmediate);
    }

    public void SetTailColor(int tailNo) {
        //_tailController.SetTail(tailNo);
    }

    private IEnumerator SendPositionAndRotation() {
        PosAndRot posAndRot = new PosAndRot();

        while (gameObject.activeSelf) {
            yield return new WaitForSeconds(0.04f);

            Vector2 pos = transform.position;

            posAndRot.X = pos.x;
            posAndRot.Y = pos.y;

            PlayerAllData playerAllData = new PlayerAllData { PlayerData = new PlayerData { PlayerId = _playerId }, PosAndRot = posAndRot };
            C_Move cMove = new C_Move { PlayerAllData = playerAllData };

            NetworkManager.Instance.RegisterSend((ushort)MSGID.CMove, cMove);
        }
    }

}
