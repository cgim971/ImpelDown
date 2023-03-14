using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMoveModule : MonoBehaviour {
    #region Property
    public BasePlayer Player => _player;
    public float Speed { get; set; }
    #endregion

    protected BasePlayer _player;
    protected Vector2 _targetPos;

    protected Rigidbody2D _rigidbody;

    protected float _speed = 10f;


    public abstract void Init();


    public virtual void Move() {
        Vector2 velocity = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) {
            velocity.y += 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            velocity.y -= 1;
        }
        if (Input.GetKey(KeyCode.A)) {
            velocity.x -= 1;
        }
        if (Input.GetKey(KeyCode.D)) {
            velocity.x += 1;
        }

        velocity.Normalize();

        _rigidbody.velocity = velocity * _speed;
    }

    public void SetPositionData(Vector2 pos, bool isImmediate) {
        if (isImmediate) {
            transform.position = pos;
        }
        else {
            _targetPos = pos;
        }
    }

    protected virtual void Update() {
        if (_player.IsPlayer == false) {
            float lerpRatio = 15;
            Vector3 pos = Vector3.Lerp(transform.position, _targetPos, Time.deltaTime * lerpRatio);

            transform.position = pos;
        }
    }

}
