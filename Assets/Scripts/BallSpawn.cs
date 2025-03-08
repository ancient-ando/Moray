using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour {
    public GameObject BallPrefab;

    void Awake() {
        Blackboard.s_Instance.OnBallLost += SpawnBall;
    }

    void OnDestroy() {
        Blackboard.s_Instance.OnBallLost -= SpawnBall;
    }

    void SpawnBall() {
        Instantiate(BallPrefab, transform.position, Quaternion.identity);
    }
}