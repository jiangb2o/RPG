using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ThirdPersonCamera : MonoBehaviour
{
    public float zoomSpeed = 10;
    public float high = 5;
    public float dis = 5;

    private Vector3 offset;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(Tag.PLAYER).transform;
        transform.position = playerTransform.position - playerTransform.forward * dis + Vector3.up * high;
        transform.forward = playerTransform.position - transform.position + Vector3.up;
        
        offset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 指针不在UI层
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            // 获取相机组件
            Camera.main.fieldOfView -= scroll * zoomSpeed;
            //  限制范围
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 35.0f, 70.0f);
        }
    }

    private void LateUpdate() {
        transform.position = playerTransform.position + offset;
    }
}
