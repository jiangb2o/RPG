using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffUI : MonoBehaviour
{
    public GameObject buffTemplate;

    private GameObject uiGameObject;
    private PlayerItemUse playerItemUse;

    private Image icon;
    private Image progressBar;

    // Start is called before the first frame update
    void Start()
    {
        uiGameObject = transform.Find("UI").gameObject;
        playerItemUse = GameObject.FindGameObjectWithTag(Tag.PLAYER).GetComponent<PlayerItemUse>();
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerItemUse.buffList.Count != 0)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }


    public void UpdateBuffUI()
    {
        Utils.ClearGrid(uiGameObject.transform);
        foreach(var buff in playerItemUse.buffList)
        {
            GameObject buffObject = Instantiate(buffTemplate, uiGameObject.transform);
            icon = buffObject.transform.Find("Icon").GetComponent<Image>();
            progressBar = buffObject.transform.Find("ProgressBar").GetComponent<Image>();

            icon.sprite = buff.itemSO.icon;
            progressBar.fillAmount = 1 - buff.currentDuration / Utils.consumableDuration;
        }

    }

    void Hide()
    {
        Utils.ClearGrid(uiGameObject.transform);
        uiGameObject.SetActive(false);
    }

    void Show()
    {   
        UpdateBuffUI();
        uiGameObject.SetActive(true);
    }



}
