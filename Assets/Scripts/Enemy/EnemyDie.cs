using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

public class EnemyDie : MonoBehaviour
{
    public Vector3 rotateOffset = Vector3.up * 1.5f;

    // Start is called before the first frame update
    void OnEnable()
    {
        // 禁用Transform 和 EnemyDie 以外的组件
        GetComponent<EnemyMove>().enabled = false;
        GetComponent<EnemyAttacked>().enabled = false;
        // NavMeshAgent 会阻碍旋转操作，因此禁用
        GetComponent<NavMeshAgent>().enabled = false;
        
        // 旋转至倒地
        transform.RotateAround(transform.position + rotateOffset, transform.forward, 90f);
    }

    void OnDisable()
    {
        // 恢复
        transform.RotateAround(transform.position + rotateOffset, transform.forward, -90f);

        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<EnemyAttacked>().enabled = true;
        GetComponent<EnemyMove>().enabled = true;
    }
}
