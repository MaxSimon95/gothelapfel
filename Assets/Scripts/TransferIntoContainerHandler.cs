using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Linq;


public class TransferIntoContainerHandler : MonoBehaviour
{

    public GameObject container;
    //private GameObject itemSlot;
    private GameObject inventoryItemInSlot;
    private bool buttonActive;
    //private int transferAmount = TransferAmountHandling.currentTransferAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        
        //itemSlot = GetComponent<TransferContainerHandler>().itemSlot;
        //updateButtonActive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateButtonActive()
    {

        buttonActive = true;
 
        if (GetComponent<TransferContainerHandler>().LoadSlotItemIntoScript() == null)
        {
            buttonActive = false;
        }

        if (container.GetComponent<AlchemyContainer>().capacity <= container.GetComponent<AlchemyContainer>().ingredientTypeAmounts.Sum())
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

    

    public void TransferIntoContainer()
    {
        inventoryItemInSlot = GetComponent<TransferContainerHandler>().LoadSlotItemIntoScript();
        if (inventoryItemInSlot == null) return;

        List<IngredientType> ingredientTypesInSlot = inventoryItemInSlot.GetComponent<InventoryItemHandler>().ingredientTypes;
        List<int> ingredientTypeAmountsInSlot = inventoryItemInSlot.GetComponent<InventoryItemHandler>().ingredientTypeAmounts;
        int transferAmount = TransferAmountHandling.currentTransferAmount;

        // transferamount anpassen, wenn weniger platz im container da ist als übertragen werden soll
        if (transferAmount > (container.GetComponent<AlchemyContainer>().capacity - container.GetComponent<AlchemyContainer>().ingredientTypeAmounts.Sum()))
        {
            transferAmount = (container.GetComponent<AlchemyContainer>().capacity - container.GetComponent<AlchemyContainer>().ingredientTypeAmounts.Sum());
        }

            // transferamount anpassen, wenn weniger stuff da ist als die schöpfkelle rausholen will
        //    Debug.Log("_____");
        //Debug.Log(" transferAmount = " + transferAmount + "; AmountTotal = " + inventoryItemInSlot.GetComponent<InventoryItemHandler>().amountTotal);
        if (transferAmount > inventoryItemInSlot.GetComponent<InventoryItemHandler>().amountTotal)
        {

            transferAmount = inventoryItemInSlot.GetComponent<InventoryItemHandler>().amountTotal;

            Debug.Log("transferAmount reduziert auf: " + transferAmount);
        }
       // Debug.Log("_____");


        // introduce variable that will be lowered each time a bit of an ingredient type gets moved out of the inventoryitem
        int remainingTransferAmount = transferAmount;

            for(int index=0; index < ingredientTypeAmountsInSlot.Count; index++)
        {
            int amountInSlot = ingredientTypeAmountsInSlot[index];
            int transfer;
            transfer = (int)(Decimal.Divide(amountInSlot, inventoryItemInSlot.GetComponent<InventoryItemHandler>().amountTotal) * transferAmount);
            remainingTransferAmount -= transfer;

            // reduce amount of ingredients in amountlist

            ingredientTypeAmountsInSlot[index] -= transfer;

            //put ingredients into container

            container.GetComponent<AlchemyContainer>().AddIngredient(ingredientTypesInSlot[index], transfer);

            // TODO see if there's cleaning up todo

            // TODO adjust ui feedback on container

        }

            // clean up the 1 unit that might still need to be moved because the division didnt produce integer results
        for (int index = 0; (index < ingredientTypeAmountsInSlot.Count)&&(remainingTransferAmount>0); index++)
        {
            if(ingredientTypeAmountsInSlot[index]>0) // this if statement could be removed once we properly and cleanly removed empty ingredients from the inventoryitem 
            {
                container.GetComponent<AlchemyContainer>().AddIngredient(ingredientTypesInSlot[index], 1);
                ingredientTypeAmountsInSlot[index] -= 1;
                remainingTransferAmount = 0;
            }
        }

            inventoryItemInSlot.GetComponent<InventoryItemHandler>().UpdateItemContent();

        updateButtonActive();

        foreach (Transform child in transform.parent)
        {

            if (child.gameObject.name == "ButtonTransferOutOfContainer")
            {
            //    Debug.Log("Calling updateButtonActive from outside");
                child.gameObject.GetComponent<TransferOutOfContainerHandler>().updateButtonActive();
            }
        }

        //Debug.Log(ingredientTypeAmountsInSlot.Count);

    }
}
