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

    public NotebookIngredientDetails notebookIngredientDetails;


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

        // set input ingredients
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

        // set output ingredients
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


        // set requirements

        // 1) display all requirements as inactive
        float alphaInactive = 0.2f;

        UIrequirementPanel_alchemyContainer.GetComponent<UnityEngine.UI.Image>().color = new Color(UIrequirementPanel_alchemyContainer.GetComponent<UnityEngine.UI.Image>().color.r, UIrequirementPanel_alchemyContainer.GetComponent<UnityEngine.UI.Image>().color.g, UIrequirementPanel_alchemyContainer.GetComponent<UnityEngine.UI.Image>().color.b, alphaInactive);
        UIrequirementPanel_alchemyContainer.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(UIrequirementPanel_alchemyContainer.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.r, UIrequirementPanel_alchemyContainer.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.g, UIrequirementPanel_alchemyContainer.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.b, alphaInactive);
        UIrequirementPanel_alchemyContainer.GetChild(1).GetComponent<UnityEngine.UI.Text>().color = new Color(UIrequirementPanel_alchemyContainer.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.r, UIrequirementPanel_alchemyContainer.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.g, UIrequirementPanel_alchemyContainer.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.b, alphaInactive);
        UIrequirementPanel_alchemyContainer.GetChild(2).GetComponent<UnityEngine.UI.Text>().color = new Color(UIrequirementPanel_alchemyContainer.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.r, UIrequirementPanel_alchemyContainer.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.g, UIrequirementPanel_alchemyContainer.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.b, alphaInactive);
        UIrequirementPanel_alchemyContainer.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "Irrelevant";


        UIrequirementPanel_temperature.GetComponent<UnityEngine.UI.Image>().color = new Color(UIrequirementPanel_temperature.GetComponent<UnityEngine.UI.Image>().color.r, UIrequirementPanel_temperature.GetComponent<UnityEngine.UI.Image>().color.g, UIrequirementPanel_temperature.GetComponent<UnityEngine.UI.Image>().color.b, alphaInactive);
        UIrequirementPanel_temperature.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(UIrequirementPanel_temperature.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.r, UIrequirementPanel_temperature.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.g, UIrequirementPanel_temperature.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.b, alphaInactive);
        UIrequirementPanel_temperature.GetChild(1).GetComponent<UnityEngine.UI.Text>().color = new Color(UIrequirementPanel_temperature.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.r, UIrequirementPanel_temperature.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.g, UIrequirementPanel_temperature.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.b, alphaInactive);
        UIrequirementPanel_temperature.GetChild(2).GetComponent<UnityEngine.UI.Text>().color = new Color(UIrequirementPanel_temperature.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.r, UIrequirementPanel_temperature.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.g, UIrequirementPanel_temperature.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.b, alphaInactive);
        UIrequirementPanel_temperature.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "Irrelevant";

        UIrequirementPanel_ventilation.GetComponent<UnityEngine.UI.Image>().color = new Color(UIrequirementPanel_ventilation.GetComponent<UnityEngine.UI.Image>().color.r, UIrequirementPanel_ventilation.GetComponent<UnityEngine.UI.Image>().color.g, UIrequirementPanel_ventilation.GetComponent<UnityEngine.UI.Image>().color.b, alphaInactive);
        UIrequirementPanel_ventilation.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(UIrequirementPanel_ventilation.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.r, UIrequirementPanel_ventilation.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.g, UIrequirementPanel_ventilation.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.b, alphaInactive);
        UIrequirementPanel_ventilation.GetChild(1).GetComponent<UnityEngine.UI.Text>().color = new Color(UIrequirementPanel_ventilation.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.r, UIrequirementPanel_ventilation.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.g, UIrequirementPanel_ventilation.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.b, alphaInactive);
        UIrequirementPanel_ventilation.GetChild(2).GetComponent<UnityEngine.UI.Text>().color = new Color(UIrequirementPanel_ventilation.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.r, UIrequirementPanel_ventilation.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.g, UIrequirementPanel_ventilation.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.b, alphaInactive);
        UIrequirementPanel_ventilation.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "Irrelevant";

        UIrequirementPanel_light.GetComponent<UnityEngine.UI.Image>().color = new Color(UIrequirementPanel_light.GetComponent<UnityEngine.UI.Image>().color.r, UIrequirementPanel_light.GetComponent<UnityEngine.UI.Image>().color.g, UIrequirementPanel_light.GetComponent<UnityEngine.UI.Image>().color.b, alphaInactive);
        UIrequirementPanel_light.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(UIrequirementPanel_light.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.r, UIrequirementPanel_light.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.g, UIrequirementPanel_light.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.b, alphaInactive);
        UIrequirementPanel_light.GetChild(1).GetComponent<UnityEngine.UI.Text>().color = new Color(UIrequirementPanel_light.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.r, UIrequirementPanel_light.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.g, UIrequirementPanel_light.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.b, alphaInactive);
        UIrequirementPanel_light.GetChild(2).GetComponent<UnityEngine.UI.Text>().color = new Color(UIrequirementPanel_light.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.r, UIrequirementPanel_light.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.g, UIrequirementPanel_light.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.b, alphaInactive);
        UIrequirementPanel_light.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "Irrelevant";

        UIrequirementPanel_moonphase.GetComponent<UnityEngine.UI.Image>().color = new Color(UIrequirementPanel_moonphase.GetComponent<UnityEngine.UI.Image>().color.r, UIrequirementPanel_moonphase.GetComponent<UnityEngine.UI.Image>().color.g, UIrequirementPanel_moonphase.GetComponent<UnityEngine.UI.Image>().color.b, alphaInactive);
        UIrequirementPanel_moonphase.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(UIrequirementPanel_moonphase.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.r, UIrequirementPanel_moonphase.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.g, UIrequirementPanel_moonphase.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.b, alphaInactive);
        UIrequirementPanel_moonphase.GetChild(1).GetComponent<UnityEngine.UI.Text>().color = new Color(UIrequirementPanel_moonphase.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.r, UIrequirementPanel_moonphase.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.g, UIrequirementPanel_moonphase.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.b, alphaInactive);
        UIrequirementPanel_moonphase.GetChild(2).GetComponent<UnityEngine.UI.Text>().color = new Color(UIrequirementPanel_moonphase.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.r, UIrequirementPanel_moonphase.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.g, UIrequirementPanel_moonphase.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.b, alphaInactive);
        UIrequirementPanel_moonphase.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "Irrelevant";

        UIrequirementPanel_timeOfDay.GetComponent<UnityEngine.UI.Image>().color = new Color(UIrequirementPanel_timeOfDay.GetComponent<UnityEngine.UI.Image>().color.r, UIrequirementPanel_timeOfDay.GetComponent<UnityEngine.UI.Image>().color.g, UIrequirementPanel_timeOfDay.GetComponent<UnityEngine.UI.Image>().color.b, alphaInactive);
        UIrequirementPanel_timeOfDay.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(UIrequirementPanel_timeOfDay.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.r, UIrequirementPanel_timeOfDay.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.g, UIrequirementPanel_timeOfDay.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.b, alphaInactive);
        UIrequirementPanel_timeOfDay.GetChild(1).GetComponent<UnityEngine.UI.Text>().color = new Color(UIrequirementPanel_timeOfDay.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.r, UIrequirementPanel_timeOfDay.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.g, UIrequirementPanel_timeOfDay.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.b, alphaInactive);
        UIrequirementPanel_timeOfDay.GetChild(2).GetComponent<UnityEngine.UI.Text>().color = new Color(UIrequirementPanel_timeOfDay.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.r, UIrequirementPanel_timeOfDay.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.g, UIrequirementPanel_timeOfDay.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.b, alphaInactive);
        UIrequirementPanel_timeOfDay.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "Irrelevant";

        UIrequirementPanel_demonicpresence.GetComponent<UnityEngine.UI.Image>().color = new Color(UIrequirementPanel_demonicpresence.GetComponent<UnityEngine.UI.Image>().color.r, UIrequirementPanel_demonicpresence.GetComponent<UnityEngine.UI.Image>().color.g, UIrequirementPanel_demonicpresence.GetComponent<UnityEngine.UI.Image>().color.b, alphaInactive);
        UIrequirementPanel_demonicpresence.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(UIrequirementPanel_demonicpresence.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.r, UIrequirementPanel_demonicpresence.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.g, UIrequirementPanel_demonicpresence.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.b, alphaInactive);
        UIrequirementPanel_demonicpresence.GetChild(1).GetComponent<UnityEngine.UI.Text>().color = new Color(UIrequirementPanel_demonicpresence.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.r, UIrequirementPanel_demonicpresence.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.g, UIrequirementPanel_demonicpresence.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.b, alphaInactive);
        UIrequirementPanel_demonicpresence.GetChild(2).GetComponent<UnityEngine.UI.Text>().color = new Color(UIrequirementPanel_demonicpresence.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.r, UIrequirementPanel_demonicpresence.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.g, UIrequirementPanel_demonicpresence.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.b, alphaInactive);
        UIrequirementPanel_demonicpresence.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "Irrelevant";

        // change the requirements that are actually set as active to be displayed as such

        //container

        if (recipe.validAlchemyContainers.Count > 0)
        {
            UIrequirementPanel_alchemyContainer.GetComponent<UnityEngine.UI.Image>().color = new Color(UIrequirementPanel_alchemyContainer.GetComponent<UnityEngine.UI.Image>().color.r, UIrequirementPanel_alchemyContainer.GetComponent<UnityEngine.UI.Image>().color.g, UIrequirementPanel_alchemyContainer.GetComponent<UnityEngine.UI.Image>().color.b, 1);
            UIrequirementPanel_alchemyContainer.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(UIrequirementPanel_alchemyContainer.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.r, UIrequirementPanel_alchemyContainer.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.g, UIrequirementPanel_alchemyContainer.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.b, 1);
            UIrequirementPanel_alchemyContainer.GetChild(1).GetComponent<UnityEngine.UI.Text>().color = new Color(UIrequirementPanel_alchemyContainer.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.r, UIrequirementPanel_alchemyContainer.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.g, UIrequirementPanel_alchemyContainer.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.b, 1);
            UIrequirementPanel_alchemyContainer.GetChild(2).GetComponent<UnityEngine.UI.Text>().color = new Color(UIrequirementPanel_alchemyContainer.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.r, UIrequirementPanel_alchemyContainer.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.g, UIrequirementPanel_alchemyContainer.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.b, 1);

            UIrequirementPanel_alchemyContainer.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "";
            foreach (string child in recipe.validAlchemyContainers)
            {
                UIrequirementPanel_alchemyContainer.GetChild(2).GetComponent<UnityEngine.UI.Text>().text += (child + ", ");
            }
            UIrequirementPanel_alchemyContainer.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = UIrequirementPanel_alchemyContainer.GetChild(2).GetComponent<UnityEngine.UI.Text>().text.Remove(UIrequirementPanel_alchemyContainer.GetChild(2).GetComponent<UnityEngine.UI.Text>().text.Length - 2);
        }

        // temperature
        if ((recipe.minTemperature != 0)||(recipe.maxTemperature !=0))
        {
            UIrequirementPanel_temperature.GetComponent<UnityEngine.UI.Image>().color = new Color(UIrequirementPanel_temperature.GetComponent<UnityEngine.UI.Image>().color.r, UIrequirementPanel_temperature.GetComponent<UnityEngine.UI.Image>().color.g, UIrequirementPanel_temperature.GetComponent<UnityEngine.UI.Image>().color.b, 1);
            UIrequirementPanel_temperature.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(UIrequirementPanel_temperature.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.r, UIrequirementPanel_temperature.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.g, UIrequirementPanel_temperature.GetChild(0).GetComponent<UnityEngine.UI.Image>().color.b, 1);
            UIrequirementPanel_temperature.GetChild(1).GetComponent<UnityEngine.UI.Text>().color = new Color(UIrequirementPanel_temperature.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.r, UIrequirementPanel_temperature.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.g, UIrequirementPanel_temperature.GetChild(1).GetComponent<UnityEngine.UI.Text>().color.b, 1);
            UIrequirementPanel_temperature.GetChild(2).GetComponent<UnityEngine.UI.Text>().color = new Color(UIrequirementPanel_temperature.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.r, UIrequirementPanel_temperature.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.g, UIrequirementPanel_temperature.GetChild(2).GetComponent<UnityEngine.UI.Text>().color.b, 1);
            
            if ((recipe.minTemperature > -10000) && (recipe.maxTemperature < 10000))
                UIrequirementPanel_temperature.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "Between " + recipe.minTemperature + "° and " + recipe.maxTemperature + "°";
            if (recipe.minTemperature <= -10000)
                UIrequirementPanel_temperature.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = recipe.maxTemperature + "° or less";
            if (recipe.maxTemperature >= 10000)
                UIrequirementPanel_temperature.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = recipe.minTemperature + "° or more";
        }

      

    }

    public void OpenIngredientDetailPageInput(int index)
    {
        GetComponent<NotebookBaseUI>().Close();
        notebookIngredientDetails.Open(inputIngredients[index]);
    }

    public void OpenIngredientDetailPageOutput(int index)
    {
        GetComponent<NotebookBaseUI>().Close();
        notebookIngredientDetails.Open(outputIngredients[index]);
    }
}
