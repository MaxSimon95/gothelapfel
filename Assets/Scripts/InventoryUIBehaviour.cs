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
        if (Input.GetKeyDown("i"))
        {
            ChangeInventoryScroll();
        }

        if (Input.GetMouseButton(0) && isScrolledUp)
        { 

            if (
                !RectTransformUtility.RectangleContainsScreenPoint
                (
                GameObject.Find("PanelInventory").GetComponent<RectTransform>(),
                Input.mousePosition
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
            Debug.Log("IS SCROLLED UP --> SCROLLING DOWN");
            isScrolledUp = false;
            LeanTween.moveY(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), -307.8795f, 0.1f);

            LeanTween.rotate(GameObject.Find("ButtonArrowInventoryImage").GetComponent<RectTransform>(), 180.0f, 0.1f);
        }

        // Only first Inventory line is visible
        else {
            Debug.Log("IS NOT SCROLLED UP --> SCROLLING UP");
            isScrolledUp = true;
            LeanTween.moveY(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), 231f, 0.1f);
            LeanTween.rotate(GameObject.Find("ButtonArrowInventoryImage").GetComponent<RectTransform>(), 180.0f, 0.1f);
        }
    }
}
