using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesDisplay : MonoBehaviour
{
    TextMeshProUGUI _text;

    void Awake() {
        _text = GetComponent<TextMeshProUGUI>();

        Blackboard.s_Instance.OnLivesChanged += UpdateText;

        UpdateText();
    }

    private void OnDestroy() {
        Blackboard.s_Instance.OnLivesChanged-= UpdateText;
    }
    void UpdateText() {
        _text.text = "Lives: " + Blackboard.s_Instance.Lives;
    }
}
