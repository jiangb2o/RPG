using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GameTaskSO : ScriptableObject
{
    public GameTaskState state;

    public string[] dialogueTaskWaiting;
    public string[] dialogueTaskExecuting;
    public string[] dialogueTaskCompleted;
    public string[] dialogueTaskEnd;

    public ItemScriptObject[] startReward;
    public ItemScriptObject[] completeReward;

    public int targetEnemyCount;
    public int currentEnemyCount;

    public void Start()
    {
        state = GameTaskState.Executing;
        currentEnemyCount = 0;
        EventCenter.OnEnemyDied += OnEnemyDied;
    }

    public void End()
    {
        state = GameTaskState.End;
        EventCenter.OnEnemyDied -= OnEnemyDied;
    }

    private void OnEnemyDied(EnemyAttacked enemyAttacked)
    {
        if(state == GameTaskState.Completed)
        {
            return;
        }

        currentEnemyCount++;
        if(currentEnemyCount >= targetEnemyCount)
        {
            state = GameTaskState.Completed;
            MessageUI.Instance.Show("任务完成! 请提交任务.");
        }
    }

}
