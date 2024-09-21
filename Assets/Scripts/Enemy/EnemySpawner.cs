using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime;
    public int maxEnemyCount = 3;

    private float spawnTimer;
    private ObjectPool EnemyObjectPool;

    // Start is called before the first frame update
    void Start()
    {
        EnemyObjectPool = new ObjectPool(enemyPrefab, maxEnemyCount, transform.parent);
        EventCenter.OnEnemyDied += OnEmemyDie;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.childCount < maxEnemyCount)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnTime)
            {
                spawnTimer = 0;
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        GameObject newEnemy = EnemyObjectPool.Get();
        newEnemy.transform.position = this.transform.position;
        newEnemy.transform.parent = this.transform;
    }

    void OnEmemyDie(EnemyAttacked enemy)
    {
        EnemyObjectPool.Return(enemy.gameObject);
    }
}
