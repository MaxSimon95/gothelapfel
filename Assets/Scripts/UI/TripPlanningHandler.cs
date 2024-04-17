using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TripPlanningHandler : MonoBehaviour
{

    public GameObject tripHandler;
    public GameObject regions;
    public List<RegionHandler> regionHandlerList = new List<RegionHandler>();
    public List<IngredientType> curatedIngredientTypeList = new List<IngredientType>(); // list to access which specific ingredient the user has selected
    public List<IngredientType> oldIngredientList = new List<IngredientType>(); // list to compare the new lists to to see if there are changes and UI updates are in order
    public List<RegionHandler.rarity> curatedRaritiesList = new List<RegionHandler.rarity>();

    public List<Dropdown.OptionData> regionsDropdownOptions = new List<Dropdown.OptionData>();

    public GameObject UIDropdownRegion;
    public GameObject UIDropdownActivity;
    public GameObject UIDropdownIngredient;
    public GameObject UIButtonCancel;
    public GameObject UIButtonStartTrip;
    public GameObject UIRadioAllAvailableIngredients;
    public GameObject UIRadioSpecificIngredient;
    public GameObject UIDropdownTime;

    public GameObject UILabelRegionSelection;
    public GameObject UILabelTimeToGetThere;
    public GameObject UILabelActivitySelection;
    public GameObject UILabelIngredientSelection;
    public GameObject UILabelTimeSelection;
    public GameObject UILabelTotalTime;

    public int totalDuration;

    public enum activity
    {
        GATHER_EVERYTHING,
        GATHER_SPECIFIC,
    }

    activity selectedActivity;
    IngredientType selectedIngredientType;
    RegionHandler.rarity selectedIngredientRarity;
    RegionHandler region;

    // Start is called before the first frame update
    void Start()
    {
        // initialize dropdownregion
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OpenTripPlanner()
    {
        Debug.Log("OpenTripPlanner");

        regionHandlerList.Clear();
        regionsDropdownOptions.Clear();
        regionsDropdownOptions.Add(new Dropdown.OptionData("Click to Select"));
        UIDropdownRegion.GetComponent<Dropdown>().ClearOptions();

        foreach (Transform region in regions.transform)
        {
            regionHandlerList.Add(region.GetComponent<RegionHandler>());
            //Debug.Log(region.GetComponent<RegionHandler>().regionName); 
            regionsDropdownOptions.Add(new Dropdown.OptionData(region.GetComponent<RegionHandler>().regionName));
        }

        UIDropdownRegion.GetComponent<Dropdown>().AddOptions(regionsDropdownOptions);

        UpdateElements();
    }

    public void CloseTripPlanner()
    {

    }

    // updateVisibility based on conducted selections
    public void UpdateElements()
    {
        Debug.Log("UpdateElements");
        // if region selected, show activity + timetogetthere, else --> hide activity + timetogetthere
        if (UIDropdownRegion.GetComponent<Dropdown>().value == 0)
        {
            UILabelActivitySelection.transform.localScale = new Vector3(0, 0, 0);
            UIDropdownActivity.transform.localScale = new Vector3(0, 0, 0);
            UILabelTimeToGetThere.transform.localScale = new Vector3(0, 0, 0);

            // clean up values down the line
            UIDropdownActivity.GetComponent<Dropdown>().value = 0;
        }
        else
        {
            UpdateIngredientDropdown();
            UILabelActivitySelection.transform.localScale = new Vector3(1, 1, 1);
            UIDropdownActivity.transform.localScale = new Vector3(1, 1, 1);
            
            if(regionHandlerList[UIDropdownRegion.GetComponent<Dropdown>().value-1].travelTimeToHome == 1f)
            {
                UILabelTimeToGetThere.GetComponent<UnityEngine.UI.Text>().text = "It takes " + regionHandlerList[UIDropdownRegion.GetComponent<Dropdown>().value-1].travelTimeToHome + " hour to get there.";
            }
            else
            {
                UILabelTimeToGetThere.GetComponent<UnityEngine.UI.Text>().text = "It takes " + regionHandlerList[UIDropdownRegion.GetComponent<Dropdown>().value-1].travelTimeToHome + " hours to get there.";
            }

            UILabelTimeToGetThere.transform.localScale = new Vector3(1, 1, 1);
        }

        // if activity == gather ingredients, show ingredient toggles, else --> hide toggles
        if (UIDropdownActivity.GetComponent<Dropdown>().value == 1)
        {
            UILabelIngredientSelection.transform.localScale = new Vector3(1, 1, 1);
            UIRadioAllAvailableIngredients.transform.localScale = new Vector3(1, 1, 1);
            UIRadioSpecificIngredient.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            UIRadioAllAvailableIngredients.GetComponent<Toggle>().isOn = false;
            UIRadioSpecificIngredient.GetComponent<Toggle>().isOn = false;
            UILabelIngredientSelection.transform.localScale = new Vector3(0, 0, 0);

            // clean up values down the line
            UIRadioAllAvailableIngredients.transform.localScale = new Vector3(0, 0, 0);
            UIRadioSpecificIngredient.transform.localScale = new Vector3(0, 0, 0);
        }


        // if specific ingredient is toggled, show ingredient selection --> else hide
        if (UIRadioSpecificIngredient.GetComponent<Toggle>().isOn)
        {
            selectedActivity = activity.GATHER_SPECIFIC;
            UIDropdownIngredient.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            selectedActivity = activity.GATHER_EVERYTHING;
            UIDropdownIngredient.transform.localScale = new Vector3(0, 0, 0);

            // clean up values down the line
            UIDropdownIngredient.GetComponent<Dropdown>().value = 0;
        }


        /* 
         * if activity.manualTimespanSelection == true 
         * and (all available ingredients toggle is set or (specific ingredient toggle set && specific ingredient dropdown selected)),
         * show time selection --> else hide

        */

        if (UIDropdownIngredient.GetComponent<Dropdown>().value != 0)
        {
            selectedIngredientType = curatedIngredientTypeList[UIDropdownIngredient.GetComponent<Dropdown>().value - 1];
            selectedIngredientRarity = curatedRaritiesList[UIDropdownIngredient.GetComponent<Dropdown>().value - 1];
            Debug.Log(selectedIngredientType.ingredientName);
        } 



        if ((UIDropdownActivity.GetComponent<Dropdown>().value == 1) // activity.manualTimespanSelection == true (activity "1" is a true one for now)
            && (
                (UIRadioAllAvailableIngredients.GetComponent<Toggle>().isOn) // all available ingredients toggle is set
                ||
                (UIDropdownIngredient.GetComponent<Dropdown>().value != 0) // specific ingredient dropdown selected
               )
            )

            
        {
            UILabelTimeSelection.transform.localScale = new Vector3(1, 1, 1);
            UIDropdownTime.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            UILabelTimeSelection.transform.localScale = new Vector3(0, 0, 0);
            UIDropdownTime.transform.localScale = new Vector3(0, 0, 0);
            UIDropdownTime.GetComponent<Dropdown>().value = 0;
        }

        /* via tempVariable bool visibilitybutton  = false 
         * if activity.manualTimespanSelection == true and time selected --> visibilityButton = true;
         * depending on visibilitybutton variable show/hide Button (and also the total time info)

       */

        bool tempVisiblityButton = false;


        if (UIDropdownTime.GetComponent<Dropdown>().value != 0)
        {
            tempVisiblityButton = true;
        }

        if (tempVisiblityButton)
        {
            UIButtonStartTrip.transform.localScale = new Vector3(1, 1, 1);

            totalDuration = (int)regionHandlerList[UIDropdownRegion.GetComponent<Dropdown>().value - 1].travelTimeToHome*2 + UIDropdownTime.GetComponent<Dropdown>().value;
            UILabelTotalTime.transform.localScale = new Vector3(1, 1, 1);
            UILabelTotalTime.GetComponent<UnityEngine.UI.Text>().text = "Total Duration: " + totalDuration + " Hours";
        }
        else
        {
            UIButtonStartTrip.transform.localScale = new Vector3(0, 0, 0);
            UILabelTotalTime.transform.localScale = new Vector3(0, 0, 0);
        }



    }


    public void DropdownRegionChanged()
    {
        UpdateElements();
    }

    public void DropdownActivityChanged()
    {
        UpdateElements();
    }

    public void DropdownIngredientChanged()
    {
        UpdateElements();
    }

    public void DropdownTimeChanged()
    {
        UpdateElements();
    }

    public void RadioAllAvailableIngredientsSelected()
    {
        UIRadioSpecificIngredient.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
        //UIRadioAllAvailableIngredients.GetComponent<Toggle>().SetIsOnWithoutNotify(true);
        UpdateElements();
    }

    public void UpdateIngredientDropdown()
    {
        
        List<Dropdown.OptionData> ingredientsDropdownOptions = new List<Dropdown.OptionData>();
        region = regionHandlerList[UIDropdownRegion.GetComponent<Dropdown>().value-1];


        curatedIngredientTypeList.Clear();
        curatedRaritiesList.Clear();

        ingredientsDropdownOptions.Add(new Dropdown.OptionData("Click to Select"));

        switch (GameTime.currentSeason)
        {

            case GameTime.season.SPRING:

                for (int i = 0; i < region.ingredientTypes.Count; i++)
                {
                    if (region.raritiesSpring[i] != RegionHandler.rarity.NONE)
                    {
                        curatedIngredientTypeList.Add(region.ingredientTypes[i]);
                        curatedRaritiesList.Add(region.raritiesSpring[i]);
                        ingredientsDropdownOptions.Add(new Dropdown.OptionData(region.ingredientTypes[i].ingredientName + " (" + RegionHandler.RarityToString(region.raritiesSpring[i]) + ")"));
                    }

                }

                break;

            case GameTime.season.SUMMER:

                for (int i = 0; i < region.ingredientTypes.Count; i++)
                {
                    if (region.raritiesSummer[i] != RegionHandler.rarity.NONE)
                    {
                        curatedIngredientTypeList.Add(region.ingredientTypes[i]);
                        curatedRaritiesList.Add(region.raritiesSummer[i]);
                        ingredientsDropdownOptions.Add(new Dropdown.OptionData(region.ingredientTypes[i].ingredientName + " (" + RegionHandler.RarityToString(region.raritiesSummer[i]) + ")"));
                    }

                }

                break;

            case GameTime.season.AUTUMN:

                for (int i = 0; i < region.ingredientTypes.Count; i++)
                {
                    if (region.raritiesAutumn[i] != RegionHandler.rarity.NONE)
                    {
                        curatedIngredientTypeList.Add(region.ingredientTypes[i]);
                        curatedRaritiesList.Add(region.raritiesAutumn[i]);
                        ingredientsDropdownOptions.Add(new Dropdown.OptionData(region.ingredientTypes[i].ingredientName + " (" + RegionHandler.RarityToString(region.raritiesAutumn[i]) + ")"));
                    }

                }

                break;

            case GameTime.season.WINTER:

                for (int i = 0; i < region.ingredientTypes.Count; i++)
                {
                    if (region.raritiesWinter[i] != RegionHandler.rarity.NONE)
                    {
                        curatedIngredientTypeList.Add(region.ingredientTypes[i]);
                        curatedRaritiesList.Add(region.raritiesWinter[i]);
                        ingredientsDropdownOptions.Add(new Dropdown.OptionData(region.ingredientTypes[i].ingredientName + " (" + RegionHandler.RarityToString(region.raritiesWinter[i]) + ")"));
                    }

                }

                break;
        }

        if (CheckMatch(oldIngredientList, curatedIngredientTypeList))
        {
            //Debug.Log("SAME LISTS");
        }
        else
        {
            //Debug.Log("DIFFERENT LISTS");
            UIDropdownIngredient.GetComponent<Dropdown>().ClearOptions();
            UIDropdownIngredient.GetComponent<Dropdown>().AddOptions(ingredientsDropdownOptions);
        }

        // for future comparisons to see if an update of the list is in order
        //oldIngredientList = curatedIngredientTypeList;
        oldIngredientList.Clear();
        foreach (IngredientType iT in curatedIngredientTypeList)
        {
            oldIngredientList.Add(iT);
        }

        
    }

    public void RadioSpecificIngredientSelected()
    {
        UIRadioAllAvailableIngredients.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
        //UIRadioSpecificIngredient.GetComponent<Toggle>().SetIsOnWithoutNotify(true);

        UpdateElements();
    }

    bool CheckMatch(List <IngredientType> l1, List<IngredientType> l2)
    {
        if (l1 == null && l2 == null)
        {
            //Debug.Log("true, both null:  oldList: " + l1.Count + ",  curatedList: " + l2.Count);
            return true;
        }
        else if (l1 == null || l2 == null)
        {
            //Debug.Log("false, only 1 is null");
            return false;
        }

        if (l1.Count != l2.Count)
        {
            //Debug.Log("false, different list counts: oldList: " + l1.Count + ",  curatedList: " + l2.Count);
            return false;
        }
            
        for (int i = 0; i < l1.Count; i++)
        {
            if (l1[i] != l2[i])
            {
                //Debug.Log("false, item " + i + " is different  oldList: " + l1.Count + ",  curatedList: " + l2.Count );
                return false;
            }
                
        }
        //Debug.Log("true, identical existing lists, oldList: " + l1.Count + ",  curatedList: " + l2.Count);
        return true;
    }


    public void StartTrip()
    {
        int activityDuration = UIDropdownTime.GetComponent<Dropdown>().value;
        int travelDuration = (int)regionHandlerList[UIDropdownRegion.GetComponent<Dropdown>().value - 1].travelTimeToHome;

        GetComponent<CanvasContainerHandler>().CloseContainerView();
        //tripHandler.GetComponent<CanvasContainerHandler>().OpenContainerView();

        tripHandler.GetComponent<TripHandler>().ExecuteTrip(selectedActivity, selectedIngredientType, selectedIngredientRarity, totalDuration, activityDuration, travelDuration, curatedIngredientTypeList, curatedRaritiesList, region);

    }
}
