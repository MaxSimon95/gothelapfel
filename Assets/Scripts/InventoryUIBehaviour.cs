using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InventoryUIBehaviour : MonoBehaviour
{
    public bool isScrolledUp = false;
    public AudioClip clickSound;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // open/close inventory when i is pressed (and nothing is currently being dragged anywhere, i.e. mousebutton0
        if (Input.GetKeyDown("i")&&!DragAndDrop.dragInProgress)
        {
            ChangeInventoryScroll();
        }

        // close inventory when its open and mouse leaves the inventory
        if (isScrolledUp && Input.GetMouseButton(0) && !DragAndDrop.dragInProgress)
        { 

            if (
                (
                !RectTransformUtility.RectangleContainsScreenPoint
                (
                GameObject.Find("PanelInventory").GetComponent<RectTransform>(),
                Input.mousePosition
                )
                )
                &&
                (
                !RectTransformUtility.RectangleContainsScreenPoint
                (
                GameObject.Find("PanelSecondaryTransferAmountSelection").GetComponent<RectTransform>(),
                Input.mousePosition
                )
                )
               )
                {
                ChangeInventoryScroll();
            }
        }
    }

   /* public void OnDeselect(BaseEventData eventData)
    {
        if (isScrolledUp)
        {
            ChangeInventoryScroll();
        }
        
    } */

    public void ChangeInventoryScroll()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(clickSound, 1f);

        // Inventory is fully visible
        if (isScrolledUp)
        {
            //Debug.Log("IS SCROLLED UP --> SCROLLING DOWN");
            isScrolledUp = false;
            LeanTween.moveY(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), -307.8795f, 0.1f);

            LeanTween.rotate(GameObject.Find("ButtonArrowInventoryImage").GetComponent<RectTransform>(), 180.0f, 0.1f);
        }

        // Only first Inventory line is visible
        else {
            //Debug.Log("IS NOT SCROLLED UP --> SCROLLING UP");
            isScrolledUp = true;
            LeanTween.moveY(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), 231f, 0.1f);
            LeanTween.rotate(GameObject.Find("ButtonArrowInventoryImage").GetComponent<RectTransform>(), 180.0f, 0.1f);
        }
    }

}
