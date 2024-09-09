using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime;
    public int maxEnemyCount = 3;

    private float spawnTimer;


    // Start is called before the first frame update
    void Start()
    {

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
        GameObject newEnemy = GameObject.Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        newEnemy.transform.parent = this.transform;
    }
}
