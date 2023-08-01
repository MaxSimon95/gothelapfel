using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripHandler : MonoBehaviour
{
    public List<IngredientType> outputIngredients = new List<IngredientType>();
    public List<int> outputAmounts = new List<int>();

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
        outputIngredients.Clear();
        outputAmounts.Clear();

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

                float sumRaritiesTotal = 0; // as a top border for the random function

                // set total sum, needs to be done only once because it stays the same in each hourly repeat of the search
                foreach(RegionHandler.rarity rarity in regionRaritiesList)
                {
                    sumRaritiesTotal += RegionHandler.RarityToFloat(rarity);
                }

                // Check WHICH ingredient gets found, add it

                // for every hour...
                for (int h = 0; h < activityDuration; h++)
                {
                    float sumRaritiesTemp = 0;  // used for iterating through with the random's function result as index to decide which ingredient was found.
                    float r = Random.Range(0f, sumRaritiesTotal);
                    bool ingredientFound = false;

                    // for every possible ingredient until we've found our precious target...
                    for (int i=0; ((i< regionIngredientTypeList.Count)&&(!ingredientFound)); i++)
                    {
                        sumRaritiesTemp += RegionHandler.RarityToFloat(regionRaritiesList[i]);
                        //Debug.Log("sumRaritiesTemp = " + sumRaritiesTemp + "; r = " + r + "; current ingredient: " + regionIngredientTypeList[i].ingredientName);
                        if (r < sumRaritiesTemp)
                            // if yes, then this is the randomly selected ingredient and we're done.
                        {
                            ingredientFound = true;
                            AddFindingsToOutput(regionIngredientTypeList[i], regionIngredientTypeList[i].standardGatherAmount);
                        }
                        else
                        // if no, then we must keep going
                        {
                            
                        }

                    }

                }

                break;

            case TripPlanningHandler.activity.GATHER_SPECIFIC:
                for (int h = 0; h < activityDuration; h++)
                {
                    //Debug.Log("trying to find ingredient " + selectedIngredient.ingredientName + "...");
                    float r = Random.Range(0f, 1f);

                    if (r < RegionHandler.RarityToFloat(selectedIngredientRarity))
                    {
                        // specific item was found in this run
                        //Debug.Log("r = " + r + "; rarity = " + RegionHandler.RarityToFloat(selectedIngredientRarity) + "; " + selectedIngredient.standardGatherAmount + " items of " + selectedIngredient.ingredientName + " gathered. ");

                        AddFindingsToOutput(selectedIngredient, selectedIngredient.standardGatherAmount);
                    }
                    else
                    {
                        // specific item wasnt found in this run
                        //Debug.Log("r = " + r + "; rarity = " + RegionHandler.RarityToFloat(selectedIngredientRarity) + "; " + 0 + " items of " + selectedIngredient.ingredientName + " gathered. ");
                        
                    }

                }

                break;
            default:
                break;
        }

        // execute travel returning


    }

    private void AddFindingsToOutput(IngredientType pFoundIngredientType, int pFoundAmount)
    {
        IngredientType foundIngredientType = pFoundIngredientType;
        int foundAmount = pFoundAmount;
        bool iTAlreadyInOutputIngredients = false;

        for(int i=0; i<outputIngredients.Count; i++)
        {
            if (outputIngredients[i] == pFoundIngredientType)
            {
                iTAlreadyInOutputIngredients = true;
                outputAmounts[i] += foundAmount;
            }
        }

        if(!iTAlreadyInOutputIngredients)
        {
            outputIngredients.Add(pFoundIngredientType);
            outputAmounts.Add(pFoundAmount);
        }


    }
}
