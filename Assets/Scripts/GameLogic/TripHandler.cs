using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExecuteTrip(TripPlanningHandler.activity pSelectedActivity, IngredientType pSelectedIngredient, RegionHandler.rarity pSelectedIngredientRarity, int pTotalDuration, int pActivityDuration, int pTravelDuration, List<IngredientType> pRegionIngredientTypeList, List<RegionHandler.rarity> pRegionRaritiesList)
    {
        TripPlanningHandler.activity selectedActivity = pSelectedActivity;
        IngredientType selectedIngredient = pSelectedIngredient;
        RegionHandler.rarity selectedIngredientRarity= pSelectedIngredientRarity;
        int totalDuration = pTotalDuration;
        int activityDuration = pActivityDuration;
        int travelDuration = pTravelDuration;
        List<IngredientType> regionIngredientTypeList = pRegionIngredientTypeList;
        List<RegionHandler.rarity> regionRaritiesList = pRegionRaritiesList;
        int tempHour = (int)GameTime.hourOfTheDay;

        // execute travel outgoing 

        // execute activity 



        switch (selectedActivity)
        {
            case TripPlanningHandler.activity.GATHER_EVERYTHING:

                for (int i = activityDuration; i > 0; i--)
                {
                    // TODO: Check WHICH ingredients gets found, add it
                    

                }

                break;

            case TripPlanningHandler.activity.GATHER_SPECIFIC:
                for (int i = activityDuration; i > 0; i--)
                {
                    Debug.Log("trying to find ingredient " + selectedIngredient.ingredientName + "...");
                   
                    if (Random.Range(0f, 1f)< RegionHandler.RarityToFloat(selectedIngredientRarity))
                    {
                        // specific item was found in this run
                        Debug.Log(selectedIngredient.standardGatherAmount + " items of " + selectedIngredient.ingredientName + " gathered. ");

                        // TODO Add gatheramount to temp gatheramount-sum value
                    }
                    else
                    {
                        // specific item wasnt found in this run
                        Debug.Log(0 + " items of " + selectedIngredient.ingredientName + " gathered. ");
                    }

                    // TODO Do something with gatheramount-sum
                }

                break;
            default:
                break;
        }

        // execute travel returning


    }
}
