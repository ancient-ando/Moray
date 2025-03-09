using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreMessage;
    public TextMeshProUGUI resetPromptText;
    public AudioSource gameOverSound;

    void Awake()
    {
        Blackboard.s_Instance.OnLivesChanged += CheckGameOver;
        gameOverScreen.SetActive(false);
    }

    void OnDestroy()
    {
        Blackboard.s_Instance.OnLivesChanged -= CheckGameOver;
    }

    void CheckGameOver()
    {
        if (Blackboard.s_Instance.Lives <= 0)
        {
            ShowGameOver();
        }
    }

    void ShowGameOver()
    {
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

        if (gameOverSound != null)
        {
            gameOverSound.Play();
        }
    }

    public void HideGameOverScreen() // New method
    {
        gameOverScreen.SetActive(false);
    }
}