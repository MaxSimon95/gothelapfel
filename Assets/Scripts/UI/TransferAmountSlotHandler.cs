using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TransferAmountSlotHandler : MonoBehaviour, IDropHandler
{

    public GameObject inventoryItemPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
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
            }
        }

    }

    public void onPullOut() // pulling out isn't safe! :3
    {
        foreach (Transform child in transform.parent)
        {
            if (child.gameObject.name == "ButtonTransferIntoContainer")
            {
                Debug.Log("Calling updateButtonActive from outside");
                child.gameObject.GetComponent<TransferIntoContainerHandler>().updateButtonActive();
            }

            if (child.gameObject.name == "ButtonTransferOutOfContainer")
            {
                Debug.Log("Calling updateButtonActive from outside");
                child.gameObject.GetComponent<TransferOutOfContainerHandler>().updateButtonActive();
            }
        }
    }

    }
