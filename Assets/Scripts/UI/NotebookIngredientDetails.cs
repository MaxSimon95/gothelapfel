using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookIngredientDetails : MonoBehaviour
{

    public IngredientType ingredient;

    public GameObject UIname;
    public GameObject UIimage_withDescription;
    public GameObject UIimage_noDescription;
    public GameObject UIdescription;

    public Transform effectPanelsParent;

    private List<Transform> effectPanels = new List<Transform>();
    private List<IngredientEffect> effects = new List<IngredientEffect>();
    private List<int> effectIntensities = new List<int>();


    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in effectPanelsParent)
        {
            effectPanels.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(IngredientType pIngredientType)
    {
       
        ingredient = pIngredientType;
        UpdateIngredientDetails();
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
                    Debug.Log(effectPanels[i].GetChild(3));
                    Debug.Log(i + "positive");
                    effectPanels[i].GetChild(2).GetChild(0).gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(0.6603774f, 0.6375712f, 0.5326629f, 0.3f);
                    effectPanels[i].GetChild(2).GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image>().fillAmount = 0f;
                    effectPanels[i].GetChild(2).GetChild(2).gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 0.3f);

                    effectPanels[i].GetChild(3).GetChild(0).gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(0.6603774f, 0.6375712f, 0.5326629f, 1f);
                    effectPanels[i].GetChild(3).GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image>().fillAmount = (Mathf.Abs((float)effectIntensities[i])) / 100;
                    effectPanels[i].GetChild(3).GetChild(2).gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 1f);
                }
                else
                {
                    Debug.Log(effectPanels[i].GetChild(2));
                    Debug.Log(i + "negative");
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
}
