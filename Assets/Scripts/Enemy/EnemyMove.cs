using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

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
    private Transform playerTransform;
    private EnemyProperty enemyProperty;
    
    public float viewRadius = 5f;
    public float viewAngle = 60f;
    public float escapeDistance = 8f;
    public float restTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        enemyProperty = GetComponent<EnemyProperty>();
        enemyAgent = GetComponent<NavMeshAgent>();
        state = EnemyState.NormalState;
        currentPosition = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        timerStr = gameObject.name + gameObject.GetInstanceID();
        TimerManager.Instance.AddTimer(timerStr, new Timer(restTime, callAction: SetRandomPosition));
    }

    // Update is called once per frame
    void Update()
    {
        enemyAgent.speed = enemyProperty.speed.value;
        UpdateState();
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
            enemyAgent.SetDestination(playerTransform.position);
        }
        currentPosition = transform.position;
    }

    private bool IsMoving() => currentPosition != transform.position;

    void UpdateState()
    {
        // switch 表达式
        // 匹配模式 => 赋值结果 
        // _ 即 default
        // when关键字后面指定case guard, 必须是布尔表达式
        // 匹配模式和case guard同时满足才进行赋值
        EnemyState newState = state switch
        {
            EnemyState.FightingState when PlayerEscape() => EnemyState.NormalState,
            EnemyState.NormalState when PlayerInView() => EnemyState.FightingState,
            _ => state
        };
        // 切换了状态, 需要清除之前的路径
        if (state != newState)
        {
            enemyAgent.ResetPath();
            state = newState;
        }
    }
    
    void SetRandomPosition()
    {
        if (state == EnemyState.NormalState)
        {
            enemyAgent.SetDestination(FindRandomPosition());
        }
    }
    
    Vector3 FindRandomPosition()
    {
        // 随机生成一个 xz平面的方向
        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        // 标准化后移动随机距离
        return transform.position + randomDir.normalized * Random.Range(5, 7);
    }
    
    bool PlayerEscape() => Vector3.Distance(transform.position, playerTransform.position) > escapeDistance;
    bool PlayerInView() =>
        Vector3.Distance(transform.position, playerTransform.position) < viewRadius &&
        Vector3.Angle(transform.forward, playerTransform.position - transform.position) < viewAngle / 2;

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position, viewRadius);
        Gizmos.color = Color.red;
        // 将forward向量分别顺时针和逆时针旋转 viewAngle/2
        Vector3 view = transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, viewAngle/2, 0) * view * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, -viewAngle/2, 0) * view * viewRadius);
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + Quaternion.Euler(0, viewAngle/2, 0) * view * viewRadius,
            transform.position + Quaternion.Euler(0, viewAngle/2, 0) * view * escapeDistance);
        Gizmos.DrawLine(transform.position + Quaternion.Euler(0, -viewAngle/2, 0) * view * viewRadius,
            transform.position + Quaternion.Euler(0, -viewAngle/2, 0) * view * escapeDistance);
    }
}
