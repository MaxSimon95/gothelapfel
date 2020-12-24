using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyReaction : MonoBehaviour
{

    // Start is called before the first frame update
    public int priority;
    public float minTemperature = -999999f;
    public float maxTemperature = -999999f;
    public List<IngredientType> inputIngredientTypes = new List<IngredientType>();
    public List<int> inputIngredientTypeAmounts = new List<int>();
    public List<IngredientType> outputIngredientTypes = new List<IngredientType>();
    public List<int> outputIngredientTypeAmounts = new List<int>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
