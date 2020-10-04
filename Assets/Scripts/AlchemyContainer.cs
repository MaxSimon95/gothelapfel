using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyContainer : MonoBehaviour
{

    public List<IngredientType> ingredientTypes = new List<IngredientType>();
    public List<int> ingredientTypeAmounts = new List<int>();
    public float temperature;



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
}
