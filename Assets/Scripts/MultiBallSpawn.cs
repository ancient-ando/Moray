using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBallSpawn : MonoBehaviour {
    public GameObject[] BallPrefab;
    BoxCollider2D _spawnArea;

    private void Awake() {
        Blackboard.s_Instance.OnMultiBallSpawn += GetSpawnPoints;

        _spawnArea = GetComponent<BoxCollider2D>();
    }

    private void OnDestroy() {
        Blackboard.s_Instance.OnMultiBallSpawn -= GetSpawnPoints;
    }

    void GetSpawnPoints(int _amount) {
        for(int i=0; i<_amount; i++) {
            //Choose a random point within the Collider area
            float x = Random.Range(transform.position.x - _spawnArea.size.x / 2, transform.position.x + _spawnArea.size.x / 2);
            float y = Random.Range(transform.position.y - _spawnArea.size.y / 2, transform.position.y + _spawnArea.size.y / 2);

            SpawnBalls(new Vector3(x, y, 0), Random.Range(0, BallPrefab.Length));
        }
    }

    void SpawnBalls(Vector3 _spawnPos, int _prefabIndex) {
        Instantiate(BallPrefab[_prefabIndex], _spawnPos, Quaternion.identity);
    }
}