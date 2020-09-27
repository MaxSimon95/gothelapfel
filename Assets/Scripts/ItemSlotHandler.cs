using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotHandler : MonoBehaviour
{
    public IngredientType ingredientType;
    public int amount;

    // Start is called before the first frame update
    void Start()
    {
        UpdateDisplayedSlotContent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDisplayedSlotContent()
    {
       // Debug.Log("IUüdate displayed slot content");

        // adjust text
        if (amount > 0)
            transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = amount.ToString();
        else
            transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = " ";

        if(ingredientType != null)
        transform.GetChild(0).gameObject.GetComponent<Image>().sprite = ingredientType.GetComponent<IngredientType>().inventorySprite;
        else
        transform.GetChild(0).gameObject.GetComponent<Image>().sprite = null;


    }
}
