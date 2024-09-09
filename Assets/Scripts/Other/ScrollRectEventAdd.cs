using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollRectEventAdd : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollHandler
{
    private ScrollRect Scroll;


    private void Awake()
    {
        GameObject InventoryUI = GameObject.Find("Canvas/InventoryUI");

        Scroll = InventoryUI.transform.Find("UI/Scroll View").GetComponent<ScrollRect>();
    }

    // scroll
    public void OnScroll(PointerEventData eventData)
    {
        Scroll.OnScroll(eventData);
    }

    // drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        Scroll?.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Scroll?.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Scroll?.OnEndDrag(eventData);
    }
}
