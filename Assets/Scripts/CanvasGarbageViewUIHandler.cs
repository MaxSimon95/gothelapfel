using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGarbageViewUIHandler : MonoBehaviour
{
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
        //OpenCauldronView();
        //CloseCauldronView();
    }

    public void OpenCauldronView()
    {
        transform.GetChild(0).localScale = new Vector3(1, 1, 1);
        //LeanTween.moveX(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), -inventoryStartPosition/2, 1f).setEaseInOutCubic();
        GameObject.Find("PanelInventory").GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        GameObject.Find("PanelInventory").GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        GameObject.Find("PanelInventory").GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
        LeanTween.moveX(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), -35, 0);
        //LeanTween.moveY(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), 280, 1);
        inventoryUIBehaviour.OpenInventory(280);
        inventoryUIBehaviour.isLocked = true;
        inventoryUIBehaviour.HideButton();

    }

    public void CloseCauldronView()
    {
        Debug.Log("close cauld");
        transform.GetChild(0).localScale = new Vector3(0, 0, 0);
        GameObject.Find("PanelInventory").GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
        GameObject.Find("PanelInventory").GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
        GameObject.Find("PanelInventory").GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        LeanTween.moveX(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), 35, 0);
        //LeanTween.moveY(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), -20, 1);
        //LeanTween.moveX(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), 0, 1f).setEaseInOutCubic();
        inventoryUIBehaviour.isLocked = false;
        inventoryUIBehaviour.CloseInventory();
        inventoryUIBehaviour.ShowButton();



    }
}
