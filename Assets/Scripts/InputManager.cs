using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class InputManager : GameplayMonoBehaviour {
    public bool LeftPaddle;
    public bool RightPaddle;
    public bool IsCharging;
    public bool IsLaunching;
    public bool IsPaused;
    public bool GameStarted;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = false;
    public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM

    public void OnGameStart(InputValue value) {
        GameStart(value.isPressed);
    }

    public void OnLeftPaddle(InputValue value) {
        LeftPaddleInput(value.isPressed);
    }

    public void OnRightPaddle(InputValue value) {
        RightPaddleInput(value.isPressed);
    }

    public void OnChargeBall(InputValue value) {
        Charge(value.isPressed);
    }

    public void OnLaunchBall(InputValue value) {
        Launch(value.isPressed);
    }

    public void OnPause(InputValue value) {
        IsPaused = !IsPaused;
    }
#endif

    public void GameStart(bool _newStartState) {
        GameStarted = _newStartState;
    }

    public void LeftPaddleInput(bool newPaddleState) {
        LeftPaddle = newPaddleState;
    }

    public void RightPaddleInput(bool newPaddleState) { 
        RightPaddle = newPaddleState; 
    }
    public void Charge(bool _newChargingState) { 
        IsCharging = _newChargingState; 
    }
    public void Launch(bool _newLaunchState) {
        IsLaunching = _newLaunchState; 
    }

    private void OnApplicationFocus(bool hasFocus) {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState) {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}