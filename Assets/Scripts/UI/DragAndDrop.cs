using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if (originalParent.name == "PanelTransfer")
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
            }
        }
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
        }
    } 


    public void OnPointerDown(PointerEventData eventData)
    {


    }
}
