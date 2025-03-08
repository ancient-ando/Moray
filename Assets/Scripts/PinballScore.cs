using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PinballScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Drag your TMP object here in Inspector
    private int score = 0;

    void Start()
    {
        UpdateScoreDisplay();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        scoreText.text = score.ToString("D6"); // Forces 6 digits, e.g., "000042"
    }
}
