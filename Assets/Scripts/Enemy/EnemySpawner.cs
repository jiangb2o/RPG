using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime;
    public int maxEnemyCount = 3;
    public float delayDieTime = 2f;

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
        if (transform.childCount < maxEnemyCount)
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
        newEnemy.transform.position = transform.position;
        newEnemy.transform.parent = transform;
    }

    void OnEmemyDie(EnemyAttacked enemy)
    {
        StartCoroutine(DelayReturn(enemy.gameObject));
    }

    IEnumerator DelayReturn(GameObject enemy)
    {
        yield return new WaitForSeconds(delayDieTime);
        enemy.GetComponent<EnemyDie>().enabled = false;
        EnemyObjectPool.Return(enemy);
    }
}
