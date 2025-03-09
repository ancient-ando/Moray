using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinballScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        Blackboard.s_Instance.OnScoreBoard += UpdateScore;
        UpdateScoreDisplay();
    }

    void OnDestroy() {
        Blackboard.s_Instance.OnScoreBoard -= UpdateScore;
    }

    void UpdateScore(int newScore) {
        Blackboard.s_Instance.CurrentScore += newScore;
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay() {
        scoreText.text = Blackboard.s_Instance.CurrentScore.ToString("D6"); // 6-digit DMD style
    }
}
