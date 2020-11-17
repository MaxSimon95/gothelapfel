using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferContainerHandler : MonoBehaviour
{

    void Start()
    {
 
    }

    void Update()
    {
        
    }

    public GameObject LoadSlotItemIntoScript()
    {
        GameObject returnItem = null;

        foreach (Transform child in transform.parent)
        {
            if (child.gameObject.name == "InventorySlot")
            {
                if (child.childCount > 0)
                {
                    returnItem = child.GetChild(0).gameObject;

                }
                else
                {
                }
            }
        }

        return returnItem;
    }
}
