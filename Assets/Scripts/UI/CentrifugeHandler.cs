using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CentrifugeHandler : MonoBehaviour
{
    public int outputSlotsUnlocked = 4;
    private bool buttonActive;
    private GameObject inventoryItemInSlot;
    private List<ItemSlotHandler> outputItemSlots = new List<ItemSlotHandler>();
     

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(transform.parent.parent.gameObject.name);

        updateButtonActive();

        // Load output item slots into the variable
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
       // UpdateDisplayedSlotsNumber();
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
                            if(i< outputSlotsUnlocked)
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
       Debug.Log("button active test");
        buttonActive = true;

        // inactive when theres no item to centrifugize
        if (GetComponent<TransferContainerHandler>().LoadSlotItemIntoScript() == null)
        {
            buttonActive = false;
        }

        // inactive when theres alteast one item still in the output slots
       
        foreach(ItemSlotHandler slot in outputItemSlots)
        {
            if(slot.transform.childCount>0)
                buttonActive = false;
        }
        
        /* if (container.GetComponent<AlchemyContainer>().capacity <= container.GetComponent<AlchemyContainer>().ingredientTypeAmounts.Sum())
        {
            buttonActive = false;
        } */

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

            
            //int targeted_slot;
            for (int i = 0; i < inventoryItemInSlot.GetComponent<InventoryItemHandler>().ingredientTypeAmounts.Count; i++)
            {
                
                if (i < outputSlotsUnlocked)
                {
                    //targeted_slot = i;

                    slotInventoryItem = (GameObject)Instantiate(Resources.Load("InventoryItemPrefab"), new Vector3(0, 0, 0), Quaternion.identity);
                    slotInventoryItem.transform.SetParent(outputItemSlots[i].transform);
                    slotInventoryItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                    slotInventoryItem.GetComponent<RectTransform>().localScale = new Vector3(slotInventoryItem.transform.parent.gameObject.GetComponent<ItemSlotHandler>().uiScale, slotInventoryItem.transform.parent.gameObject.GetComponent<ItemSlotHandler>().uiScale, slotInventoryItem.transform.parent.gameObject.GetComponent<ItemSlotHandler>().uiScale);

                    slotInventoryItem.GetComponent<InventoryItemHandler>().ingredientTypeAmounts.RemoveAt(0);
                    slotInventoryItem.GetComponent<InventoryItemHandler>().ingredientTypes.RemoveAt(0);
                }
                // handle the case that there are more ingredienttypes than output slots present: stuff all the remaining types into one mixture
                else
                {

                }

                    //Debug.Log("i = " + i);
                // (i < inventoryItemInSlot.GetComponent<InventoryItemHandler>().ingredientTypeAmounts.Count)
               

                slotInventoryItem.GetComponent<InventoryItemHandler>().AddIngredient(inventoryItemInSlot.GetComponent<InventoryItemHandler>().ingredientTypes[i], inventoryItemInSlot.GetComponent<InventoryItemHandler>().ingredientTypeAmounts[i], inventoryItemInSlot.GetComponent<InventoryItemHandler>().temperature);

                slotInventoryItem.GetComponent<InventoryItemHandler>().UpdateItemContent();
                //slotInventoryItem.GetComponent<InventoryItemHandler>().UpdateTemperature();
                

                

            }

            inventoryItemInSlot.GetComponent<InventoryItemHandler>().DeleteInstanceOfInventoryItem();


        }


        updateButtonActive();

    }
}
