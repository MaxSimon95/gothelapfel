using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class JobHandler : MonoBehaviour
{
    public string title;
    public int payment;

    public IngredientType requestedIngredientType;
    public List<IngredientEffect> requestedEffects = new List<IngredientEffect>();
    public List<IngredientEffect.EffectIntensity> requestedEffectIntensities = new List<IngredientEffect.EffectIntensity> ();
    public string requestedEffectsString = "";

    public int requestedAmount;

    bool ignoreSideffectsForItemSuitableChecks;

    public int startPointInTime;
    public int startDays;
    public int remainingDays;
    public bool jobIsSaved;
    public int orderNumber;
    public string description;

    public NPC client;

    public static JobHandler detailJob;

    public enum state { UPCOMING, ACTIVE, COMPLETED, EXPIRED}
    public state currentState;

    public enum ItemTypeSuitable
    {
        CORRECT_EFFECT_WITHOUT_UNWANTED_HARMFUL_SIDEFFECTS,
        CORRECT_EFFECT_WITH_UNWANTED_HARMFUL_SIDEFFECTS,
        WRONG_EFFECT,
        CORRECT_INVENTORYITEM,
        WRONG_INVENTORYITEM
    }

    public enum ItemAmountSuitable
    {
        TOO_LITTLE,
        CORRECT,
        TOO_MUCH

    }


    // Start is called before the first frame update
    void Awake()
    {
        for (int i=0; i < requestedEffects.Count; i++) 
        {
            IngredientEffect e = requestedEffects[i];
            int tempEffectStrength = (int)requestedEffectIntensities[i];

            requestedEffectsString += e.GetEffectDescriptionString(tempEffectStrength).ToString() + ",\n" ;
        }

        // clean up string
        if(requestedEffectsString != "")
        {
            // remove last ,
            requestedEffectsString = requestedEffectsString.Remove(requestedEffectsString.Length - 1);
            // remove last linebreak
            requestedEffectsString = requestedEffectsString.Remove(requestedEffectsString.Length - 1);
        }

        //Debug.Log(requestedEffectsString);

        remainingDays = startDays;
        orderNumber = CalculateMarkedOrderNumber();
    }

    int CalculateMarkedOrderNumber()
    {
        int tempNumber = 0;
        if (!jobIsSaved)
        {
            tempNumber += 999999;
        }

        tempNumber += remainingDays;

        return tempNumber;
    }

    public void Expire()
    {
        Debug.Log(" EXPIRE: " + title);
        currentState = JobHandler.state.EXPIRED;
        jobIsSaved = false;

    }

    public void Activate()
    {
        // set state to active
        currentState = JobHandler.state.ACTIVE;
        // set startpointinTime
        startPointInTime = GameTime.daysSinceYearStart;
    }

    public void Complete()
    {
        currentState = JobHandler.state.COMPLETED;
        jobIsSaved = false;
    }

    public ItemTypeSuitable CheckItemTypeSuitable (InventoryItemHandler item)
    {
        if(requestedIngredientType != null)
        {
            if(requestedIngredientType == item.ingredientTypes[0])
            {
                return ItemTypeSuitable.CORRECT_INVENTORYITEM;
            }
            else
            {
                return ItemTypeSuitable.WRONG_INVENTORYITEM;
            }
        }
        else
        {
            for (int i = 0; i < requestedEffects.Count; i++)
            {

                IngredientEffect tempJobeffect = requestedEffects[i];
                Debug.Log("Effect loop for effect: " + tempJobeffect.effectName + "checking if there is a fitting item effect. Loop: " + i + " / " + requestedEffects.Count);

                // if an effect is missing or to weak: WRONG EFFECT
                if (item.IngredientEffects.Contains(tempJobeffect))
                {
                    Debug.Log("Effect " + tempJobeffect.effectName + "is present in the item. Intensity check following.");
                    // if one of the requested effect intensities is not being met --> WRONG EFFECT

                    Debug.Log("Effect " + tempJobeffect.effectName + "; requested intensity: " + (int) requestedEffectIntensities[i] + "; item intensity: " + item.IngredientEffectIntensities[item.IngredientEffects.FindIndex(s => s.Equals(tempJobeffect))]);
                    if((int)requestedEffectIntensities[i]<0)
                    {
                        if (item.IngredientEffectIntensities[item.IngredientEffects.FindIndex(s => s.Equals(tempJobeffect))] < (int)requestedEffectIntensities[i])
                        {
                            Debug.Log("Effect " + tempJobeffect.effectName + "intensity not fitting");
                            return ItemTypeSuitable.WRONG_EFFECT;
                        }

                        Debug.Log(item.IngredientEffects.FindIndex(s => s.Equals(tempJobeffect)));
                    }
                    else
                    {
                        if (item.IngredientEffectIntensities[item.IngredientEffects.FindIndex(s => s.Equals(tempJobeffect))] > (int)requestedEffectIntensities[i])
                        {
                            Debug.Log("Effect " + tempJobeffect.effectName + "intensity not fitting");
                            return ItemTypeSuitable.WRONG_EFFECT;
                        }

                        Debug.Log(item.IngredientEffects.FindIndex(s => s.Equals(tempJobeffect)));
                    }
                    
                }
                else
                {
                    Debug.Log("Effect " + tempJobeffect.effectName + "not found at all in item effects");
                    return ItemTypeSuitable.WRONG_EFFECT;
                }

                Debug.Log("All effects are there in the correct intensity");
            }
                // if we got this far then the requested effect is being met. time to see if there are sideeffects.
                if (ignoreSideffectsForItemSuitableChecks)
                {
                Debug.Log("Side effects are being ignored.");
                return ItemTypeSuitable.CORRECT_EFFECT_WITHOUT_UNWANTED_HARMFUL_SIDEFFECTS;
                }
                else
                {
                Debug.Log("Side effects are not being ignored. Now iterating through all present item effects, to see if any are harmful");
                //bool problematicEffectDetected = false;
                for (int e = 0; e< item.IngredientEffects.Count; e++)
                    {
                    Debug.Log("effect: " + item.IngredientEffects[e].effectName);
                        if (item.IngredientEffectIntensities[e] != 0)
                        {
                            IngredientEffect effect = item.IngredientEffects[e];

                            

                            switch (item.IngredientEffects[e].intensityType)
                            {
                                case IngredientEffect.IntensityType.IRRELEVANT:
                                    //return ItemTypeSuitable.CORRECT_EFFECT_WITHOUT_UNWANTED_HARMFUL_SIDEFFECTS;
                                    break;

                                case IngredientEffect.IntensityType.ZERO_IS_HEALTHY:
                                    if (item.IngredientEffectIntensities[e] == 0) { }
                                    //return ItemTypeSuitable.CORRECT_EFFECT_WITHOUT_UNWANTED_HARMFUL_SIDEFFECTS;

                                    else
                                        return ItemTypeSuitable.CORRECT_EFFECT_WITH_UNWANTED_HARMFUL_SIDEFFECTS;
                                    break;

                                case IngredientEffect.IntensityType.MAXIMUM_IS_HEALTHY:
                                    if (item.IngredientEffectIntensities[e] >= 0) { }
                                    //return ItemTypeSuitable.CORRECT_EFFECT_WITHOUT_UNWANTED_HARMFUL_SIDEFFECTS;
                                    else
                                        return ItemTypeSuitable.CORRECT_EFFECT_WITH_UNWANTED_HARMFUL_SIDEFFECTS;
                                    break;

                                case IngredientEffect.IntensityType.MINIMUM_IS_HEALTHY:
                                    if (item.IngredientEffectIntensities[e] <= 0) { }
                                    //return ItemTypeSuitable.CORRECT_EFFECT_WITHOUT_UNWANTED_HARMFUL_SIDEFFECTS;
                                    else
                                        return ItemTypeSuitable.CORRECT_EFFECT_WITH_UNWANTED_HARMFUL_SIDEFFECTS;
                                    break;
                            


                                
                            }
                        }
                    }

                    
                }


            
        }
        return ItemTypeSuitable.CORRECT_EFFECT_WITHOUT_UNWANTED_HARMFUL_SIDEFFECTS;
    }

    public ItemAmountSuitable CheckItemAmoundSuitable (InventoryItemHandler item)
    {
        if (item.ingredientTypeAmounts.Sum() < requestedAmount)
        {
            return ItemAmountSuitable.TOO_LITTLE;
        }

        if (item.ingredientTypeAmounts.Sum() == requestedAmount)
        {
            return ItemAmountSuitable.CORRECT;
        }

        //if (item.ingredientTypeAmounts.Sum() > requestedAmount)
        //{
            return ItemAmountSuitable.TOO_MUCH;
        //}
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
