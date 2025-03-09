using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Ball")) {
            DestroyBall(collision.gameObject);
        }
    }

    void DestroyBall(GameObject _ball) {
        Destroy(_ball);
        Blackboard.s_Instance.ModifyBallCount(-1);
        if (Blackboard.s_Instance.BallsActive == 0) {
            if (Blackboard.s_Instance.Lives > 0) {
                Blackboard.s_Instance.ModifyLives(-1);
                Blackboard.s_Instance.OnSpawnBall?.Invoke();
            }
            // No CheckHighScore here—GameOverManager handles it
        }
    }
}