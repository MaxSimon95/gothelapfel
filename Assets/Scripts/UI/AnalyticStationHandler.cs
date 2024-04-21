using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticStationHandler : MonoBehaviour
{

    public ItemSlotHandler itemSlot;
    public Transform effectPanelsParent;
    public GameObject hintText;

    private List<Transform> effectPanels = new List<Transform>();
    private List<IngredientEffect> effects = new List<IngredientEffect>();
    private List<float> effectIntensities = new List<float>();
    

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in effectPanelsParent)
        {
            effectPanels.Add(child);
        }
        UpdateDisplayedEffects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDisplayedEffects()
    {

        Debug.Log("UpdateDisplayedEffects");
        // hide effect panels when no inventory item is in the slot, show message instead
        if (itemSlot.transform.childCount == 0)
        {
            foreach (Transform panel in effectPanels)
            {
                panel.localScale = new Vector3(0, 0, 0);
            }

            hintText.gameObject.GetComponent<UnityEngine.UI.Text>().text = "Add an alchemicum into the slot to analyze its properties.";
            hintText.transform.localScale = new Vector3(1, 1, 1);
        }
        // read effects out of item
        else
        {

            InventoryItemHandler inventoryItem = itemSlot.transform.GetChild(0).GetComponent<InventoryItemHandler>();

            effects = inventoryItem.IngredientEffects;
            effectIntensities = inventoryItem.IngredientEffectIntensities;

            // handle items without any effects
            if (effects.Count == 0)
            {
                hintText.gameObject.GetComponent<UnityEngine.UI.Text>().text = "This alchemicum does not have any effects";
                hintText.transform.localScale = new Vector3(1, 1, 1);
            }
            else 
            {
                hintText.transform.localScale = new Vector3(0, 0, 0);
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

                    switch (IngredientEffect.IntensityTypeIntToEnum(effectIntensities[i]))
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

                    if (effectIntensities[i] > 0)
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

                    effectPanels[i].GetChild(4).gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(375 + adjustedOffset * 375 / 100, effectPanels[i].GetChild(4).gameObject.GetComponent<RectTransform>().anchoredPosition.y);

                }
            }
        }
        
    }

}
