using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime;
    public int maxEnemyCount = 3;
    public float delayDieTime = 2f;

    private string timerStr;
    private float spawnTimer;
    private ObjectPool EnemyObjectPool;

    // Start is called before the first frame update
    void Start()
    {
        EnemyObjectPool = new ObjectPool(enemyPrefab, maxEnemyCount, transform.parent);
        timerStr = gameObject.name + gameObject.GetInstanceID();
        TimerManager.Instance.AddTimer(timerStr, new Timer(spawnTime, callAction:SpawnEnemy));
        EventCenter.OnEnemyDied += OnEnemyDie;
    }

    private void OnDisable()
    {
        TimerManager.Instance.RemoveTimer(timerStr);
    }

    void SpawnEnemy()
    {
        if(transform.childCount >= maxEnemyCount) return;
        
        GameObject newEnemy = EnemyObjectPool.Get();
        newEnemy.transform.position = transform.position;
        newEnemy.transform.parent = transform;
    }

    void OnEnemyDie(EnemyAttacked enemy)
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
