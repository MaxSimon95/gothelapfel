using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientType : MonoBehaviour
{

    public bool knownToPlayer;
    public bool AlwaysHideFromNotebookView;

    public string ingredientName;
    public Color color;
    public Sprite inventorySprite;
    public AudioClip ingredientSound;
    public string description;
    public bool burnsToAsh;

    public float meltingTemperature;
    public float boilingTemperature;


    public List<IngredientEffect> effects = new List<IngredientEffect>();
    public List<int> effectIntensities = new List<int>();

    public List<AlchemyReaction> reactionsOutput = new List<AlchemyReaction>();
    public List<AlchemyReaction> reactionsInput = new List<AlchemyReaction>();

    private GameObject alchemyReactions;

    public IngredientCutUp ingredientCutUp;

    public int standardGatherAmount;

    void Start()
    {
        alchemyReactions = GameObject.Find("AlchemyReactions");
        UpdateReactionsOutput();
        UpdateReactionsInput();
    }

    void Update()
    {
        
    }

    public void UpdateReactionsOutput()
    {
        //Debug.Log("UpdateReactionsOutput START");

        reactionsOutput.Clear();

        foreach (Transform child in alchemyReactions.transform)
        {
            AlchemyReaction currentReaction = child.gameObject.GetComponent<AlchemyReaction>();


            // check if the reaction is "known to the player"
            if(currentReaction.knownToPlayer)
            {
                // check if the reaction is "always hide from notebook view"
                if (!currentReaction.AlwaysHideFromNotebookView)
                {
                    // check if the current ingredient is part of the output of the currently checked reaction
                    foreach (IngredientType outputIngredient in currentReaction.outputIngredientTypes)
                    {
                        if(outputIngredient == gameObject.GetComponent<IngredientType>())
                        {
                            reactionsOutput.Add(currentReaction);
                        }
                    }
                }
            }
            

            

        }

    }

    public void UpdateReactionsInput()
    {
       // Debug.Log("UpdateReactionsInput START");

        reactionsInput.Clear();

        foreach (Transform child in alchemyReactions.transform)
        {
            AlchemyReaction currentReaction = child.gameObject.GetComponent<AlchemyReaction>();


            // check if the reaction is "known to the player"
            if (currentReaction.knownToPlayer)
            {
                // check if the reaction is "always hide from notebook view"
                if (!currentReaction.AlwaysHideFromNotebookView)
                {
                    // check if the current ingredient is part of the output of the currently checked reaction
                    foreach (IngredientType inputIngredient in currentReaction.inputIngredientTypes)
                    {
                        if (inputIngredient == gameObject.GetComponent<IngredientType>())
                        {
                            reactionsInput.Add(currentReaction);
                        }
                    }
                }
            }

        }
    }

    public void SetKnownToPlayer()
    {
        knownToPlayer = true;
        GameObject.Find("PanelNotificationFlag").GetComponent<NotificationFlagHandler>().AddNotificationToQueue(new Notification(this));
    }
}
