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
    public GameObject associatedContainer;
    public bool prohibitDrop;


    // if uiScaling = 0, do nothing, else use it as a multiplier for dropped items
    public float uiScale;

    void Start()
    {
        UpdateSlotVisibility();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PutItemIntoSlot(InventoryItemHandler incomingItem)
    {
        GameObject incomingItemOriginalSlot = incomingItem.transform.parent.gameObject;

        // Put item in slot if slot is empty
        if (transform.childCount == 0)
        {

            incomingItem.transform.SetParent(transform);
            incomingItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        }

        // if itemslot is not empty, check if a merge is appicable (1 ingredient incoming, 1 ingredient existing, same ingredients incoming and existing)
        else
        {
            if
                (
                    (incomingItem.GetComponent<InventoryItemHandler>().ingredientTypes.Count == 1)
                    &&
                    (transform.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().ingredientTypes.Count == 1)
                    &&
                    (incomingItem.GetComponent<InventoryItemHandler>().ingredientTypes[0] == transform.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().ingredientTypes[0])
                )
            {   // its the same ingredient type: merge items
                //Debug.Log("FIT FOR MERGE");
                int amountIncoming = incomingItem.GetComponent<InventoryItemHandler>().ingredientTypeAmounts[0];
                int amountInSlot = transform.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().ingredientTypeAmounts[0];
                int amountTotal = amountInSlot + amountIncoming;
                float temperatureIncoming = incomingItem.GetComponent<InventoryItemHandler>().temperature;
                float temperatureInSlot = transform.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().temperature;

                // set temperature
                transform.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().temperature = temperatureIncoming * ((float)amountIncoming / (float)amountTotal) + temperatureInSlot * ((float)amountInSlot / (float)amountTotal);

                // set amount
                transform.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().ingredientTypeAmounts[0] = amountTotal;

                // delete the incoming object if incoming item != the existing item in slot
                //if(incomingItem.gameObject != transform.GetChild(0).gameObject)
                incomingItem.gameObject.GetComponent<InventoryItemHandler>().DeleteInstanceOfInventoryItem();

                // update item in slot 

                transform.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().UpdateItemContent();
            }
            else
            {   // its not the same ingredient type or there are mixtures involved: switch items around if slot already is full (and its two different items)
                //Debug.Log("NOT FIT FOR MERGE");

                originalSlotItem = transform.GetChild(0);
                originalSlotItem.transform.SetParent(incomingItem.GetComponent<DragAndDrop>().startParent);
                originalSlotItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

                // scale item
                Debug.Log(originalSlotItem);
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
                incomingItem.transform.SetParent(transform);
                incomingItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            }


        }

        // scale item

        if (uiScale != 0)
        {
            incomingItem.GetComponent<RectTransform>().localScale = new Vector3(uiScale, uiScale, uiScale);
        }
        else
        {
            incomingItem.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }


        // update transfer buttons of origin (if applicable)
        //Debug.Log(incomingItemOriginalSlot);
        //Debug.Log(incomingItemOriginalSlot.transform.parent);

        if(incomingItemOriginalSlot.name != "CanvasDragItem")
        {
            foreach (Transform child in incomingItemOriginalSlot.transform.parent)
            {
                //Debug.Log(child.gameObject);
                if (child.gameObject.name == "ButtonTransferIntoContainer")
                {
                    child.gameObject.GetComponent<TransferIntoContainerHandler>().updateButtonActive();
                }

                if (child.gameObject.name == "ButtonTransferOutOfContainer")
                {
                    child.gameObject.GetComponent<TransferOutOfContainerHandler>().updateButtonActive();
                }

                if (child.gameObject.name == "ButtonStartCentrifuge")
                {
                    child.gameObject.GetComponent<CentrifugeHandler>().updateButtonActive();
                }

                if (child.gameObject.name == "ButtonCutIngredient")
                {
                    child.gameObject.GetComponent<CuttingBoardHandler>().updateButtonActive();
                }

                if (child.gameObject.name == "ButtonSubmitInventoryItem")
                {
                    //Debug.Log("Origin");
                    child.gameObject.GetComponent<SubmitAlchemicumButton>().canvas.GetComponent<JobDetails>().UpdateSubmitButton();
                    child.gameObject.GetComponent<SubmitAlchemicumButton>().canvas.GetComponent<JobDetails>().UpdateItemSubmissionInfoText();
                }
            }

            incomingItemOriginalSlot.GetComponent<ItemSlotHandler>().UpdateSlotVisibility();

            // update button if quick takeout from the output itemslot happens
            //Debug.Log(incomingItemOriginalSlot.transform.parent.parent.parent.gameObject);
            foreach (Transform child in incomingItemOriginalSlot.transform.parent.parent.parent)
            {
                if (child.gameObject.name == "PanelTransfer")
                {
                    foreach (Transform child2 in child)
                    {
                        if (child2.gameObject.name == "ButtonStartCentrifuge")
                        {
                            child2.gameObject.GetComponent<CentrifugeHandler>().updateButtonActive();
                        }

                        if (child.gameObject.name == "ButtonCutIngredient")
                        {
                            child.gameObject.GetComponent<CuttingBoardHandler>().updateButtonActive();
                        }
                    }
                }
            }

        }


        

        // update transfer buttons of target (if applicable)
        foreach (Transform child in transform.parent)
        {
            if (child.gameObject.name == "ButtonTransferIntoContainer")
            {
                child.gameObject.GetComponent<TransferIntoContainerHandler>().updateButtonActive();
            }

            if (child.gameObject.name == "ButtonTransferOutOfContainer")
            {
                child.gameObject.GetComponent<TransferOutOfContainerHandler>().updateButtonActive();
            }

            if (child.gameObject.name == "ButtonStartCentrifuge")
            {
                child.gameObject.GetComponent<CentrifugeHandler>().updateButtonActive();
            }

            if (child.gameObject.name == "ButtonCutIngredient")
            {
                child.gameObject.GetComponent<CuttingBoardHandler>().updateButtonActive();
            }

            if (child.gameObject.name == "ButtonSubmitInventoryItem")
            {
                Debug.Log("Target");
                child.gameObject.GetComponent<SubmitAlchemicumButton>().canvas.GetComponent<JobDetails>().UpdateSubmitButton();
                child.gameObject.GetComponent<SubmitAlchemicumButton>().canvas.GetComponent<JobDetails>().UpdateItemSubmissionInfoText();
            }
        }

        UpdateSlotVisibility();



    }
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
        if((eventData.pointerDrag != null)&&!prohibitDrop)
        {
            PutItemIntoSlot(eventData.pointerDrag.GetComponent<InventoryItemHandler>());

        }
        
    }

    public void UpdateSlotVisibility()
    {
        //Debug.Log("Update slot visibility");

        Image image = GetComponent<Image>();
        var tempColor = image.color;

        if (prohibitDrop && (transform.childCount==0))
        {
            tempColor.a = 0.2f;
        }
        else
        {
            tempColor.a = 1f;
        }
        GetComponent<Image>().color = tempColor;
    }

    public void ButtonPress()
    {
    }
}
