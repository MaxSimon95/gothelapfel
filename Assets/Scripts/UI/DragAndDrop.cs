using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
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

        Debug.Log(originalParent.transform.parent.parent.gameObject.name);
        if (originalParent.transform.parent.parent.gameObject.name == "PanelCentrifugeView")
        {
            foreach (Transform child in originalParent.transform.parent.parent)
            {
                //Debug.Log(child.gameObject.name);
                if (child.gameObject.name == "PanelTransfer")
                {
                    Debug.Log(child.gameObject.name);
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

        Debug.Log(startParent.gameObject.name);
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
        }
    } 


    public void OnPointerDown(PointerEventData eventData)
    {


    }
}
*/

//adjusted version
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
            TransferItemAutomatically(gameObject.GetComponent<InventoryItemHandler>(), transform.parent.GetComponent<ItemSlotHandler>());
        }

    }

    public void TransferItemAutomatically(InventoryItemHandler transferItem, ItemSlotHandler originalSlot)
    {
        GameObject targetPanelInventorySlots = null;
        // check if its in the inventory or anywhere else, and depending on that were to put it now
        //Debug.Log(originalSlot.transform.parent.parent.gameObject.name);
        if (originalSlot.transform.parent.parent.gameObject.name == "PanelInventory")
        {
            // the clicked item is in the inventory, therefore transfer item in the inventory's current secondary inventoryslots-panel 
            // which should be set according to the opened canvas

            // TODO replace provisional solution with actual solution
            foreach (Transform child in GameObject.Find("PanelInventory").transform)
            {
                //Debug.Log(child.gameObject.name);
                if (child.gameObject.name == "PanelInventorySlots")
                {

                    targetPanelInventorySlots = child.gameObject;
                    //Debug.Log(targetPanelInventorySlots.name);
                }
            }
            // END TODO
        }
        else
        {
            // the clicked item is not in the inventory but in a secondary inventoryslots-panel. it shall be transferred to the inventory.
            

            foreach (Transform child in GameObject.Find("PanelInventory").transform)
            {
                //Debug.Log(child.gameObject.name);
                if (child.gameObject.name == "PanelInventorySlots")
                {

                    targetPanelInventorySlots = child.gameObject;
                    //Debug.Log(targetPanelInventorySlots.name);
                }
            }
        }

        ItemSlotHandler targetItemSlot = null;

        // step 1: iterate through all fitting slots and see if there is a free slot
        foreach (Transform child in targetPanelInventorySlots.transform)
        {
            //Debug.Log(child.gameObject.name);
            if (child.transform.childCount == 0)
            {

                targetItemSlot = child.gameObject.gameObject.GetComponent<ItemSlotHandler>();
                //Debug.Log(targetPanelInventorySlots.name);
                break;
            }
        }

        //  step 2: iterate through all fitting slots and see if there's any one which already has the correct ingredient inside so they can be merged, 
        // in that case overwrite previous targetSlot

        //check if the item to be moved has exactly 1 ingredient
        Debug.Log("check if the item to be moved has exactly 1 ingredient: " + (originalSlot.transform.GetChild(0).GetComponent<InventoryItemHandler>().ingredientTypes.Count == 1));
        Debug.Log(originalSlot.transform.GetChild(0));
        Debug.Log(originalSlot.transform.GetChild(0).GetComponent<InventoryItemHandler>().ingredientTypes.Count);
        if (originalSlot.transform.GetChild(0).GetComponent<InventoryItemHandler>().ingredientTypes.Count == 1)
        {
            foreach (Transform child in targetPanelInventorySlots.transform)
            {
            
                //check if the slot has an item
                if (child.transform.childCount == 1)
                {
                    // check if the item in the slot has exactly 1 ingredient
                    if (child.transform.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().ingredientTypes.Count == 1)
                    {
                        Debug.Log("check if the item in the target slot has exactly 1 ingredient: " + (child.transform.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().ingredientTypes.Count == 1));
                        //check if the ingredients match
                        if (child.transform.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().ingredientTypes[0] == originalSlot.transform.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().ingredientTypes[0])
                        {
                            //check that the item in slot is not the exact same item we want to reposition
                            if (child.transform.GetChild(0) != originalSlot.transform.GetChild(0))
                            {

                                targetItemSlot = child.gameObject.gameObject.GetComponent<ItemSlotHandler>();

                                Debug.Log("item slot applicable for merge found" + targetItemSlot);

                                break;
                            }
                        }
                    }
                }
            }
        }

        // step 3: move item into target slot
        if(targetItemSlot != null)
        {
            Debug.Log("target Itemslot for instant transfer set");
            Debug.Log(targetItemSlot);
            /*var colors = GetComponent<Button>().colors;
            colors.normalColor = Color.red;
            GetComponent<Button>().colors = colors; */

            targetItemSlot.PutItemIntoSlot(transferItem);
        }
        else
        {
            Debug.Log("no applicable itemslot for instant transfer found");
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

        //Debug.Log(originalParent.transform.parent.parent.gameObject.name);
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
        }
    }


}