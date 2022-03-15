using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuttingBoardHandler : MonoBehaviour
{
    private int outputSlotsUnlocked = 9999;
    private bool buttonActive;
    private GameObject inventoryItemInSlot;
    private List<ItemSlotHandler> outputItemSlots = new List<ItemSlotHandler>();


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("START CUTTING BOARD");
        Debug.Log(transform.parent.parent.gameObject.name);

        updateButtonActive();

        // Load output item slots into the variable
        foreach (Transform child in transform.parent.parent)
        {
            if (child.gameObject.name == "PanelCuttingboard")
            {
                Debug.Log(child.gameObject.name);
                foreach (Transform child2 in child)
                {


                    if (child2.gameObject.name == "PanelInventorySlots")
                    {
                        Debug.Log(child2.gameObject.name);
                        foreach (Transform slot in child2)
                        {
                            outputItemSlots.Add(slot.gameObject.GetComponent<ItemSlotHandler>());
                        }



                    }

                }
            }


        }

        /*if (slot.childCount > 0)
       {
           returnItem = child.GetChild(0).gameObject;

       }
       else
       {
       }*/

        //Debug.Log(outputItemSlots.Count);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateDisplayedSlotsNumber()
    {
        int i = 0;
        foreach (Transform child in transform.parent.parent)
        {
            if (child.gameObject.name == "PanelCentrifuge")
            {
                //Debug.Log(child.gameObject.name);
                foreach (Transform child2 in child)
                {


                    if (child2.gameObject.name == "PanelInventorySlots")
                    {
                        //Debug.Log(child2.gameObject.name);
                        foreach (Transform slot in child2)
                        {
                            outputItemSlots.Add(slot.gameObject.GetComponent<ItemSlotHandler>());
                            if (i < outputSlotsUnlocked)
                            {
                                slot.localScale = new Vector3(1, 1, 1);
                            }
                            else
                            {
                                slot.localScale = new Vector3(0, 0, 0);
                            }
                            i++;
                        }



                    }

                }
            }


        }
    }

    public void updateButtonActive()

    {
        buttonActive = true;

        // inactive when theres no item to centrifugize
        if (GetComponent<TransferContainerHandler>().LoadSlotItemIntoScript() == null)
        {
            Debug.Log("inactive because no item");
            buttonActive = false;
        }
        else
        {
            if (GetComponent<TransferContainerHandler>().LoadSlotItemIntoScript().GetComponent<InventoryItemHandler>().ingredientTypes.Count > 1)
            {
                Debug.Log("inactive because mixture");
                buttonActive = false;
            }
            else
            {
                if (GetComponent<TransferContainerHandler>().LoadSlotItemIntoScript().GetComponent<InventoryItemHandler>().ingredientTypes[0].ingredientCutUp == null)
                {
                    Debug.Log("inactive because no cut up found");
                    buttonActive = false;
                }
            }
        }
       

        // inactive when theres alteast one item still in the output slots

        foreach (ItemSlotHandler slot in outputItemSlots)
        {
            if (slot.transform.childCount > 0)
            {
                Debug.Log("inactive because output slots not free");
                buttonActive = false;
            }
                
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

    public void SeparateIngredients()
    {
        GameObject slotInventoryItem = null;

        Debug.Log(GetComponent<TransferContainerHandler>().LoadSlotItemIntoScript());
        inventoryItemInSlot = GetComponent<TransferContainerHandler>().LoadSlotItemIntoScript();
        if (inventoryItemInSlot != null)
        {



            for (int i = 0; i < inventoryItemInSlot.GetComponent<InventoryItemHandler>().ingredientTypes[0].ingredientCutUp.OutputIngredientTypes.Count; i++)
            {


                // make a new item in a free slot if i is less than unlockedslots,
                //otherwise the remaining ingredients just get added into the last item that was created on the last free slot in every loop
                if (i < outputSlotsUnlocked)
                {


                    slotInventoryItem = (GameObject)Instantiate(Resources.Load("InventoryItemPrefab"), new Vector3(0, 0, 0), Quaternion.identity);
                    slotInventoryItem.transform.SetParent(outputItemSlots[i].transform);
                    slotInventoryItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                    slotInventoryItem.GetComponent<RectTransform>().localScale = new Vector3(slotInventoryItem.transform.parent.gameObject.GetComponent<ItemSlotHandler>().uiScale, slotInventoryItem.transform.parent.gameObject.GetComponent<ItemSlotHandler>().uiScale, slotInventoryItem.transform.parent.gameObject.GetComponent<ItemSlotHandler>().uiScale);

                    // throw out the prefab amount and type
                    slotInventoryItem.GetComponent<InventoryItemHandler>().ingredientTypeAmounts.RemoveAt(0);
                    slotInventoryItem.GetComponent<InventoryItemHandler>().ingredientTypes.RemoveAt(0);
                }




                slotInventoryItem.GetComponent<InventoryItemHandler>().AddIngredient(inventoryItemInSlot.GetComponent<InventoryItemHandler>().ingredientTypes[0].ingredientCutUp.OutputIngredientTypes[i], inventoryItemInSlot.GetComponent<InventoryItemHandler>().ingredientTypes[0].ingredientCutUp.OutputIngredientAmounts[i], inventoryItemInSlot.GetComponent<InventoryItemHandler>().temperature);

                slotInventoryItem.GetComponent<InventoryItemHandler>().UpdateItemContent();



                // update slot visuals
                outputItemSlots[i].UpdateSlotVisibility();

            }

            // delete the original item
            inventoryItemInSlot.GetComponent<InventoryItemHandler>().DeleteInstanceOfInventoryItem();


        }


        updateButtonActive();

    }
}
