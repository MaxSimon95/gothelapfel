using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobHandler : MonoBehaviour
{
    public string title;
    public int payment;

    public IngredientType requestedIngredientType;
    public List<IngredientEffect> requestedEffects = new List<IngredientEffect>();
    public List<IngredientEffect.EffectIntensity> requestedEffectIntensities = new List<IngredientEffect.EffectIntensity> ();
    public string requestedEffectsString = "";

    public int requestedAmount;

    public int startPointInTime;
    public int startDays;
    public int remainingDays;
    public bool jobIsSaved;
    public int orderNumber;

    public enum state { UPCOMING, ACTIVE, COMPLETED, EXPIRED}
    public state currentState;


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
        // set startpointinTime
    }

    public void Complete()
    {
        currentState = JobHandler.state.COMPLETED;
        jobIsSaved = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
