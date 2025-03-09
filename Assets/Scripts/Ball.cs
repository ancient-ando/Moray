using System.Collections;
using UnityEngine;

public class Ball : GameplayMonoBehaviour {

    Rigidbody2D _rb;
    public float Multiplier = 1;
    public float MaxSpeed = 10;
    AudioSource _sfx;

    protected override void Awake() {
        base.Awake();
        Blackboard.s_Instance.ModifyBallCount(1);
        _rb = GetComponent<Rigidbody2D>();
        _sfx = GetComponent<AudioSource>();
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        
    }

    protected override void PostPause() {
        _rb.simulated = false;
    }

    protected override void PostResume() {
        _rb.simulated = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        _sfx.Play();
    }

    private void FixedUpdate() {
        // Limit the speed of the ball
        if (_rb.velocity.magnitude > MaxSpeed) {
            _rb.velocity = _rb.velocity.normalized * MaxSpeed;
        }

        Blackboard.s_Instance.UpdateBallSpeed(_rb.velocity.magnitude);
    }
}
