using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : GameplayMonoBehaviour {

    public bool _ballDetected = false;
    bool _isCharging = false;
    public float Charge = 0;
    public float ChargeRate = 3;

    Rigidbody2D _ballRb;

    protected override void Awake() {
        base.Awake();
        //_ballDetected = false;

        Blackboard.s_Instance.OnBallCharge += OnCharging;
    }
    void Start() {

    }
    protected override void OnDestroy() {
        base.OnDestroy();

        Blackboard.s_Instance.OnBallCharge -= OnCharging;
    }

    void OnTriggerStay2D(Collider2D collision) { 
        if (collision.gameObject.CompareTag("Ball")) {
            _ballDetected = true;
            if(_ballRb == null)
                _ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Ball")) {
            _ballDetected = false;
            _ballRb = null;
        }
    }

    private IEnumerator ChargeCoroutine() {
        while (_isCharging) {
            if (!Blackboard.s_Instance.IsCharging) {
                LaunchBall();
                yield return null;
            }
            Charge += Time.fixedDeltaTime * ChargeRate;
            yield return new WaitForFixedUpdate();
        }
    }

    public void OnCharging() {
        if (!_ballDetected || _isCharging)
            return;
        _isCharging = true;
        StartCoroutine(ChargeCoroutine());
    }

    public void LaunchBall() {
        _ballRb.AddForce(Vector2.up * (Charge * 10), ForceMode2D.Impulse);
        _isCharging = false;
    }
}