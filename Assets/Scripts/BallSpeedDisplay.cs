using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallSpeedText : MonoBehaviour {
    TextMeshProUGUI _text;

    void Awake() {
        _text = GetComponent<TextMeshProUGUI>();

        Blackboard.s_Instance.OnUpdateBallSpeed += UpdateText;
    }

    private void OnDestroy() {
        Blackboard.s_Instance.OnUpdateBallSpeed -= UpdateText;
    }

    void UpdateText(float speed) {
        _text.text = speed.ToString("Ball Speed: " + speed);
    }
    
}