using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PaddleType {
    Left,
    Right
}
public class Paddle : GameplayMonoBehaviour {
    public PaddleType PaddleType;
    [SerializeField]
    float _lerpTime = 0.1f;
    Vector3 _defaultRotation;
    Rigidbody2D _rb;
    [SerializeField]
    float _torque = 1000;

    protected override void Awake() {
        base.Awake();

        Blackboard.s_Instance.OnPaddleTriggered += OnPaddleTriggerd;
        Blackboard.s_Instance.OnPaddleReset += OnPaddleReset;

        _rb = GetComponent<Rigidbody2D>();

        _defaultRotation = transform.transform.eulerAngles;
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        Blackboard.s_Instance.OnPaddleTriggered -= OnPaddleTriggerd;
        Blackboard.s_Instance.OnPaddleReset -= OnPaddleReset;
    }

    protected override void PostResume() {
        OnPaddleReset(PaddleType);
    }

    void OnPaddleTriggerd(PaddleType _paddleType) {
        if (Blackboard.s_Instance.Paused)
            return;
        if (PaddleType != _paddleType)
            return;

        switch(PaddleType) {
            case PaddleType.Left:
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -315), _lerpTime);
            _rb.AddTorque(_torque);
            break;
            case PaddleType.Right:
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 315), _lerpTime);
            _rb.AddTorque(-_torque);
            break;
        }
    }

    void OnPaddleReset(PaddleType _paddleType) {
        if (Blackboard.s_Instance.Paused)
            return;
        if (PaddleType != _paddleType)
            return;

        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_defaultRotation), 0.1f);
    }
}