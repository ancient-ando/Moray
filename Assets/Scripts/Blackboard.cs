using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Manager script that all other scripts can reference to handle game state and events
/// </summary>
public class Blackboard : MonoBehaviour {
    public static Blackboard s_Instance;

    public bool Paused = false;

    public UnityAction OnPause;
    public UnityAction OnResume;
    public UnityAction<float> OnUpdateBallSpeed;
    public UnityAction<PaddleType> OnPaddleTriggered;
    public UnityAction<PaddleType> OnPaddleReset;
    public UnityAction OnBallCharge;
    public UnityAction OnSpawnBall;
    public UnityAction OnFilledHolesChanged;
    public UnityAction OnLivesChanged;
    public UnityAction OnBallCountChanged;
    public UnityAction OnMultiBallSpawn;
    public UnityAction<int> OnScoreBoard;
    public UnityAction OnGameStart;
    public UnityAction OnGameReset;
    public UnityAction OnGameOver;

    public bool IsCharging { get { return _inputs.IsCharging; } }
    public bool BallLaunched;
    public bool IsGameStarted;
    public int Lives { get; private set; } = 3;
    public int HolesFilledCount { get; private set; } = 0;

    //KM - Code for Score
    public int CurrentScore;
    public HighScoreData highScoreData;

    public int BallsActive { get; private set; }

    InputManager _inputs;

    public float BallSpeed { get; private set; }

    void Awake() {
        s_Instance = this;

        _inputs = GetComponent<InputManager>();

        Debug.Log("Blackboard Initialised");
    }

    void Update() {
        if (_inputs.LeftPaddle)
            OnPaddleTriggered?.Invoke(PaddleType.Left);
        else
            OnPaddleReset?.Invoke(PaddleType.Left);
        if (_inputs.RightPaddle)
            OnPaddleTriggered?.Invoke(PaddleType.Right);
        else
            OnPaddleReset?.Invoke(PaddleType.Right);

        if (_inputs.IsCharging)
            OnBallCharge?.Invoke();

        if (!Paused && _inputs.IsPaused)
            Pause();
        else
        if (Paused && !_inputs.IsPaused)
            Resume();

        if (_inputs.GameStarted && !IsGameStarted) {
            IsGameStarted = true;
            OnGameStart?.Invoke();
        }

    }

    public void UpdateBallSpeed(float speed) {
        BallSpeed = speed;
        OnUpdateBallSpeed?.Invoke(speed);
    }

    public void ModifyLives(int delta) {
        Lives += delta;
        OnLivesChanged?.Invoke();
    }

    public void ModifyBallCount(int delta) {
        BallsActive += delta;
        OnBallCountChanged?.Invoke();
    }

    public void AddScore(int points) {
        CurrentScore += points;
        OnScoreBoard?.Invoke(CurrentScore);
    }

    #region Pause/Resume

    [ContextMenu("Pause")]
    public void Pause() {
        Paused = true;
        OnPause?.Invoke();
    }

    [ContextMenu("Resume")]
    public void Resume() {
        Paused = false;
        OnResume?.Invoke();
    }
    #endregion
    //KM - This should update the highscores?
    public void CheckHighScore() {
        if (highScoreData != null) {
            highScoreData.AddScore(CurrentScore);
        }
    }
    public void ResetScore() {
        CurrentScore = 0;
        OnScoreBoard?.Invoke(CurrentScore);
    }

    public void Reset() {
        ResetScore();
        _inputs.GameStarted = false;
        IsGameStarted = false;
        OnGameReset?.Invoke();
        Lives = 3;
    }

    public void ChangeHoleFilledCount(int delta) {
        HolesFilledCount += delta;
        OnFilledHolesChanged?.Invoke();
    }
}