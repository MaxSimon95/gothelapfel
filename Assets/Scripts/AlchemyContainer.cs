using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyContainer : MonoBehaviour
{

    public List<IngredientType> ingredientTypes = new List<IngredientType>();
    public List<int> ingredientTypeAmounts = new List<int>();
    public float temperature;
    public int capacity;



    // Start is called before the first frame update
    void Start()
    {
        // Add parts to the list.
        //ingredients.Add(new Part() { PartName = "crank arm", PartId = 1234 });
        //ingredients.Add(new Part() { PartName = "chain ring", PartId = 1334 });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddIngredient(IngredientType ingredientType, int amount)
    {
        Debug.Log("Adding");
        Debug.Log(ingredientType);
        Debug.Log(amount);

        ingredientTypes.Add(ingredientType);
        ingredientTypeAmounts.Add(amount);

        MergeIdenticalIngredients();

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
                    
                    if(ingredientTypes[index2] == tempIngredientType)
                    {
                        amountTemp += ingredientTypeAmounts[index2];
                    }
                }

                newIngredientTypeList.Add(tempIngredientType);
                newIngredientTypeAmountsList.Add(amountTemp);
                    // am ende in neue liste hinzufügen mit zusammengezähltem vorkommen
            }

        }

    ingredientTypes = newIngredientTypeList;
    ingredientTypeAmounts = newIngredientTypeAmountsList;

    // alte liste durch neue ersetzen
    }
}
