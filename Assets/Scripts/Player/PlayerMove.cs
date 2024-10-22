using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    private NavMeshAgent playerAgent;
    // Start is called before the first frame update
    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * 获取EventSystem组件, 它为UI服务, IsPointerOverGameObject 为true表示在UI上, 则不进行移动
         * 从摄像机方向向鼠标位置发射射线
         * 如果碰撞到物体
         * 设置NavmeshAgent的目的位置, 利用自动导航系统导航到目标位置
         */
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            bool isCollide = Physics.Raycast(ray, out RaycastHit hit);
            if (isCollide)
            {
                if (hit.collider.CompareTag(Tag.GROUND))
                {
                    playerAgent.stoppingDistance = 0;
                    playerAgent.SetDestination(hit.point);
                }
                else if (hit.collider.CompareTag(Tag.INTERACTABLE))
                {
                    hit.collider.GetComponent<InteractableObject>().OnClick(playerAgent);
                }
            }

        }
    }
}
