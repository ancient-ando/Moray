using UnityEngine;
using UnityEngine.Events;

public abstract class GameplayMonoBehaviour : MonoBehaviour {

    [SerializeField] bool _pausable;
    public bool _initialised;

    protected virtual void Awake() {
        if (!_pausable) {
            _initialised = true;
            return;
        }
        if (Blackboard.s_Instance == null)
            return;

        Blackboard.s_Instance.OnPause += OnPause;
        Blackboard.s_Instance.OnResume += OnResume;

        _initialised = true;
    }

    protected virtual void OnDestroy() {
        if (!_pausable)
            return;
        if (Blackboard.s_Instance == null)
            return;
        Blackboard.s_Instance.OnPause -= OnPause;
        Blackboard.s_Instance.OnResume -= OnResume;
    }

    void OnPause() {
        if (!_pausable)
            return;
        enabled = false;
        PostPause();
    }
    void OnResume() {
        if (!_pausable)
            return;
        enabled = true;
        PostResume();
    }

    //#######################
    //Functions to handle what each specific GameObject should do when paused/resumed
    //e.g. Turn off physics, VFX,SFX etf
    //#######################

    protected virtual void PostPause() { }
    protected virtual void PostResume() { }
}