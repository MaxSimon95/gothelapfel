using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alchemy.Namespace;

public class InventoryItemHandler : MonoBehaviour
{

    public IngredientTypeHandler ingredientType;
    public int amount;

    // Start is called before the first frame update
    void Start()
    {
        UpdateItemContent();
        GetComponent<TooltipUITargetHandler>().tooltipText = ingredientType.GetComponent<IngredientTypeHandler>().ingredientName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateItemContent()
    {

        // adjust text
        if (amount > 0)
            transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = amount.ToString();
        else
            Destroy(gameObject);

        if (ingredientType != null)
            transform.GetChild(0).gameObject.GetComponent<Image>().sprite = ingredientType.GetComponent<IngredientType>().inventorySprite;
        else
            Destroy(gameObject);
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
