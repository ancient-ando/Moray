using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PaddleType {
    Left,
    Right
}
public class Paddle : GameplayMonoBehaviour {
    public PaddleType PaddleType;
    public float Torque = 1000;
    Rigidbody2D _rb;
    

    protected override void Awake() {
        base.Awake();

        Blackboard.s_Instance.OnPaddleTriggered += OnPaddleTriggerd;
        Blackboard.s_Instance.OnPaddleReset += OnPaddleReset;

        _rb = GetComponent<Rigidbody2D>();
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        Blackboard.s_Instance.OnPaddleTriggered -= OnPaddleTriggerd;
        Blackboard.s_Instance.OnPaddleReset -= OnPaddleReset;
    }

    void OnPaddleTriggerd(PaddleType _paddleType) {
        if (Blackboard.s_Instance.Paused)
            return;
        if (PaddleType != _paddleType)
            return;

        if (PaddleType == PaddleType.Left)
            _rb.AddTorque(Torque);
        else
            _rb.AddTorque(-Torque);
    }

    void OnPaddleReset(PaddleType _paddleType) {
        if (Blackboard.s_Instance.Paused)
            return;
        if (PaddleType != _paddleType)
            return;

        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_defaultRotation), 0.1f);
    }
}