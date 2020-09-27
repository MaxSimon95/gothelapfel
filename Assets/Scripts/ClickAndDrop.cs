using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    
    private RectTransform rectTransformDrag;
    private RectTransform rectTransformItemSlotImage;
    private RectTransform rectTransformItemSlot;
    private RectTransform rectTransformItemRow;
    //private bool isDragging = false;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        //UnityEngine.Debug.Log("Click Down");
        rectTransformDrag = GameObject.Find("DragImageInventory").GetComponent<RectTransform>();
        rectTransformItemSlotImage = GetComponent<RectTransform>();
        rectTransformItemSlot = GetComponent<RectTransform>().parent.GetComponent<RectTransform>();
        rectTransformItemRow = GetComponent<RectTransform>().parent.GetComponent<RectTransform>().parent.GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
       /* if(isDragging)
        {
            rectTransform.anchoredPosition3D = Input.mousePosition; / canvas.scaleFactor  // - 
               //new Vector3(GameObject.Find("PanelInventory").GetComponent<RectTransform>().position.x, GameObject.Find("PanelInventory").GetComponent<RectTransform>().position.y, 0);
        }
        
        else
        {

        } */
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("Drag go");
        rectTransformDrag.anchoredPosition =
            rectTransformItemSlotImage.anchoredPosition + rectTransformItemSlot.anchoredPosition + rectTransformItemRow.anchoredPosition;

    }

    public void OnDrag(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("Drag");
        rectTransformDrag.anchoredPosition += eventData.delta / canvas.scaleFactor / GameObject.Find("PanelInventory").GetComponent<RectTransform>().localScale;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("Drag stop");
    } 

    public void OnPointerDown(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("Click Down");
        /*if(isDragging)
        {
            isDragging = false;
        }
        else
        {
            isDragging = true;
        } */
    }
}
