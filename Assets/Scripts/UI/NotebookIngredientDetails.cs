using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;


public class NotebookIngredientDetails : MonoBehaviour
{

    public IngredientType ingredient;

    public GameObject UIname;
    public GameObject UIimage_withDescription;
    public GameObject UIimage_noDescription;
    public GameObject UIdescription;

    public GameObject UI_inputRecipesLabel;
    public GameObject UI_outputRecipesLabel;
    public GameObject UI_regionsLabel;

    public Transform effectPanelsParent;

    public Transform effectsLabel;

    private List<Transform> effectPanels = new List<Transform>();
    private List<IngredientEffect> effects = new List<IngredientEffect>();
    private List<int> effectIntensities = new List<int>();

    private int numberRecipes;
    public Transform recipePanelsParent;

    private List<Transform> regionPanels = new List<Transform>();
    private List<Transform> inputRecipePanels = new List<Transform>();
    private List<Transform> outputRecipePanels = new List<Transform>();

    public GameObject UI_label_noRegions;
    public GameObject UI_label_noInputRecipes;
    public GameObject UI_label_noOutputRecipes;

    //public List<AlchemyReaction> allReactions = new List<AlchemyReaction>();



    public NotebookRecipeDetails notebookRecipeDetails;
    public NotebookRegionDetails notebookRegionDetails;


    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in effectPanelsParent)
        {
            effectPanels.Add(child);
        }

        /*foreach (Transform child in effectPanelsParent)
        {
            effectPanels.Add(child);
        }*/

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Open(IngredientType pIngredientType)
    {
       
        ingredient = pIngredientType;
        UpdateIngredientDetails();

        LoadPanels();

        UpdateAllAttachedLinks();

        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());

        GetComponent<NotebookBaseUI>().Open();
    }

    public void UpdateIngredientDetails()
    {
        effects = ingredient.effects;
        effectIntensities = ingredient.effectIntensities;

        
        UIname.GetComponent<UnityEngine.UI.Text>().text = ingredient.ingredientName;

        
        
        if(ingredient.description == "")
        {
            UIdescription.transform.localScale = new Vector3(0, 0, 0);
            UIimage_noDescription.GetComponent<UnityEngine.UI.Image>().sprite = ingredient.inventorySprite;
            UIimage_withDescription.transform.localScale = new Vector3(0, 0, 0);
            UIimage_noDescription.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            
            UIimage_withDescription.GetComponent<UnityEngine.UI.Image>().sprite = ingredient.inventorySprite;
            UIimage_withDescription.transform.localScale = new Vector3(1, 1, 1);
            UIimage_noDescription.transform.localScale = new Vector3(0, 0, 0);
            UIdescription.transform.localScale = new Vector3(1, 1, 1);
            UIdescription.GetComponent<UnityEngine.UI.Text>().text = ingredient.description;
        }


        // show / hide effects label

        if(effects.Count == 0 )
            effectsLabel.localScale = new Vector3(0, 0, 0);
        else effectsLabel.localScale = new Vector3(1, 1, 1);


        // adjust the graphical intensity indicators

        for (int i = 0; i < effectPanels.Count; i++)
        {
            effectPanels[i].localScale = new Vector3(0, 0, 0);

            //Debug.Log(i);

            //Debug.Log(JobsManagement.activeJobList.Count);
            if (i < effects.Count)
            {
                effectPanels[i].localScale = new Vector3(1, 1, 1);
                //Debug.Log(i + "set to visible, local scale: " + effectPanels[i].localScale);
                //Debug.Log(i);
                // CAREFUL: THIS STUFF IS ORDERING SENSITIVE. YOU MESS WITH THE ORDERING, YOU MESS WITH THE CONTENTS, YO! 
                //Debug.Log(i);

                
                effectPanels[i].GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = effects[i].effectName + ": ";

                switch(IngredientEffect.IntensityTypeIntToEnum(effectIntensities[i]) )
                {
                    case IngredientEffect.EffectIntensity.EXTREME_NEGATIVE:
                        effectPanels[i].GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text += effects[i].stringLowerM100;
                        break;

                    case IngredientEffect.EffectIntensity.STRONG_NEGATIVE:
                        effectPanels[i].GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text += effects[i].stringM100ToM50;
                        break;

                    case IngredientEffect.EffectIntensity.SLIGHT_NEGATIVE:
                        effectPanels[i].GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text += effects[i].stringM50To0;
                        break;

                    case IngredientEffect.EffectIntensity.EXTREME_POSITIVE:
                        effectPanels[i].GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text += effects[i].stringHigher100;
                        break;

                    case IngredientEffect.EffectIntensity.STRONG_POSITIVE:
                        effectPanels[i].GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text += effects[i].string50To100;
                        break;

                    case IngredientEffect.EffectIntensity.SLIGHT_POSITIVE:
                        effectPanels[i].GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text += effects[i].string0To50;
                        break;

                }

                //adjust fill amount and transparency

                if (effectIntensities[i]>0)
                {
                   // Debug.Log(effectPanels[i].GetChild(3));
                   // Debug.Log(i + "positive");
                    effectPanels[i].GetChild(2).GetChild(0).gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(0.6603774f, 0.6375712f, 0.5326629f, 0.3f);
                    effectPanels[i].GetChild(2).GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image>().fillAmount = 0f;
                    effectPanels[i].GetChild(2).GetChild(2).gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 0.3f);

                    effectPanels[i].GetChild(3).GetChild(0).gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(0.6603774f, 0.6375712f, 0.5326629f, 1f);
                    effectPanels[i].GetChild(3).GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image>().fillAmount = (Mathf.Abs((float)effectIntensities[i])) / 100;
                    effectPanels[i].GetChild(3).GetChild(2).gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 1f);
                }
                else
                {
                   // Debug.Log(effectPanels[i].GetChild(2));
                   // Debug.Log(i + "negative");
                    effectPanels[i].GetChild(2).GetChild(0).gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(0.6603774f, 0.6375712f, 0.5326629f, 1f);
                    effectPanels[i].GetChild(2).GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image>().fillAmount = (Mathf.Abs((float)effectIntensities[i])) / 100;
                    effectPanels[i].GetChild(2).GetChild(2).gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 1f);

                    effectPanels[i].GetChild(3).GetChild(0).gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(0.6603774f, 0.6375712f, 0.5326629f, 0.3f);
                    effectPanels[i].GetChild(3).GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image>().fillAmount = 0f;
                    effectPanels[i].GetChild(3).GetChild(2).gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 0.3f);
                }


                // adjust foreground fill colors

                effectPanels[i].GetChild(2).GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image>().color = effects[i].negativeColor;
                effectPanels[i].GetChild(3).GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image>().color = effects[i].positiveColor;

                //adjust indicator icon

                float adjustedOffset;
                adjustedOffset = effectIntensities[i];

                if (adjustedOffset < -100) adjustedOffset = -100;

                if (adjustedOffset > 100) adjustedOffset = 100;

                effectPanels[i].GetChild(4).gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(375 + adjustedOffset*375/100, effectPanels[i].GetChild(4).gameObject.GetComponent<RectTransform>().anchoredPosition.y);
               
            }
        }

        

        

    }

    public void LoadPanels()
    {
        regionPanels.Clear();
        inputRecipePanels.Clear();
        outputRecipePanels.Clear();

        foreach (Transform child in recipePanelsParent)
        {
            if (child.gameObject.name == "PanelRegion")
            {
                regionPanels.Add(child);
            }

            if (child.gameObject.name == "PanelRecipeInput")
            {
                inputRecipePanels.Add(child);
            }

            if (child.gameObject.name == "PanelRecipeOutput")
            {
                outputRecipePanels.Add(child);
            }
        }
    }

    /*public void OpenRecipe(int i)
    {
        //Debug.Log(i + openPage * 10);
        //Debug.Log(i + " " + JobsManagement.activeJobList[i + openPage * 10].title);
        //Debug.Log(jobList[i + openPage * 10]);
        //JobHandler.detailJob = JobsManagement.activeJobList[i + openPage * 10];
        GetComponent<NotebookBaseUI>().Close();
        //Debug.Log("opening call");
        notebookRecipeDetails.Open(allReactions[i]);

    }*/

    public void UpdateAllAttachedLinks()
    {
        ingredient.UpdateReactionsOutput();
        ingredient.UpdateReactionsInput();
        ingredient.UpdateRegions();

        // update no ... labels

       
        if(ingredient.regions.Count() == 0)
            UI_label_noRegions.transform.localScale = new Vector3(1, 1, 1);
        else
            UI_label_noRegions.transform.localScale = new Vector3(0, 0, 0);

        if (ingredient.reactionsInput.Count() == 0)
            UI_label_noInputRecipes.transform.localScale = new Vector3(1, 1, 1);
        else
            UI_label_noInputRecipes.transform.localScale = new Vector3(0, 0, 0);

        if (ingredient.reactionsOutput.Count() == 0)
            UI_label_noOutputRecipes.transform.localScale = new Vector3(1, 1, 1);
        else
            UI_label_noOutputRecipes.transform.localScale = new Vector3(0, 0, 0);


        Debug.Log("update recipe panels");
        // update recipe panels

        foreach (Transform panel in regionPanels)
        {
            panel.localScale = new Vector3(0, 0, 0);
        }

        for(int i=0;i< ingredient.regions.Count();i++)
        {
            regionPanels[i].localScale = new Vector3(1, 1, 1);
            regionPanels[i].GetChild(0).GetComponent<UnityEngine.UI.Text>().text = ingredient.regions[i].regionName;
        }


        foreach (Transform panel in inputRecipePanels)
        {
            panel.localScale = new Vector3(0, 0, 0);
        }

        for (int i = 0; i < ingredient.reactionsInput.Count(); i++)
        {
            inputRecipePanels[i].localScale = new Vector3(1, 1, 1);
            inputRecipePanels[i].GetChild(0).GetComponent<UnityEngine.UI.Text>().text = ingredient.reactionsInput[i].reactionName;
        }


        foreach (Transform panel in outputRecipePanels)
        {
            panel.localScale = new Vector3(0, 0, 0);
        }

        for (int i = 0; i < ingredient.reactionsOutput.Count(); i++)
        {
            outputRecipePanels[i].localScale = new Vector3(1, 1, 1);
            outputRecipePanels[i].GetChild(0).GetComponent<UnityEngine.UI.Text>().text = ingredient.reactionsOutput[i].reactionName;
        }


    }

    public void OpenRegion(int i)
    {
        

            //Debug.Log("Region detailseite ["+i+"] öffnen: " + ingredient.regions[i].regionName);
  
            notebookRegionDetails.Open(ingredient.regions[i]);
       
        this.GetComponent<NotebookBaseUI>().Close();
    }

    public void OpenInputRecipe(int i)
    {

            // Recipse detailseite [i - regions list length] öffnen
            //Debug.Log("Recipe detailseite [" + (i - ingredient.regions.Count) + "] öffnen: " + allReactions[i - ingredient.regions.Count].reactionName);
            notebookRecipeDetails.Open(ingredient.reactionsInput[i]);


        this.GetComponent<NotebookBaseUI>().Close();
    }

    public void OpenOutputRecipe(int i)
    {

        // Recipse detailseite [i - regions list length] öffnen
        //Debug.Log("Recipe detailseite [" + (i - ingredient.regions.Count) + "] öffnen: " + allReactions[i - ingredient.regions.Count].reactionName);
        notebookRecipeDetails.Open(ingredient.reactionsOutput[i]);


        this.GetComponent<NotebookBaseUI>().Close();
    }

}
