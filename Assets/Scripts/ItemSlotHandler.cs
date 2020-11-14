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


    // if uiScaling = 0, do nothing, else use it as a multiplier for dropped items
    public float uiScale;

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
                originalSlotItem.transform.SetParent(eventData.pointerDrag.GetComponent<DragAndDrop>().startParent);
                originalSlotItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

                // scale item
                float uiScaleOriginalSlot = originalSlotItem.transform.parent.gameObject.GetComponent<ItemSlotHandler>().uiScale;

                
                if (uiScaleOriginalSlot != 0)
                {
                    originalSlotItem.gameObject.GetComponent<RectTransform>().localScale = new Vector3(uiScaleOriginalSlot, uiScaleOriginalSlot, uiScaleOriginalSlot);
                }
                else
                {
                    originalSlotItem.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                }
                

                // move incoming item into this slot
                eventData.pointerDrag.transform.SetParent(transform);
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            }

            // scale item

            if (uiScale!=0)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().localScale = new Vector3(uiScale, uiScale, uiScale);
            }
            else
            {
                eventData.pointerDrag.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
            

        }
    }

    public void ButtonPress()
    {
    }
}
