using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CanvasContainerHandler : MonoBehaviour
{

    private float inventoryStartPosition;
    private InventoryUIBehaviour inventoryUIBehaviour;
    public GameObject associatedSceneObject;
    public bool hideInventory;
    public bool hasTransferAmountSelection;
    //public bool closingInProgress = false;
    //public GameObject itemAutoTransferTargetParent;

    public static List<GameObject> sceneUIToggle = new List<GameObject>();

    public static CanvasContainerHandler currentCanvasContainer;


    void Awake()
    {
        transform.GetChild(0).localScale = new Vector3(0, 0, 0);

        sceneUIToggle.Add(GameObject.Find("CanvasClock"));
        sceneUIToggle.Add(GameObject.Find("CanvasFeaturedJobs"));
        sceneUIToggle.Add(GameObject.Find("CanvasTopBar"));
        sceneUIToggle.Add(GameObject.Find("CanvasEnvironmentIndicators"));

    }
 

    void Start()
    {
        inventoryStartPosition = GameObject.Find("PanelInventory").GetComponent<RectTransform>().position.x;
        inventoryUIBehaviour = GameObject.Find("ButtonArrowInventory").GetComponent<InventoryUIBehaviour>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           
            if (transform.GetChild(0).localScale.Equals(new Vector3(1, 1, 1)))
            {
                Debug.Log("Escape key was pressed");
                CloseContainerView();

            }
        }
    }

    public static void SetSceneUIVisible(bool visible)
    {

        foreach(GameObject canvasGo in sceneUIToggle)
        {
            if(visible)
            {
                foreach(Transform goTransform in canvasGo.transform)
                {
                    goTransform.localScale = new Vector3(1, 1, 1);
                }
                
            }
            else
            {
                foreach (Transform goTransform in canvasGo.transform)
                {
                    goTransform.localScale = new Vector3(0, 0, 0);
                }
            }
            //go.SetActive(visible);
        }
    }

    public void OpenContainerView()
    {
        currentCanvasContainer = this;
        Debug.Log(currentCanvasContainer);
        SetSceneUIVisible(false);

        //closingInProgress = false;
        RenderOrderAdjustment.anyOverlayOpen = true;

        //Debug.Log("OpenContainerView");

        /*// set auto transfer target parent, so the auto transfer from the inventory knows where to put items when this canvas has been opened
        if(itemAutoTransferTargetParent != null)
        {
            Debug.Log("AUTOPARENT FATHER SET");
            InventoryItemHandler.SetAutoTransferTargetParent(itemAutoTransferTargetParent);
        }*/
        GetComponent<AutoTransferItemCapability>().PrepareAutoTransferTarget();

        foreach (Transform child in transform.GetChild(0))
        {

            if (child.gameObject.name == "PanelTransfer")
            {
                foreach (Transform child2 in child)
                {

                    if (child2.gameObject.name == "ButtonTransferIntoContainer")
                    {
                        child2.gameObject.GetComponent<TransferIntoContainerHandler>().updateButtonActive();
                    }

                    if (child2.gameObject.name == "ButtonTransferOutOfContainer")
                    {
                        child2.gameObject.GetComponent<TransferOutOfContainerHandler>().updateButtonActive();
                    }

                    if (child2.gameObject.name == "ButtonStartCentrifuge")
                    {
                        child2.gameObject.GetComponent<CentrifugeHandler>().updateButtonActive();
                        child2.gameObject.GetComponent<CentrifugeHandler>().UpdateDisplayedSlotsNumber();
                    }

                    if (child.gameObject.name == "ButtonCutIngredient")
                    {
                        child.gameObject.GetComponent<CuttingBoardHandler>().updateButtonActive();
                    }
                }
            }
        }


        if(gameObject.GetComponent<TripPlanningHandler>() != null)
        {
            gameObject.GetComponent<TripPlanningHandler>().OpenTripPlanner();
        }

        transform.GetChild(0).localScale = new Vector3(1, 1, 1);

        if (hideInventory)
        {
          //  GameObject.Find("PanelInventory").SetActive(false);
        }
        else { 

        GameObject.Find("PanelInventory").GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            GameObject.Find("PanelInventory").GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            GameObject.Find("PanelInventory").GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
            LeanTween.moveX(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), -35, 0);
            inventoryUIBehaviour.OpenInventory(280);
            inventoryUIBehaviour.isLocked = true;
            inventoryUIBehaviour.HideButton();
        }



        if (hasTransferAmountSelection)
        GameObject.Find("PanelTransferAmount").transform.localScale = new Vector3(0.75f, 0.75f, 1);


    }

    public void CloseContainerView()
    {
        RenderOrderAdjustment.anyOverlayOpen = false;
        SetSceneUIVisible(true);

        if (hideInventory)
        {
           // GameObject.Find("PanelInventory").SetActive(true);
        }
        else
        {

        
        GameObject.Find("PanelInventory").GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
            GameObject.Find("PanelInventory").GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
            GameObject.Find("PanelInventory").GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            LeanTween.moveX(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), 35, 0);
            inventoryUIBehaviour.isLocked = false;
            inventoryUIBehaviour.CloseInventory();
            inventoryUIBehaviour.ShowButton();
        }


        transform.GetChild(0).localScale = new Vector3(0, 0, 0);
        
        GameObject.Find("PanelTransferAmount").transform.localScale = new Vector3(0, 0, 0);

        InventoryItemHandler.ResetAutoTransferTargetParent();

    }
    /*
    void OnGUI()
    {
        
        Event e = Event.current;
        // nur zuhören, wenn das zugehörige panel auf 100% skaliertist, d.h. es offen ist.
        if (transform.GetChild(0).localScale.Equals(new Vector3(1, 1, 1)))
        {
            //closingInProgressCounter += 1;
            Debug.Log("Detected key code: " + e.keyCode);
            Debug.Log(gameObject.name);

            if (e.keyCode == KeyCode.Escape)
            {
                CloseContainerView();
            }
            
        }
    }
    */

}
