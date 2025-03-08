using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class InputManager: GameplayMonoBehaviour{
    [Header("Character Input Values")]
    public Vector2 move;
    public bool LeftPaddle;
    public bool RightPaddle;
    public bool IsCharging;
    public bool IsLaunching;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
    public void OnMove(InputValue value) {
        MoveInput(value.Get<Vector2>());
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
#endif


    public void MoveInput(Vector2 newMoveDirection) {
        move = newMoveDirection;
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