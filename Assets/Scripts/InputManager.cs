using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class InputManager : GameplayMonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public bool LeftPaddle;
    public bool RightPaddle;
    public bool IsCharging;
    public bool IsLaunching;
    public bool Reset;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    private GameOverManager gameOverManager;
    private IntroManager introManager;

    void Awake()
    {
        gameOverManager = FindObjectOfType<GameOverManager>();
        introManager = FindObjectOfType<IntroManager>();
        if (introManager == null)
        {
            Debug.LogError("IntroManager not found in Awake - Looking for inactive");
            introManager = FindObjectOfType<IntroManager>(true); // Include inactive
        }
        Debug.Log("IntroManager: " + (introManager != null ? introManager.name : "Null"));
    }

#if ENABLE_INPUT_SYSTEM
    public void OnMove(InputValue value) { MoveInput(value.Get<Vector2>()); }
    public void OnLeftPaddle(InputValue value)
    {
        LeftPaddleInput(value.isPressed);
        Debug.Log("Left Paddle: " + value.isPressed);
    }
    public void OnRightPaddle(InputValue value)
    {
        RightPaddleInput(value.isPressed);
        Debug.Log("Right Paddle: " + value.isPressed);
    }
    public void OnChargeBall(InputValue value) { Charge(value.isPressed); }
    public void OnLaunchBall(InputValue value) { Launch(value.isPressed); }

    public void OnReset(InputValue value)
    {
        Debug.Log("OnReset InputManager: " + value.isPressed);
        if (value.isPressed)
        {
            if (Blackboard.s_Instance.Lives <= 0 && gameOverManager.gameOverScreen.activeSelf)
            {
                Debug.Log("Reset Pressed (Controller)—Restarting!");
                ResetGame();
                gameOverManager.HideGameOverScreen();
            }
            else if (introManager != null && introManager.introScreen.activeSelf)
            {
                Debug.Log("Start Pressed—Starting Game!");
                introManager.HideIntroScreen();
            }
        }
    }
#endif

    public void MoveInput(Vector2 newMoveDirection) { move = newMoveDirection; }
    public void LeftPaddleInput(bool newPaddleState) { LeftPaddle = newPaddleState; }
    public void RightPaddleInput(bool newPaddleState) { RightPaddle = newPaddleState; }
    public void Charge(bool _newChargingState) { IsCharging = _newChargingState; }
    public void Launch(bool _newLaunchState) { IsLaunching = _newLaunchState; }
    public void ResetInput(bool newResetState) { Reset = newResetState; }

    private void OnApplicationFocus(bool hasFocus) { SetCursorState(cursorLocked); }
    private void SetCursorState(bool newState) { Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None; }

    private void ResetGame()
    {
        Debug.Log("ResetGame Started");
        Blackboard.s_Instance.ResetScore();
        Debug.Log("Score Reset: " + Blackboard.s_Instance.CurrentScore);
        Blackboard.s_Instance.ModifyLives(3 - Blackboard.s_Instance.Lives);
        Debug.Log("Lives Reset: " + Blackboard.s_Instance.Lives);
        Blackboard.s_Instance.Pause();
        Debug.Log("Game Paused: " + Blackboard.s_Instance.Paused);
        if (introManager != null)
        {
            Debug.Log("Calling ShowIntroScreen");
            introManager.ShowIntroScreen();
            Debug.Log("ShowIntroScreen Done");
        }
        else
        {
            Debug.LogError("IntroManager is null in ResetGame!");
        }
    }
}