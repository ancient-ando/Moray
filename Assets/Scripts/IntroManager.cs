using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class IntroManager : MonoBehaviour
{
    public GameObject introScreen;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI startPromptText;

    void Awake()
    {
        ShowIntroScreen();
    }

#if ENABLE_INPUT_SYSTEM
    public void OnReset(InputValue value)
    {
        if (value.isPressed && introScreen.activeSelf)
        {
            Debug.Log("Start Pressed—Starting Game!");
            HideIntroScreen();
        }
    }
#endif

    public void ShowIntroScreen()
    {
        Debug.Log("ShowIntroScreen Called");
        introScreen.SetActive(true);
        Debug.Log("Intro Active: " + introScreen.activeSelf);
        Blackboard.s_Instance.Pause();
        Debug.Log("Paused: " + Blackboard.s_Instance.Paused);

        var highScores = Blackboard.s_Instance.highScoreData.GetHighScores();
        string displayText = "High Scores:\n";
        for (int i = 0; i < highScores.Count; i++)
        {
            displayText += $"{i + 1}. {highScores[i]:D6}\n";
        }
        highScoreText.text = displayText;

        startPromptText.text = "Press A to Start";
    }

    public void HideIntroScreen()
    {
        Debug.Log("Hiding Intro Screen - Active Before: " + introScreen.activeSelf);
        introScreen.SetActive(false);
        Debug.Log("Active After: " + introScreen.activeSelf);
        Blackboard.s_Instance.Resume();
        Blackboard.s_Instance.OnSpawnBall?.Invoke();
    }


}