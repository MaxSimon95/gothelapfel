﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Linq;


public class TransferOutOfContainerHandler : MonoBehaviour
{

    public GameObject container;

    // used to put the output of the container into:
    private GameObject slotInventoryItem;

    private bool buttonActive;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        updateButtonActive();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateButtonActive()
    {

        buttonActive = true;

        if (container.GetComponent<AlchemyContainer>().ingredientTypeAmounts.Sum() == 0)
        {
            buttonActive = false;
        }

        if (buttonActive)
        {

            GetComponent<Button>().interactable = true;
        }
        else
        {

            GetComponent<Button>().interactable = false;
        }

    }

    private void CreateInventoryItemFromPrefab()
    {

        foreach (Transform child in transform.parent)
        {

            if (child.gameObject.name == "InventorySlot")
            {
                slotInventoryItem = (GameObject)Instantiate(Resources.Load("InventoryItemPrefab"), new Vector3(0,0,0), Quaternion.identity);
                slotInventoryItem.transform.SetParent(child);
                slotInventoryItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                slotInventoryItem.GetComponent<RectTransform>().localScale = new Vector3(slotInventoryItem.transform.parent.gameObject.GetComponent<ItemSlotHandler>().uiScale, slotInventoryItem.transform.parent.gameObject.GetComponent<ItemSlotHandler>().uiScale, slotInventoryItem.transform.parent.gameObject.GetComponent<ItemSlotHandler>().uiScale);
                
            }
        }

        slotInventoryItem.GetComponent<InventoryItemHandler>().ingredientTypeAmounts.RemoveAt(0);
        slotInventoryItem.GetComponent<InventoryItemHandler>().ingredientTypes.RemoveAt(0);

        Debug.Log("INSTANTIATE TEMPERATURE: " + slotInventoryItem.GetComponent<InventoryItemHandler>().temperature);

    }

    private void AddIntoExistingItem()
    {
        
        foreach (Transform child in transform.parent)
        {

            if (child.gameObject.name == "InventorySlot")
            {
                slotInventoryItem = child.GetChild(0).gameObject;
            }
        }
    }
    
    public void TransferOutOfContainer()
    {
        List<IngredientType> ingredientTypesInContainer = container.GetComponent<AlchemyContainer>().ingredientTypes;
        List<int> ingredientTypeAmountsinContainer = container.GetComponent<AlchemyContainer>().ingredientTypeAmounts;
        int transferAmount = TransferAmountHandling.currentTransferAmount;

        foreach (Transform child in transform.parent)
        {

            if (child.gameObject.name == "InventorySlot")
            {
                if (child.childCount != 0)
                    AddIntoExistingItem();
                else 
                    CreateInventoryItemFromPrefab();
            }
        }

        // transferamount anpassen, wenn weniger stuff da ist als die schöpfkelle rausholen will
        if (transferAmount > container.GetComponent<AlchemyContainer>().ingredientTypeAmounts.Sum())
        {

            transferAmount = container.GetComponent<AlchemyContainer>().ingredientTypeAmounts.Sum();

        }
        


        // introduce variable that will be lowered each time a bit of an ingredient type gets moved out of the container
        int remainingTransferAmount = transferAmount;
        Debug.Log("____________________________________________________________________");
        Debug.Log("initial total transferAmount: " + remainingTransferAmount);
        Debug.Log("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ");

        for (int index = 0; index < ingredientTypeAmountsinContainer.Count; index++)
        {
            
            int amountInContainer = ingredientTypeAmountsinContainer[index];
            int transfer;
            
            transfer = (int)(Decimal.Divide(amountInContainer, container.GetComponent<AlchemyContainer>().amountTotal) * transferAmount);

            if (transfer > 0)
            {
                remainingTransferAmount -= transfer;
                // reduce amount of ingredients in amountlist

                ingredientTypeAmountsinContainer[index] -= transfer;

                slotInventoryItem.GetComponent<InventoryItemHandler>().AddIngredient(ingredientTypesInContainer[index], transfer, container.GetComponent<AlchemyContainer>().temperature);

            }


            Debug.Log("ingriedient number: " + index + ";  amount in container before transfer: " + amountInContainer + ";  transfer: " + transfer + ";  remaining transfer amount: " + remainingTransferAmount + ";  total amount transferred: " + slotInventoryItem.GetComponent<InventoryItemHandler>().ingredientTypeAmounts.Sum());

        }

        // clean up the 1 unit that might still need to be moved because the division didnt produce integer results
        for (int index = 0; (index < ingredientTypeAmountsinContainer.Count) && (remainingTransferAmount > 0); index++)
        {
            Debug.Log("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ");
            Debug.Log(" clean up round begins! remaining transfer amount: " + remainingTransferAmount);
            if (ingredientTypeAmountsinContainer[index] > 0) 
            {
                slotInventoryItem.GetComponent<InventoryItemHandler>().AddIngredient(ingredientTypesInContainer[index], 1, container.GetComponent<AlchemyContainer>().temperature);
                ingredientTypeAmountsinContainer[index] -= 1;
                remainingTransferAmount -= 1;
            }

            Debug.Log(" clean up round concludes! remaining transfer amount: " + remainingTransferAmount + ";  totalAmount transferred: " + slotInventoryItem.GetComponent<InventoryItemHandler>().amountTotal);
        }

        slotInventoryItem.GetComponent<InventoryItemHandler>().UpdateItemContent();
        container.GetComponent<AlchemyContainer>().UpdateContent();


        updateButtonActive();
        container.GetComponent<AlchemyContainer>().PlaySound();


        foreach (Transform child in transform.parent)
        {

            if (child.gameObject.name == "ButtonTransferIntoContainer")
            {
                child.gameObject.GetComponent<TransferIntoContainerHandler>().updateButtonActive();
            }
        }

        

    }
}
