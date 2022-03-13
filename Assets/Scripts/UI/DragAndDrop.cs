using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    private RectTransform rectTransform;

    public static GameObject itemBeingDragged;
    private GameObject canvas;
    Vector3 startPosition;
    public Transform startParent;
    public static bool dragInProgress = false;

    void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("CanvasDragItem");


    }

    void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Initiate InstantTransfer");
        } */
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Pönter döwn");
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Mouse1))
        {
            //Debug.Log("Initiate InstantTransfer");
            gameObject.GetComponent<InventoryItemHandler>().TransferItemAutomatically();
        }

    }

    

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject originalParent = transform.parent.parent.gameObject;

        dragInProgress = true;
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        transform.SetParent(canvas.transform);


        // activate according function in previous itemslot if that was a slot in the transfer panel, so ui adjustments can be made there
   //     if (originalParent.name == "PanelTransfer")
    if(originalParent.transform.childCount > 0)
        {

            foreach (Transform child in originalParent.transform)
            {
                if (child.gameObject.name == "ButtonTransferIntoContainer")
                {
                    child.gameObject.GetComponent<TransferIntoContainerHandler>().updateButtonActive();
                }

                if (child.gameObject.name == "ButtonTransferOutOfContainer")
                {
                    child.gameObject.GetComponent<TransferOutOfContainerHandler>().updateButtonActive();
                }

                if (child.gameObject.name == "ButtonStartCentrifuge")
                {
                    child.gameObject.GetComponent<CentrifugeHandler>().updateButtonActive();
                }

                if (child.gameObject.name == "ButtonCutIngredient")
                {
                    child.gameObject.GetComponent<CuttingBoardHandler>().updateButtonActive();
                }

                if (child.gameObject.name == "ButtonSubmitInventoryItem")
                {
                    Debug.Log("Take out");
                    child.gameObject.GetComponent<SubmitAlchemicumButton>().canvas.GetComponent<JobDetails>().UpdateSubmitButton();
                    child.gameObject.GetComponent<SubmitAlchemicumButton>().canvas.GetComponent<JobDetails>().UpdateItemSubmissionInfoText();
                }
            }
        }

        //special case centrifuge output slots:
        if (originalParent.transform.parent.parent.gameObject.name == "PanelCentrifugeView")
        {
            foreach (Transform child in originalParent.transform.parent.parent)
            {
                //Debug.Log(child.gameObject.name);
                if (child.gameObject.name == "PanelTransfer")
                {
                   // Debug.Log(child.gameObject.name);
                    foreach (Transform child2 in child)
                    {
                        if (child2.gameObject.name == "ButtonStartCentrifuge")
                        {
                            child2.gameObject.GetComponent<CentrifugeHandler>().updateButtonActive();
                        }

                        
                    }

                }
            }
        }

        //special case centrifuge output slots:
        if (originalParent.transform.parent.parent.gameObject.name == "PanelCuttingboardView")
        {
            foreach (Transform child in originalParent.transform.parent.parent)
            {
                //Debug.Log(child.gameObject.name);
                if (child.gameObject.name == "PanelTransfer")
                {
                    // Debug.Log(child.gameObject.name);
                    foreach (Transform child2 in child)
                    {
                        if (child2.gameObject.name == "ButtonCutIngredient")
                        {
                            child2.gameObject.GetComponent<CuttingBoardHandler>().updateButtonActive();
                        }


                    }

                }
            }
        }

        //Debug.Log(startParent.gameObject.name);
        startParent.gameObject.GetComponent<ItemSlotHandler>().UpdateSlotVisibility();
    }

    public void OnDrag(PointerEventData eventData)
    {

        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragInProgress = false;
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == canvas.transform)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
            startParent.gameObject.GetComponent<ItemSlotHandler>().UpdateSlotVisibility();
        }
    }


}