using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractableObject : MonoBehaviour
{
    private NavMeshAgent playerAgent;
    // 点击到物体时调用
    public void OnClick(NavMeshAgent playerAgent)
    {
        this.playerAgent = playerAgent;
        // Move
        playerAgent.stoppingDistance = 2.5f;
        playerAgent.SetDestination(transform.position);

    }

    private void Update()
    {
        if (playerAgent != null && playerAgent.pathPending == false)
        {
            // pathPending 为 true 时, 路径计算可能还未完成, 此时物体没到达终点但 remainingDistance 可能为0
            // 接近目标才触发交互
            if (playerAgent.remainingDistance <= 2)
            {
                Interact();
                // 一次点击的交互触发后结束, 防止持续触发交互
                playerAgent = null;
            }

        }
    }

    protected virtual void Interact()
    {
        print("Interacting with Interactable Object.");
    }


}
