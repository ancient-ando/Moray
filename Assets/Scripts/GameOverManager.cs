using TMPro;
using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {
    public GameObject gameOverScreen;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreMessage;
    public TextMeshProUGUI resetPromptText;
    public AudioSource gameOverSound;

    void Awake() {
        Blackboard.s_Instance.OnGameOver += ShowGameOver;
        gameOverScreen.SetActive(false);
    }

    void OnDestroy() {
        Blackboard.s_Instance.OnGameOver -= ShowGameOver;
    }

    void ShowGameOver() {
        Blackboard.s_Instance.Pause();
        gameOverScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        int finalScore = Blackboard.s_Instance.CurrentScore;
        finalScoreText.text = "Your Score: " + finalScore.ToString("D6");

        var highScores = Blackboard.s_Instance.highScoreData.GetHighScores();
        bool isNewHighScore = highScores.Count < Blackboard.s_Instance.highScoreData.MaxEntries ||
                              finalScore > highScores[highScores.Count - 1];
        highScoreMessage.text = isNewHighScore ? "New High Score!" : "";
        Blackboard.s_Instance.CheckHighScore();
        if (gameOverSound != null) {
            gameOverSound.Play();
        }
        StartCoroutine(Reset());
    }

    private IEnumerator Reset() {
        yield return new WaitForSeconds(3);
        HideGameOverScreen();
    }

    public void HideGameOverScreen() {
        gameOverScreen.SetActive(false);
        Blackboard.s_Instance.Reset();
    }
}