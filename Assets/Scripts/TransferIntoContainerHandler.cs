using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;


public class TransferIntoContainerHandler : MonoBehaviour
{

    public GameObject container;
    public GameObject itemSlot;
    public GameObject inventoryItemInSlot;
    //private int transferAmount = TransferAmountHandling.currentTransferAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject LoadSlotItemIntoScript()
    {
        //Debug.Log("UpdateInventoryItemInSlot");
        GameObject returnItem = null;

        foreach (Transform child in transform.parent)
        {
            //Debug.Log(child.gameObject.name);
            if (child.gameObject.name == "InventorySlot")
            {
                if(child.childCount > 0)
                {
                    Debug.Log("There is an item in the slot");
                    returnItem = child.GetChild(0).gameObject;
                    
                }
                else
                {
                    Debug.Log("No item in slot");
                } 
            }
        }
        return returnItem;
    }

    public void TransferIntoContainer()
    {
        inventoryItemInSlot = LoadSlotItemIntoScript();
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
            Debug.Log("_____");
        Debug.Log(" transferAmount = " + transferAmount + "; AmountTotal = " + inventoryItemInSlot.GetComponent<InventoryItemHandler>().amountTotal);
        if (transferAmount > inventoryItemInSlot.GetComponent<InventoryItemHandler>().amountTotal)
        {

            transferAmount = inventoryItemInSlot.GetComponent<InventoryItemHandler>().amountTotal;

            Debug.Log("transferAmount reduziert auf: " + transferAmount);
        }
        Debug.Log("_____");


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
            //ingredientTypeAmountsInSlot[index] = 500;

            // TODO put ingredients into container

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



        //Debug.Log(ingredientTypeAmountsInSlot.Count);

    }
}
