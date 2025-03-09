using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinballScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Awake()
    {
        Blackboard.s_Instance.OnScoreBoard += UpdateScore;
        UpdateScoreDisplay();
    }

    void OnDestroy() {
        Blackboard.s_Instance.OnScoreBoard -= UpdateScore;
    }

    void UpdateScore(int newScore) {
        score = newScore;
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay() {
        scoreText.text = score.ToString("D6"); // 6-digit DMD style
    }
}
