using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CanvasContainerHandler : MonoBehaviour
{
    // Start is called before the first frame update
    //public static GameObject viewGameObject;
    private float inventoryStartPosition;
    private InventoryUIBehaviour inventoryUIBehaviour;


    void Awake()
    {
        transform.GetChild(0).localScale = new Vector3(0, 0, 0);

    }

    void Start()
    {
        inventoryStartPosition = GameObject.Find("PanelInventory").GetComponent<RectTransform>().position.x;
        inventoryUIBehaviour = GameObject.Find("ButtonArrowInventory").GetComponent<InventoryUIBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenContainerView()
    {
        transform.GetChild(0).localScale = new Vector3(1, 1, 1);
        GameObject.Find("PanelInventory").GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        GameObject.Find("PanelInventory").GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        GameObject.Find("PanelInventory").GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
        LeanTween.moveX(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), -35, 0);
        inventoryUIBehaviour.OpenInventory(280);
        inventoryUIBehaviour.isLocked = true;
        inventoryUIBehaviour.HideButton();
        GameObject.Find("PanelTransferAmount").transform.localScale = new Vector3(1, 1, 1);

        // TODO move transferamount panel along  

    }

    public void CloseContainerView()
    {
        Debug.Log("close cauld");
        transform.GetChild(0).localScale = new Vector3(0, 0, 0);
        GameObject.Find("PanelInventory").GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
        GameObject.Find("PanelInventory").GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
        GameObject.Find("PanelInventory").GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        LeanTween.moveX(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), 35, 0);
        inventoryUIBehaviour.isLocked = false;
        inventoryUIBehaviour.CloseInventory();
        inventoryUIBehaviour.ShowButton();
        GameObject.Find("PanelTransferAmount").transform.localScale = new Vector3(0, 0, 0);

        // TODO move transferamount panel along  

    }
}
