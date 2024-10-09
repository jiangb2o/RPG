using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPUI : MonoBehaviour
{
    public Vector3 offset;
    public float lerpSpeed = 1.5f;
    private EnemyAttacked targetEnemy;
    private RectTransform rect;
    private CapsuleCollider enemyCollider;

    private Image blood;
    private Image virtualBlood;

    // Start is called before the first frame update
    void Start()
    {
        targetEnemy = transform.parent.GetComponent<EnemyAttacked>();
        rect = GetComponent<RectTransform>();
        enemyCollider = transform.parent.GetComponent<CapsuleCollider>();
        virtualBlood = transform.Find("VirtualBlood").GetComponent<Image>();
        blood = transform.Find("Blood").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        blood.fillAmount = targetEnemy.HP / targetEnemy.MaxHp;
        virtualBlood.fillAmount = Mathf.Lerp(virtualBlood.fillAmount, blood.fillAmount, Time.deltaTime * lerpSpeed);
    }
        

    void LateUpdate()
    {
        Vector3 pos = targetEnemy.transform.position;
        pos.y = enemyCollider.height * transform.parent.localScale.y + offset.y;
        rect.transform.position = pos;
        rect.transform.forward = -Camera.main.transform.forward;
    }
}