using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : GameplayMonoBehaviour {

    Rigidbody2D _ball;
    int _targetBallAmount = 1;
    int _ballsInHole = 0;
    bool _holeBlocked = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Ball")) {
            _ball = other.GetComponent<Rigidbody2D>();
            other.transform.position = transform.position;
            _ballsInHole++;
            StartCoroutine(DelayedLaunch());
        }
        else
            if (other.CompareTag("Planet") && !_holeBlocked) {
            _holeBlocked = true;
            _ball = other.GetComponent<Rigidbody2D>();
            other.transform.position = transform.position;
            _ball.simulated = false;
            _ball.gameObject.AddComponent<Bumper>();
            Destroy(_ball.GetComponent<Ball>());
            Destroy(gameObject);
        }
    }



    IEnumerator DelayedLaunch() {
        _ball.simulated = false;
        _ball.velocity = Vector2.zero;
        //while (Blackboard.s_Instance.Paused) {
        //    yield return new WaitForEndOfFrame();
        //}

        yield return new WaitForSeconds(1);


        //add some score of something idk

        LaunchBall();

    }

    void LaunchBall() {
        Vector2 direction = GetRandomDirection2D();
        _ball.simulated = true;
        _ball.AddForce(direction * 1000, ForceMode2D.Impulse);
        Blackboard.s_Instance.OnScoreBoard?.Invoke(5000);
        if (_ballsInHole >= _targetBallAmount) {
            Blackboard.s_Instance.ChangeHoleFilledCount(1);
        }
    }

    Vector2 GetRandomDirection2D() {
        float angle = Random.Range(0f, 359f);
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }

}