﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemHandler : MonoBehaviour
{

    public List<IngredientType> ingredientTypes = new List<IngredientType>();
    public List<int> ingredientTypeAmounts = new List<int>();
    private int amountTotal;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ingredientTypes.Count);
        amountTotal = ingredientTypeAmounts.Sum();
        UpdateItemContent();

        // set tooltip text for inventoryItem

        // just one ingredienttype: use its ingredienttype name for tooltip text
        if(ingredientTypes.Count == 1)
        {
            GetComponent<TooltipUITargetHandler>().tooltipText = ingredientTypes[0].GetComponent<IngredientType>().ingredientName;
        }

        // more than one: Say "Unknown Mixture" --> Can be fancied up later on, with all sorts of fun special cases we cover here (any mix of salt + water gets called salt water for example)
        else
        {
            GetComponent<TooltipUITargetHandler>().tooltipText = "Mysterious Mixture";
            Debug.Log(ingredientTypes.Count);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateItemContent()
    {


        // adjust amount text
        if (amountTotal > 0)
            transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = amountTotal.ToString();
        else
        {
            Debug.Log("Destroyed because amounttotal 0");
            Destroy(gameObject);
        }
            

        // adjust inventoryitem sprite
        if (ingredientTypes.Count > 0)
        {
            // if just one ingredienttype , take it's ingredienttype sprite
            if (ingredientTypes.Count == 1)
                transform.GetChild(0).gameObject.GetComponent<Image>().sprite = ingredientTypes[0].GetComponent<IngredientType>().inventorySprite;
            
            // if more than one ingredienttype, take a designated mixture sprite, or do something fancy later on. here we can add FUN little mechanics to make it more fancy as we see fit.
            else
            {
                Debug.Log("NOT YET IMPLEMENTED - MORE THAN 1 INGREDIENT IN INVENTORYITEM - SPRITE");
            }
        }
            
        else
        {
            Debug.Log("Destroyed because ingredientTypes count 0");
                Destroy(gameObject);
        }
            
    }

    public void DeleteInstanceOfInventoryItem()
    {
        Destroy(gameObject);
    }

    public void ButtonPress()
    {
        Debug.Log("InventoryItem Click");
    }

}
