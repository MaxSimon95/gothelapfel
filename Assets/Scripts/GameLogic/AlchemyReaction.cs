using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyReaction : MonoBehaviour
{
    public bool knownToPlayer;
    public bool AlwaysHideFromNotebookView;

    public string reactionName;

    // Start is called before the first frame update
    public int priority;
    public float minTemperature = -999999f;
    public float maxTemperature = -999999f;
    public List<string> validAlchemyContainers;

    public List<IngredientType> inputIngredientTypes = new List<IngredientType>();
    public List<int> inputIngredientTypeAmounts = new List<int>();
    public List<IngredientType> outputIngredientTypes = new List<IngredientType>();
    public List<int> outputIngredientTypeAmounts = new List<int>();

    void Start()
    {
        if (reactionName == "")
            reactionName = "!Reaction name not set: " + name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
