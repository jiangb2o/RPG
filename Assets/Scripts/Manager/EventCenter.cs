using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCenter : MonoBehaviour
{
    public static event Action<EnemyAttacked> OnEnemyDied;

    public static void EnemyDied(EnemyAttacked enemyAttacked)
    {
        OnEnemyDied?.Invoke(enemyAttacked);
    }
}
