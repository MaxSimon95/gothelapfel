using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotHandler : MonoBehaviour, IDropHandler 
{
    private Transform originalSlotItem;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if(eventData.pointerDrag != null)
        {
            // Put item in slot if slot is empty
            if (transform.childCount == 0)
            {

            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            }

            //switch items around if slot already is full
            else
            {
                originalSlotItem = transform.GetChild(0);
                // move this slot's original item into incoming item's origin slot
                //transform.GetChild(0).transform.SetParent(eventData.pointerDrag.transform.parent); 
                originalSlotItem.transform.SetParent(eventData.pointerDrag.GetComponent<DragAndDrop>().startParent);
                originalSlotItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

                // move incoming item into this slot
                eventData.pointerDrag.transform.SetParent(transform);
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            }
        }
    }

    public void ButtonPress()
    {
        Debug.Log("Item Slot Handler Click");
    }
}
