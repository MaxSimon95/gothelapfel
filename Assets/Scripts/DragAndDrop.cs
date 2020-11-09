using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    
    private RectTransform rectTransform;

    public static GameObject itemBeingDragged;
    public GameObject canvas;
    Vector3 startPosition;
    public Transform startParent;
    public static bool dragInProgress = false;

    // Start is called before the first frame update
    void Start()
    {

        rectTransform = GetComponent<RectTransform>();


    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject originalParent = transform.parent.parent.gameObject;

        //UnityEngine.Debug.Log("Drag go");
        dragInProgress = true;
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        //canvas = GameObject.FindGameObjectWithTag("UI Canvas").transform;
        //Debug.Log("transform.parent = " + transform.parent + "; canvas.transform = " + canvas.transform);
        transform.SetParent(canvas.transform);
        //Debug.Log("transform.parent = " + transform.parent + "; canvas.transform = " + canvas.transform);

        // activate according function in previous itemslot if that was a slot in the transfer panel, so ui adjustments can be made there
        if (originalParent.name == "PanelTransfer")
        {
            Debug.Log("Panel Transfer alright");


            foreach (Transform child in originalParent.transform)
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

    public void OnDrag(PointerEventData eventData)
    {
        //UnityEngine.Debug.Log("Drag");
        //rectTransform.anchoredPosition += eventData.delta; // / canvas.scaleFactor / GameObject.Find("PanelInventory").GetComponent<RectTransform>().localScale;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragInProgress = false;
        //UnityEngine.Debug.Log("Drag stop");
        //Debug.Log(transform.parent + " == " + canvas);
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == canvas.transform)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
    } 

    /*public void ReadjustPosition()
    {
        Debug.Log("transform.position = " + transform.position + "; startPosition = " + startPosition);
        transform.position = startPosition;
    }*/

    public void OnPointerDown(PointerEventData eventData)
    {
        //UnityEngine.Debug.Log("Click Down");

    }
}
