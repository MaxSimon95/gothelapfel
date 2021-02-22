using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookRecipeDetails : MonoBehaviour
{

    public AlchemyReaction recipe;

    public GameObject UIname;
    public GameObject UIimage_withDescription;
    public GameObject UIimage_noDescription;
    public GameObject UIdescription;

    public Transform inputIngredientsPanelParent;
    public Transform outputIngredientsPanelParent;

    public Transform UIrequirementPanel_alchemyContainer;
    public Transform UIrequirementPanel_temperature;
    public Transform UIrequirementPanel_ventilation;
    public Transform UIrequirementPanel_light;
    public Transform UIrequirementPanel_moonphase;
    public Transform UIrequirementPanel_timeOfDay;
    public Transform UIrequirementPanel_demonicpresence;

    private List<Transform> inputIngredientsPanels = new List<Transform>();
    private List<Transform> outputIngredientsPanels = new List<Transform>();
    private List<IngredientType> inputIngredients = new List<IngredientType>();
    private List<IngredientType> outputIngredients = new List<IngredientType>();
    private List<int> inputIngredientAmounts = new List<int>();
    private List<int> outputIngredientAmounts = new List<int>();


    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in outputIngredientsPanelParent)
        {
            outputIngredientsPanels.Add(child);
        }

        foreach (Transform child in inputIngredientsPanelParent)
        {
            inputIngredientsPanels.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(AlchemyReaction pReaction)
    {
        Debug.Log("Opening 2");
        recipe = pReaction;
        Debug.Log(recipe);
        UpdateRecipeDetails();
        GetComponent<NotebookBaseUI>().Open();
        Debug.Log(recipe.reactionName);
    }

    public void UpdateRecipeDetails()
    {
        Debug.Log("Update Recipe Details ");
        inputIngredients = recipe.inputIngredientTypes;
        outputIngredients = recipe.outputIngredientTypes;

        inputIngredientAmounts = recipe.inputIngredientTypeAmounts;
        outputIngredientAmounts = recipe.outputIngredientTypeAmounts;

        UIname.GetComponent<UnityEngine.UI.Text>().text = recipe.reactionName;

        if (recipe.description == "")
        {
            UIdescription.transform.localScale = new Vector3(0, 0, 0);
            //UIimage_noDescription.GetComponent<UnityEngine.UI.Image>().sprite = ingredient.inventorySprite;
            UIimage_withDescription.transform.localScale = new Vector3(0, 0, 0);
            UIimage_noDescription.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {

            //UIimage_withDescription.GetComponent<UnityEngine.UI.Image>().sprite = recipe.inventorySprite;
            UIimage_withDescription.transform.localScale = new Vector3(1, 1, 1);
            UIimage_noDescription.transform.localScale = new Vector3(0, 0, 0);
            UIdescription.transform.localScale = new Vector3(1, 1, 1);
            UIdescription.GetComponent<UnityEngine.UI.Text>().text = recipe.description;
        }

        for (int i = 0; i < inputIngredientsPanels.Count; i++)
        {
            inputIngredientsPanels[i].gameObject.SetActive(false);

            //Debug.Log(i);

            //Debug.Log(JobsManagement.activeJobList.Count);
            if (i < inputIngredients.Count)
            {
                inputIngredientsPanels[i].gameObject.SetActive(true);
                //Debug.Log(i + "set to visible, local scale: " + effectPanels[i].localScale);
                //Debug.Log(i);
                // CAREFUL: THIS STUFF IS ORDERING SENSITIVE. YOU MESS WITH THE ORDERING, YOU MESS WITH THE CONTENTS, YO! 
                //Debug.Log(i);


                inputIngredientsPanels[i].GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = inputIngredientAmounts[i].ToString();
                inputIngredientsPanels[i].GetChild(2).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = inputIngredients[i].inventorySprite;
                inputIngredientsPanels[i].GetChild(4).gameObject.GetComponent<UnityEngine.UI.Text>().text = inputIngredients[i].ingredientName;

            }



        }

        for (int i = 0; i < outputIngredientsPanels.Count; i++)
        {
            outputIngredientsPanels[i].gameObject.SetActive(false);

            //Debug.Log(i);

            //Debug.Log(JobsManagement.activeJobList.Count);
            if (i < outputIngredients.Count)
            {
                outputIngredientsPanels[i].gameObject.SetActive(true);
                //Debug.Log(i + "set to visible, local scale: " + effectPanels[i].localScale);
                //Debug.Log(i);
                // CAREFUL: THIS STUFF IS ORDERING SENSITIVE. YOU MESS WITH THE ORDERING, YOU MESS WITH THE CONTENTS, YO! 
                //Debug.Log(i);

                outputIngredientsPanels[i].GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = outputIngredientAmounts[i].ToString();
                outputIngredientsPanels[i].GetChild(2).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = outputIngredients[i].inventorySprite;
                outputIngredientsPanels[i].GetChild(4).gameObject.GetComponent<UnityEngine.UI.Text>().text = outputIngredients[i].ingredientName;
            }



        }
    }
}
