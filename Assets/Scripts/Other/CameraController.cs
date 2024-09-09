using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 10;

    private Vector3 offset;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(Tag.PLAYER).transform;
        offset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + offset;

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            // 获取相机组件
            Camera.main.fieldOfView -= scroll * zoomSpeed;
            //  限制范围
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 35.0f, 70.0f);
        }
    }
}
