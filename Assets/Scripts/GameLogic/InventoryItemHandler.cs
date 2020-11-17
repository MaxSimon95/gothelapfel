using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemHandler : MonoBehaviour
{

    public List<IngredientType> ingredientTypes = new List<IngredientType>();
    public List<int> ingredientTypeAmounts = new List<int>();
    public int amountTotal;


    void Start()
    {    
        UpdateItemContent();
    }

    void Update()
    {
        
    }

    private void SetToolTipText()
    {


        // just one ingredienttype: use its ingredienttype name for tooltip text
        if (ingredientTypes.Count == 1)
        {
            GetComponent<TooltipUITargetHandler>().tooltipText = ingredientTypes[0].GetComponent<IngredientType>().ingredientName;
        }

        // more than one: Say "Unknown Mixture" --> Can be fancied up later on, with all sorts of fun special cases we cover here (any mix of salt + water gets called salt water for example)
        else
        {
            GetComponent<TooltipUITargetHandler>().tooltipText = "Mysterious Mixture";
            //Debug.Log(ingredientTypes.Count);
        }
    }

    public void UpdateItemContent()
    {
        DeleteIngredientIfEmpty();
        MergeIdenticalIngredients();

        amountTotal = ingredientTypeAmounts.Sum();

        if (amountTotal > 0)
            transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = amountTotal.ToString();
        else
        {
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
                transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("ph_ingredient_mix");
            }
        }
            
        else
        {
            Debug.Log("Destroyed because ingredientTypes count 0");
            DeleteInstanceOfInventoryItem();
        }

        
        SetToolTipText();

    }

    private void DeleteIngredientIfEmpty()
    {
        for (int index = ingredientTypeAmounts.Count-1; index >= 0; index--)
        {
            if(ingredientTypeAmounts[index] <= 0 )
            {
                ingredientTypeAmounts.RemoveAt(index);
                ingredientTypes.RemoveAt(index);
                Debug.Log("REMOVED EMPTY INGREDIENT");
            }
        }
    }

    public void AddIngredientAndUpdate(IngredientType ingredientType, int amount)
    {

        AddIngredient(ingredientType, amount);
        UpdateItemContent();

    }

    public void AddIngredient(IngredientType ingredientType, int amount)
    {
        Debug.Log("Adding ingredient");
        Debug.Log(ingredientType);
        Debug.Log(amount);

        ingredientTypes.Add(ingredientType);
        ingredientTypeAmounts.Add(amount);
    }

    private void MergeIdenticalIngredients()
    {
        List<IngredientType> newIngredientTypeList = new List<IngredientType>();
        List<int> newIngredientTypeAmountsList = new List<int>();

        for (int index = 0; index < ingredientTypes.Count; index++)
        {
            IngredientType tempIngredientType = ingredientTypes[index];

            // wenn nicht in neuerliste, dann alle vorkommen dieses typs jetzt in diese liste überführen
            if (!newIngredientTypeList.Contains(tempIngredientType))
            {
                int amountTemp = 0;

                // hochzählen für jedes vorkommen
                for (int index2 = 0; index2 < ingredientTypes.Count; index2++)
                {

                    if (ingredientTypes[index2] == tempIngredientType)
                    {
                        amountTemp += ingredientTypeAmounts[index2];
                    }
                }

                newIngredientTypeList.Add(tempIngredientType);
                newIngredientTypeAmountsList.Add(amountTemp);
                // am ende in neue liste hinzufügen mit zusammengezähltem vorkommen
            }

        }

        // alte liste durch neue ersetzen
        ingredientTypes = newIngredientTypeList;
        ingredientTypeAmounts = newIngredientTypeAmountsList;
        

    }

    public void DeleteInstanceOfInventoryItem()
    {
        // this ensures that the transfer buttons dont get into trouble with their buttonactive checks when they check whether their connected item slot content is == 0
        transform.SetParent(GameObject.Find("CanvasDragItem").transform);

        Destroy(gameObject);
    }

    public void ButtonPress()
    {

    }

}
