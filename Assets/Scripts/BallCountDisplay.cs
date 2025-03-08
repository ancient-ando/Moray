using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallCountDisplay : MonoBehaviour {
    TextMeshProUGUI _text;

    void Awake() {
        _text = GetComponent<TextMeshProUGUI>();

        Blackboard.s_Instance.OnBallCountChanged += UpdateText;

        UpdateText();
    }

    private void OnDestroy() {
        Blackboard.s_Instance.OnBallCountChanged -= UpdateText;
    }
    void UpdateText() {
        _text.text = "Balls: " + Blackboard.s_Instance.BallsActive;
    }
}
