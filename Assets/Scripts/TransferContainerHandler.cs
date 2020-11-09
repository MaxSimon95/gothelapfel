using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferContainerHandler : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject LoadSlotItemIntoScript()
    {
        //Debug.Log("UpdateInventoryItemInSlot");
        GameObject returnItem = null;

        foreach (Transform child in transform.parent)
        {
            //Debug.Log(child.gameObject.name);
            if (child.gameObject.name == "InventorySlot")
            {
                if (child.childCount > 0)
                {
                    //Debug.Log("There is an item in the slot");
                    returnItem = child.GetChild(0).gameObject;

                }
                else
                {
                    //Debug.Log("No item in slot");
                }
            }
        }

        return returnItem;
    }
}
