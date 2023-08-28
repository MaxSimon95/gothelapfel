using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripHandler : MonoBehaviour
{
    public GameObject panelInvetorySlots;


    public List<IngredientType> outputIngredients = new List<IngredientType>();
    public List<int> outputAmounts = new List<int>();

    public List<ItemSlotHandler> outputItemSlots = new List<ItemSlotHandler>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform slot in panelInvetorySlots.transform)
        {
            if (slot.gameObject.activeSelf)
                outputItemSlots.Add(slot.gameObject.GetComponent<ItemSlotHandler>());
        }
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

        PutOutputIntoInventoryItems();
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

    public void ButtonTakeAllPressed()
    {
        foreach (Transform slot in panelInvetorySlots.transform)
        {
            if (slot.childCount > 0)
            {
                slot.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().TransferItemAutomatically();
            }
        }
    }

    private void PutOutputIntoInventoryItems()
    {
        for (int i = 0; i < outputItemSlots.Count; i++)
        {
            if(i < outputIngredients.Count)
            {
                GameObject slotInventoryItem = null;
                // make a new item in a free slot if i is less than unlockedslots,
                //otherwise the remaining ingredients just get added into the last item that was created on the last free slot in every loop

                slotInventoryItem = (GameObject)Instantiate(Resources.Load("InventoryItemPrefab"), new Vector3(0, 0, 0), Quaternion.identity);
                slotInventoryItem.transform.SetParent(outputItemSlots[i].transform);
                slotInventoryItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                slotInventoryItem.GetComponent<RectTransform>().localScale = new Vector3(slotInventoryItem.transform.parent.gameObject.GetComponent<ItemSlotHandler>().uiScale, slotInventoryItem.transform.parent.gameObject.GetComponent<ItemSlotHandler>().uiScale, slotInventoryItem.transform.parent.gameObject.GetComponent<ItemSlotHandler>().uiScale);

                // throw out the prefab amount and type
                slotInventoryItem.GetComponent<InventoryItemHandler>().ingredientTypeAmounts.RemoveAt(0);
                slotInventoryItem.GetComponent<InventoryItemHandler>().ingredientTypes.RemoveAt(0);

                slotInventoryItem.GetComponent<InventoryItemHandler>().AddIngredient(outputIngredients[i], outputAmounts[i]);

                slotInventoryItem.GetComponent<InventoryItemHandler>().UpdateItemContent();

            }




            // update slot visuals
            outputItemSlots[i].UpdateSlotVisibility();

        }
    }

    public void EmptyItemSlots()
    {
        foreach(ItemSlotHandler itemslot in outputItemSlots)
        {
            if(itemslot.transform.childCount > 0)
            GameObject.Destroy(itemslot.transform.GetChild(0).gameObject);
            //itemslot.UpdateSlotVisibility();
        }
    }

}
