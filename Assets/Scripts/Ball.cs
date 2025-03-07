using System.Collections;
using UnityEngine;

public class Ball : GameplayMonoBehaviour {

    Rigidbody2D _rb;
    public float Multiplier = 1;
    public float MaxSpeed = 10;

    protected override void Awake() {
        base.Awake();

        _rb = GetComponent<Rigidbody2D>();
    }

    protected override void PostPause() {
        _rb.simulated = false;
    }

    protected override void PostResume() {
        _rb.simulated = true;
    }

    private void FixedUpdate() {
        if (_rb.velocity.magnitude > MaxSpeed) {
            _rb.velocity = _rb.velocity.normalized * MaxSpeed;
        }
        Blackboard.s_Instance.UpdateBallSpeed(_rb.velocity.magnitude * Multiplier);
    }
}
