using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Destroy(collision.gameObject);
            Blackboard.s_Instance.ModifyLives(-1);
            if (Blackboard.s_Instance.Lives > 0)
            { // Changed to > 0 for clarity
                Blackboard.s_Instance.OnSpawnBall?.Invoke();
            }
            // No CheckHighScore here—GameOverManager handles it
        }
    }
}