using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CanvasContainerHandler : MonoBehaviour
{

    private float inventoryStartPosition;
    private InventoryUIBehaviour inventoryUIBehaviour;
    public GameObject associatedSceneObject;
    public bool hasTransferAmountSelection;


    void Awake()
    {
        transform.GetChild(0).localScale = new Vector3(0, 0, 0);

    }

    void Start()
    {
        inventoryStartPosition = GameObject.Find("PanelInventory").GetComponent<RectTransform>().position.x;
        inventoryUIBehaviour = GameObject.Find("ButtonArrowInventory").GetComponent<InventoryUIBehaviour>();
    }

    void Update()
    {
        
    }

    public void OpenContainerView()
    {
        //Debug.Log("OpenContainerView");

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
                }
            }
        }

        transform.GetChild(0).localScale = new Vector3(1, 1, 1);

        GameObject.Find("PanelInventory").GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        GameObject.Find("PanelInventory").GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        GameObject.Find("PanelInventory").GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
        LeanTween.moveX(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), -35, 0);
        inventoryUIBehaviour.OpenInventory(280);
        inventoryUIBehaviour.isLocked = true;
        inventoryUIBehaviour.HideButton();
        
        
        if(hasTransferAmountSelection)
        GameObject.Find("PanelTransferAmount").transform.localScale = new Vector3(1, 1, 1);


    }

    public void CloseContainerView()
    {
        //Debug.Log("close cauld");
        transform.GetChild(0).localScale = new Vector3(0, 0, 0);
        GameObject.Find("PanelInventory").GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
        GameObject.Find("PanelInventory").GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
        GameObject.Find("PanelInventory").GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        LeanTween.moveX(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), 35, 0);
        inventoryUIBehaviour.isLocked = false;
        inventoryUIBehaviour.CloseInventory();
        inventoryUIBehaviour.ShowButton();
        GameObject.Find("PanelTransferAmount").transform.localScale = new Vector3(0, 0, 0);

    }
}
