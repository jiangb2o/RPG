using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance {get; private set;}

    public ItemDetailUI itemDetailUI;

    public GameObject itemPrefab;
    private GameObject uiGameObject;
    private GameObject content;

    private ItemUI currentItemUI;

    private bool isShow = false;

    private void Awake() 
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        uiGameObject = transform.Find("UI").gameObject;
        content = transform.Find("UI/Scroll View/Viewport/Content").gameObject;
        Hide();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            if(isShow) Hide();
            else Show();

            isShow = !isShow;
        }
    }

    public void Hide()
    {
        // 先调用itemDetailUI的Hide方法, 其中会调用ClearItemDetailUI删除启用的属性, 若先禁用则无法删除
        itemDetailUI.Hide();
        uiGameObject.SetActive(false);
    }

    public void Show()
    {
        uiGameObject.SetActive(true);
    }

    public void AddItem(ItemScriptObject itemSO)
    {
        GameObject ItemGameObject = GameObject.Instantiate(itemPrefab, content.transform);
        // 添加组件, 并为Scroll赋值
        ItemGameObject.AddComponent<ScrollRectEventAdd>().Scroll = transform.Find("UI/Scroll View").GetComponent<ScrollRect>();
        ItemUI itemUI = ItemGameObject.GetComponent<ItemUI>();

        itemUI.InitItem(itemSO);
    }
    

    public void ShowItemDetail(ItemUI itemUI)
    {
        // 显示itemSO详情
        itemDetailUI.Show();
        itemDetailUI.UpdateItemDetailUI(itemUI.itemSO);

        currentItemUI = itemUI;
    }

    public void CloseItemDetail()
    {
        itemDetailUI.Hide();
        currentItemUI = null;
    }

    // use current item
    public void OnItemUse()
    {
        InventoryManager.Instance.RemoveItem(currentItemUI.itemSO);
        itemDetailUI.Hide();
        ItemScriptObject itemSO = currentItemUI.itemSO;
        GameObject.FindGameObjectWithTag(Tag.PLAYER).GetComponent<PlayerItemUse>().UseItem(itemSO);


        Destroy(currentItemUI.gameObject);
    }
}
