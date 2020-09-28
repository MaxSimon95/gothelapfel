using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCauldronViewUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    //public static GameObject viewGameObject;
    private float inventoryStartPosition;

    void Awake()
    {
        transform.GetChild(0).localScale = new Vector3(0, 0, 0);
    }

    void Start()
    {
        inventoryStartPosition = GameObject.Find("PanelInventory").GetComponent<RectTransform>().position.x;
        //OpenCauldronView();
        //CloseCauldronView();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCauldronView()
    {
        transform.GetChild(0).localScale = new Vector3(1, 1, 1);
        LeanTween.moveX(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), -inventoryStartPosition/2, 1f).setEaseInOutCubic();

    }

    public void CloseCauldronView()
    {
        Debug.Log("close cauld");
        transform.GetChild(0).localScale = new Vector3(0, 0, 0);
        LeanTween.moveX(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), 0, 1f).setEaseInOutCubic();
    }
}
