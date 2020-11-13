using System;
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

        //itemSlot = GetComponent<TransferContainerHandler>().itemSlot;
        updateButtonActive();
        //CreateInventoryItemFromPrefab();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateButtonActive()
    {

        buttonActive = true;
        /*
        Debug.Log("update Out button:");
        Debug.Log(GetComponent<TransferContainerHandler>().LoadSlotItemIntoScript() != null);
        
        if (GetComponent<TransferContainerHandler>().LoadSlotItemIntoScript() != null)
        {
            buttonActive = false;
        }
        */




        //Debug.Log(container.GetComponent<AlchemyContainer>().ingredientTypeAmounts.Sum() == 0);
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

        // TODO
        foreach (Transform child in transform.parent)
        {

            if (child.gameObject.name == "InventorySlot")
            {
                //newInventoryItem = Instantiate(child.gameObject.GetComponent<TransferAmountSlotHandler>().inventoryItemPrefab);
                slotInventoryItem = (GameObject)Instantiate(Resources.Load("InventoryItemPrefab"), new Vector3(0,0,0), Quaternion.identity);
                //Debug.Log(newInventoryItem.GetComponent<RectTransform>().position);
                slotInventoryItem.transform.SetParent(child);
                slotInventoryItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                //Debug.Log(newInventoryItem.transform.parent.gameObject.GetComponent<ItemSlotHandler>().uiScale);
                slotInventoryItem.GetComponent<RectTransform>().localScale = new Vector3(slotInventoryItem.transform.parent.gameObject.GetComponent<ItemSlotHandler>().uiScale, slotInventoryItem.transform.parent.gameObject.GetComponent<ItemSlotHandler>().uiScale, slotInventoryItem.transform.parent.gameObject.GetComponent<ItemSlotHandler>().uiScale);
                
            }
        }

        slotInventoryItem.GetComponent<InventoryItemHandler>().ingredientTypeAmounts.RemoveAt(0);
        slotInventoryItem.GetComponent<InventoryItemHandler>().ingredientTypes.RemoveAt(0);

    }

    private void AddIntoExistingItem()
    {

        Debug.Log(slotInventoryItem);
        
        foreach (Transform child in transform.parent)
        {

            if (child.gameObject.name == "InventorySlot")
            {
                Debug.Log(child.GetChild(0).gameObject);
                slotInventoryItem = child.GetChild(0).gameObject;
            }
        }
        Debug.Log(slotInventoryItem);
    }
    
    public void TransferOutOfContainer()
    {
        Debug.Log("TransferOutofcontainer BEGIN");
        //inventoryItemInSlot = GetComponent<TransferContainerHandler>().LoadSlotItemIntoScript();
        //if (inventoryItemInSlot == null) return;

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

        // create new inventory item in slot
        //CreateInventoryItemFromPrefab();
        

        // remove provisional content of that item, which it got because otherwise it wouldve been cleaned up (deleted) right after creation due to emptyness


        // transferamount anpassen, wenn weniger stuff da ist als die schöpfkelle rausholen will

        //Debug.Log(" transferAmount = " + transferAmount + "; AmountTotal = " + inventoryItemInSlot.GetComponent<InventoryItemHandler>().amountTotal);
        if (transferAmount > container.GetComponent<AlchemyContainer>().ingredientTypeAmounts.Sum())
        {

            transferAmount = container.GetComponent<AlchemyContainer>().ingredientTypeAmounts.Sum();

            Debug.Log("transferAmount reduziert auf: " + transferAmount);
        }
        


        // introduce variable that will be lowered each time a bit of an ingredient type gets moved out of the container
        int remainingTransferAmount = transferAmount;

        Debug.Log("Amount Total:");
        Debug.Log(container.GetComponent<AlchemyContainer>().amountTotal);
        for (int index = 0; index < ingredientTypeAmountsinContainer.Count; index++)
        {
            int amountInContainer = ingredientTypeAmountsinContainer[index];
            int transfer;
            
            transfer = (int)(Decimal.Divide(amountInContainer, container.GetComponent<AlchemyContainer>().amountTotal) * transferAmount);
            
            remainingTransferAmount -= transfer;

            // reduce amount of ingredients in amountlist

            ingredientTypeAmountsinContainer[index] -= transfer;

            //TODO put ingredients into previously newly created inventoryitem

            slotInventoryItem.GetComponent<InventoryItemHandler>().AddIngredient(ingredientTypesInContainer[index], transfer);

            //container.GetComponent<AlchemyContainer>().AddIngredient(ingredientTypesInContainer[index], transfer);

            // TODO see if there's cleaning up todo

            // TODO adjust ui feedback on container

        }
        
        // clean up the 1 unit that might still need to be moved because the division didnt produce integer results
        for (int index = 0; (index < ingredientTypeAmountsinContainer.Count) && (remainingTransferAmount > 0); index++)
        {
            if (ingredientTypeAmountsinContainer[index] > 0) // this if statement could be removed once we properly and cleanly removed empty ingredients from the inventoryitem 
            {
                //container.GetComponent<AlchemyContainer>().AddIngredient(ingredientTypeAmountsinContainer[index], 1);
                slotInventoryItem.GetComponent<InventoryItemHandler>().AddIngredient(ingredientTypesInContainer[index], 1);
                ingredientTypeAmountsinContainer[index] -= 1;
                remainingTransferAmount = 0;
            }
        }

        slotInventoryItem.GetComponent<InventoryItemHandler>().UpdateItemContent();
        container.GetComponent<AlchemyContainer>().UpdateContent();


        updateButtonActive();

        foreach (Transform child in transform.parent)
        {

            if (child.gameObject.name == "ButtonTransferIntoContainer")
            {
                //Debug.Log("Calling updateButtonActive from TRANSFERINTOCONTAINER");
                child.gameObject.GetComponent<TransferIntoContainerHandler>().updateButtonActive();
            }
        }

        //Debug.Log(ingredientTypeAmountsInSlot.Count);

    }
}
