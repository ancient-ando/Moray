using UnityEngine;
using UnityEngine.Events;

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
    public UnityAction OnBallEnterHole;
    public UnityAction OnLivesChanged;
    public bool IsCharging { get { return _inputs.IsCharging; } }
    public bool BallLaunched;
    public int Lives { get; private set; } = 3;

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

        if(_inputs.IsCharging)
            OnBallCharge?.Invoke();
    }

    public void UpdateBallSpeed(float speed) {
        BallSpeed = speed;
        OnUpdateBallSpeed?.Invoke(speed);
    }

    public void ModifyLives(int delta) {
        Lives+= delta;
        OnLivesChanged?.Invoke();
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
}