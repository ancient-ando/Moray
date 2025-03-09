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
        _ball.velocity = Vector2.zero;
        while (Blackboard.s_Instance.Paused) {
            yield return new WaitForEndOfFrame();
        }
        while (true) {
            yield return new WaitForSeconds(1);
            break;
        }

        //add some score of something idk
        
        LaunchBall();

    }

    void LaunchBall() {
        Vector2 direction = GetRandomDirection2D();
        _ball.simulated = true;
        _ball.AddForce(direction * 10, ForceMode2D.Impulse);
    }

    Vector2 GetRandomDirection2D() {
        float angle = Random.Range(0f, 359f);
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }

}