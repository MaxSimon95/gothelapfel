using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CentrifugeHandler : MonoBehaviour
{

    private bool buttonActive;
    private GameObject inventoryItemInSlot;
    private List<ItemSlotHandler> outputItemSlots = new List<ItemSlotHandler>();
     

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.parent.parent.gameObject.name);

        updateButtonActive();

        // Load output item slots into the variable
        foreach (Transform child in transform.parent.parent)
        {
            if (child.gameObject.name == "PanelCentrifuge")
            {
                Debug.Log(child.gameObject.name);
                foreach (Transform child2 in child)
                {
                  
                   
                    if (child2.gameObject.name == "PanelItemSlots")
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

        Debug.Log(outputItemSlots.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateButtonActive()

    {
        Debug.Log("button active test");
        buttonActive = true;

        if (GetComponent<TransferContainerHandler>().LoadSlotItemIntoScript() == null)
        {
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

    public void StartCentrifuge()
    {
        Debug.Log(GetComponent<TransferContainerHandler>().LoadSlotItemIntoScript());
        inventoryItemInSlot = GetComponent<TransferContainerHandler>().LoadSlotItemIntoScript();
        if (inventoryItemInSlot != null)
        {
            inventoryItemInSlot.GetComponent<InventoryItemHandler>().DeleteInstanceOfInventoryItem();
        }

        updateButtonActive();

    }
}
