using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBallSpawn : MonoBehaviour {
    public GameObject[] BallPrefab;
    BoxCollider2D _spawnArea;

    private void Awake() {
        Blackboard.s_Instance.OnMultiBallSpawn += GetSpawnPoints;
        Blackboard.s_Instance.OnFilledHolesChanged += CheckHoleFilledCount;

        _spawnArea = GetComponent<BoxCollider2D>();
    }

    private void OnDestroy() {
        Blackboard.s_Instance.OnMultiBallSpawn -= GetSpawnPoints;
        Blackboard.s_Instance.OnFilledHolesChanged -= CheckHoleFilledCount;
    }

    void GetSpawnPoints() {
        for(int i=0; i<BallPrefab.Length; i++) {
            //Choose a random point within the Collider area
            float x = Random.Range(transform.position.x - _spawnArea.size.x / 2, transform.position.x + _spawnArea.size.x / 2);
            float y = Random.Range(transform.position.y - _spawnArea.size.y / 2, transform.position.y + _spawnArea.size.y / 2);

            Instantiate(BallPrefab[i], new Vector2(x,y), Quaternion.identity);
        }
    }

    public void SpawnBalls(Vector3 _spawnPos, int _prefabIndex) {
        Instantiate(BallPrefab[_prefabIndex], _spawnPos, Quaternion.identity);
    }

    void CheckHoleFilledCount() {
        if (Blackboard.s_Instance.HolesFilledCount ==3) {
            Blackboard.s_Instance.OnMultiBallSpawn?.Invoke();
        }
    }
}