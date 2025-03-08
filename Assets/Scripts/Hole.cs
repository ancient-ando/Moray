using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : GameplayMonoBehaviour {

    Rigidbody2D _ball;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Ball")) {
            _ball = other.GetComponent<Rigidbody2D>();
            Blackboard.s_Instance.OnBallEnterHole?.Invoke();
            other.transform.position = transform.position;
            StartCoroutine(DelayedDestroy());
        }
    }

    IEnumerator DelayedDestroy() {
        _ball.simulated = false;
        while(Blackboard.s_Instance.Paused) {
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1);
        Destroy(_ball.gameObject);
        Blackboard.s_Instance.OnSpawnBall?.Invoke();
    }

}