using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public int level = 1;
    public int currentExp = 0; 

    private PlayerProperty playerProperty;
    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        // 注册事件
        EventCenter.OnEnemyDied += OnEnemyDied;

        playerProperty = gameObject.GetComponent<PlayerProperty>();
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        // 取消注册
        EventCenter.OnEnemyDied -= OnEnemyDied;
    }

    private void OnEnemyDied(EnemyAttacked enemyAttacked)
    {
        this.currentExp += enemyAttacked.exp;

        if(currentExp >= level * 30)
        {
            currentExp -= level * 30;
            level++;
            playerProperty.UpdateMaxHp(level);
        }
        PlayerPropertyUI.Instance.UpdatePlayerPropertyUI();
    }
}
