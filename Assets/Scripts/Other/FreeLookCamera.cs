using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class FreeLookCamera : MonoBehaviour
{
    public float zoomSpeed = 10;
    public float mouseSensitivityX = 1000f;
    public float mouseSensitivityY = 500f;
    public float high = 3;
    public float dis = 5;
    public float yRotation;

    private Vector3 offset;
    private Transform playerTransform;
    private Vector3 PlayerHead() => playerTransform.position + Vector3.up;

    // Start is called before the first frame update
    void Start()
    {
        yRotation = 0;
        playerTransform = GameObject.FindGameObjectWithTag(Tag.PLAYER).transform;

        transform.position = playerTransform.position - playerTransform.forward * dis + Vector3.up * high;
        transform.LookAt(PlayerHead());

        offset = transform.position - playerTransform.position;
    }
    void Correction()
    {
        yRotation = 0;
        transform.position = playerTransform.position - playerTransform.forward * dis + Vector3.up * high;
        transform.LookAt(PlayerHead());

        offset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 指针不在UI层
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(2))
            {
                Correction();
            }

            FreeLook();
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            // 获取相机组件
            Camera.main.fieldOfView -= scroll * zoomSpeed;
            //  限制范围
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 35.0f, 70.0f);
        }
    }

    private void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
    }

    private void FreeLook()
    {
        // 水平
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        // 竖直
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        yRotation -= mouseY;

        transform.RotateAround(PlayerHead(), Vector3.up, mouseX);

        if (yRotation < 30f && yRotation > -50f)
        {
            transform.RotateAround(PlayerHead(), transform.right, -mouseY);
        }
        else
        {
            mouseY = Mathf.Clamp(yRotation, -50f, 30f) - (yRotation + mouseY);
            transform.RotateAround(PlayerHead(), transform.right, -mouseY);
            yRotation = Mathf.Clamp(yRotation, -50f, 30f);
        }

        // 避免摄像机移动到地面下
        if (transform.position.y <= playerTransform.position.y - 1.0f)
        {
            transform.position = transform.position - Vector3.down;
        }

        // update offset
        offset = transform.position - playerTransform.position;
        transform.LookAt(PlayerHead());
    }
}
