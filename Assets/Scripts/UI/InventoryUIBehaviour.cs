using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InventoryUIBehaviour : MonoBehaviour
{
    public bool isScrolledUp = false;
    public AudioClip clickSound;
    private AudioSource source;
    public static GameObject panelInventory;
    public static GameObject additionalPanelWithSlots;
    public bool isLocked = false;

    void Start()
    {
        
    }

    void Update()
    {
        // open/close inventory when i is pressed (and nothing is currently being dragged anywhere, i.e. mousebutton0
        if (Input.GetKeyDown("i")&&!DragAndDrop.dragInProgress)
        {
            ChangeInventoryScroll();
        }

        // close inventory when its open and mouse leaves the inventory
        if (isScrolledUp && Input.GetMouseButton(0) && !DragAndDrop.dragInProgress && !isLocked)
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

    public void ChangeInventoryScroll()
    {
        

      

            

            // Inventory is fully visible
            if (isScrolledUp)
            {
                CloseInventory();
            }

            // Only first Inventory line is visible
            else
            {
                OpenInventory();
            }
        
        
    }

    public void OpenInventory(float customMoveY)
    {
        if (!isLocked)
        {
            source = GetComponent<AudioSource>();
            source.PlayOneShot(clickSound, 1f);

            isScrolledUp = true;
            LeanTween.moveY(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), customMoveY, 0.8f).setEaseInOutCubic();
            LeanTween.rotate(GameObject.Find("ButtonArrowInventoryImage").GetComponent<RectTransform>(), 180.0f, 0.1f).setEaseInOutCubic();
        }
    }

    public void OpenInventory()
    {
        OpenInventory(231f);
    

    }

   

    public void CloseInventory()
    {
        if (!isLocked)
        {
            source = GetComponent<AudioSource>();
            source.PlayOneShot(clickSound, 1f);

            isScrolledUp = false;
            LeanTween.moveY(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), -307.8795f, 0.8f).setEaseInOutCubic();
        }
        

    LeanTween.rotate(GameObject.Find("ButtonArrowInventoryImage").GetComponent<RectTransform>(), 180.0f, 0.1f).setEaseInOutCubic(); ;
    }

    public void HideButton()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowButton()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
