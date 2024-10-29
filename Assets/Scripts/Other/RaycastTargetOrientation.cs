using UnityEngine;
using UnityEngine.UI;

public class RaycastTargetOrientation : MonoBehaviour
{
    // 定义一个静态变量保存UI四个角点位置信息
    private static Vector3[] UIFourCorners = new Vector3[4];

    private void OnDrawGizmos()
    {
        // 获取所有 UI元素
        Image[] images = GameObject.FindObjectsOfType<Image>();
        // 遍历所有元素
        foreach (Image image in images)
        {
            // 如果元素勾选 raycastTarget，则进行划线显示
            if (image.raycastTarget == true && image.isActiveAndEnabled)
            {
                RectTransform rect = image.transform as RectTransform;
                rect.GetWorldCorners(UIFourCorners);
                Gizmos.color = Color.red;

                for (int i = 0; i < 4; i++)
                {
                    Gizmos.DrawLine(UIFourCorners[i], UIFourCorners[(i + 1) % 4]);
                }
            }
        }
    }

}