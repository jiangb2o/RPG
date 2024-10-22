using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

class MyColor
{
    public static Color criticalDamage = new Color(0.86f, 0.27f, 0.11f);
    public static Color normalDamage = new Color(0.81f, 0.57f, 0.33f);
}
public class PopupText : MonoBehaviour
{
    private static int nextId;
    public int id;
    
    public float damage;
    public bool isCritical = false;
    public float duration = 1.5f;
    
    public float moveSpeed = 1.2f;
    public Vector3 moveDirection;
    public Vector3 dirOffset = Vector3.zero;
    public Vector3 spawnOffset = Vector3.zero;
    public float threshAlpha = 0.5f;
    
    private RectTransform rectTransform;
    private TextMeshProUGUI text;
    
    // Start is called before the first frame update
    void Start()
    {
        id = nextId++;
        GetComponent<Canvas>().sortingOrder = id;
        
        rectTransform = GetComponent<RectTransform>();
        text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        text.text = damage.ToString();

        Transform parent = transform.parent;
        Vector3 pos = parent.position;
        pos.y = parent.gameObject.GetComponent<CapsuleCollider>().height * transform.parent.localScale.y + spawnOffset.y;
        rectTransform.position = pos;

        dirOffset.y = Random.Range(-0.1f, 0.1f);
        dirOffset.x = Random.Range(-0.1f, 0.1f);
        moveDirection = (Vector3.up + Vector3.right + dirOffset).normalized;
        
        CriticalTextSetting();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fade();
        
        if(text.alpha <= threshAlpha) Destroy(gameObject);
    }

    void LateUpdate()
    {
        rectTransform.LookAt(rectTransform.position + Camera.main.transform.forward);
    }

    private void Move()
    {
        rectTransform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void Fade()
    {
        text.alpha -= Time.deltaTime / duration * (1 - threshAlpha);
    }

    private void CriticalTextSetting()
    {
        if (isCritical)
        {
            text.color = MyColor.criticalDamage;
            text.fontStyle = FontStyles.Bold;
            text.fontSize = 0.3f;
        }
        else
        {
            text.color = MyColor.normalDamage;
            text.fontSize = 0.2f;
        }
    }
}
