using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookRecipeDetails : MonoBehaviour
{

    public AlchemyReaction recipe;

    public GameObject UIname;
    public GameObject UIimageMain;
    public GameObject UIimage_noDescription;
    public GameObject UIdescription;

    public Transform inputIngredientsPanelParent;
    public Transform outputIngredientsPanelParent;

    private List<Transform> inputIngredientsPanels = new List<Transform>();
    private List<Transform> outputIngredientsPanels = new List<Transform>();
    private List<IngredientType> inputIngredients = new List<IngredientType>();
    private List<IngredientType> ouputIngredients = new List<IngredientType>();
    private List<int> inputIngredientAmounts = new List<int>();
    private List<int> outputIngredientAmounts = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(AlchemyReaction pReaction)
    {

        recipe = pReaction;
        UpdateRecipeDetails();
        GetComponent<NotebookBaseUI>().Open();
        Debug.Log(recipe.reactionName);
    }

    public void UpdateRecipeDetails()
    {

    }
}
