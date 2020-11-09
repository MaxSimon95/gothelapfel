using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AlchemyContainer : MonoBehaviour
{

    public List<IngredientType> ingredientTypes = new List<IngredientType>();
    public List<int> ingredientTypeAmounts = new List<int>();
    public float temperature;
    public int capacity;
    public int amountTotal;



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

        UpdateContent();

    }

    private void DeleteIngredientIfEmpty()
    {
        for (int index = ingredientTypeAmounts.Count - 1; index >= 0; index--)
        {
            if (ingredientTypeAmounts[index] <= 0)
            {
                ingredientTypeAmounts.RemoveAt(index);
                ingredientTypes.RemoveAt(index);
            }
        }
    }

    public void UpdateContent()
    {
        MergeIdenticalIngredients();

        // only relevant if container is really empty, everything else gets solved by merging
        DeleteIngredientIfEmpty();

        Debug.Log("Update Container Content");
        amountTotal = ingredientTypeAmounts.Sum();

        // adjust amount text
        /*if (amountTotal > 0)
            transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = amountTotal.ToString();
        else
        {
            Debug.Log("Destroyed because amounttotal 0");
            Destroy(gameObject);
        } */


        // adjust inventoryitem sprite
        /*if (ingredientTypes.Count > 0)
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
            Destroy(gameObject);
        }*/

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
