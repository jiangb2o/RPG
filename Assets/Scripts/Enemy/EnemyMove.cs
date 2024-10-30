using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//
// 未发现Player
// 正常状态: 设置随机目的位置 -> 移动到目的位置 -> 休息
// 发现Player
// 战斗状态:
// 
public class EnemyMove : MonoBehaviour
{
    public enum EnemyState
    {
        NormalState,
        FightingState,
    }

    private NavMeshAgent enemyAgent;
    private EnemyState state;
    private Vector3 currentPosition;
    private string timerStr;

    public float restTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        state = EnemyState.NormalState;
        currentPosition = transform.position;
        timerStr = gameObject.name + gameObject.GetInstanceID();
        TimerManager.Instance.AddTimer(timerStr, new Timer(restTime, 
            callAction: () => enemyAgent.SetDestination(FindRandomPosition())
            )
        );
    }

    // Update is called once per frame
    void Update()
    {

        if (state == EnemyState.NormalState)
        {
            if (IsMoving())
            {
                if (enemyAgent.remainingDistance > 0)
                {
                    TimerManager.Instance.ResetTimer(timerStr);
                }
            }
        }
        else if (state == EnemyState.FightingState)
        {

        }
        currentPosition = transform.position;

    }

    private bool IsMoving() => currentPosition != transform.position;

    Vector3 FindRandomPosition()
    {
        // 随机生成一个 xz平面的方向
        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        // 标准化后移动随机距离
        return transform.position + randomDir.normalized * Random.Range(5, 7);
    }
}
