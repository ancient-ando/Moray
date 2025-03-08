using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HighScoreData", menuName = "Pinball/HighScoreData", order = 1)]
public class HighScoreData : ScriptableObject {
    [SerializeField]
    private List<int> highScores = new List<int>();

    public int MaxEntries = 10;

    // Add a new score and sort the list
    public void AddScore(int newScore) {
        highScores.Add(newScore);
        highScores.Sort((a, b) => b.CompareTo(a)); //Should be highest to lowest)
        if (highScores.Count > MaxEntries) {
            highScores.RemoveAt(highScores.Count - 1); // Trim to MaxEntries
        }
    }

    // Get the list of high scores
    public List<int> GetHighScores() {
        return highScores;
    }
}
