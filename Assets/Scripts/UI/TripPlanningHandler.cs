using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TripPlanningHandler : MonoBehaviour
{
    public GameObject regions;
    public List<RegionHandler> regionHandlerList = new List<RegionHandler>();
    public List<IngredientType> curatedIngredientTypeList = new List<IngredientType>();
    List<Dropdown.OptionData> regionsDropdownOptions = new List<Dropdown.OptionData>();

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
        UIDropdownRegion.GetComponent<Dropdown>().ClearOptions();

        foreach (Transform region in regions.transform)
        {
            regionHandlerList.Add(region.GetComponent<RegionHandler>());
            //Debug.Log(region.GetComponent<RegionHandler>().regionName);
            regionsDropdownOptions.Add(new Dropdown.OptionData(region.GetComponent<RegionHandler>().regionName));
        }

        UIDropdownRegion.GetComponent<Dropdown>().AddOptions(regionsDropdownOptions);

        UpdateVisibilityOfElements();
    }

    public void CloseTripPlanner()
    {

    }

    // updateVisibility based on conducted selections
    public void UpdateVisibilityOfElements()
    {
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
            UIDropdownIngredient.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            UIDropdownIngredient.transform.localScale = new Vector3(0, 0, 0);

            // clean up values down the line
            UIDropdownIngredient.GetComponent<Dropdown>().value = 0;
        }


        /* 
         * if activity.manualTimespanSelection == true 
         * and (all available ingredients toggle is set or (specific ingredient toggle set && specific ingredient dropdown selected)),
         * show time selection --> else hide

        */



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
         * depending on visibilitybutton variable show/hide Button

       */

        bool tempVisiblityButton = false;

        if (UIDropdownTime.GetComponent<Dropdown>().value != 0)
        {
            tempVisiblityButton = true;
        }

        if (tempVisiblityButton)
        {
            UIButtonStartTrip.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            UIButtonStartTrip.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public void DropdownRegionChanged()
    {
        UpdateIngredientDropdown();
        UpdateVisibilityOfElements();
    }

    public void DropdownActivityChanged()
    {
        UpdateVisibilityOfElements();
    }

    public void DropdownIngredientChanged()
    {
        UpdateVisibilityOfElements();
    }

    public void DropdownTimeChanged()
    {
        UpdateVisibilityOfElements();
    }

    public void RadioAllAvailableIngredientsSelected()
    {
        UIRadioSpecificIngredient.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
        //UIRadioAllAvailableIngredients.GetComponent<Toggle>().SetIsOnWithoutNotify(true);
        UpdateVisibilityOfElements();
    }

    public void UpdateIngredientDropdown()
    {
        //
        List<Dropdown.OptionData> ingredientsDropdownOptions = new List<Dropdown.OptionData>();
        RegionHandler region = regionHandlerList[UIDropdownRegion.GetComponent<Dropdown>().value-1];

        curatedIngredientTypeList.Clear();
        UIDropdownIngredient.GetComponent<Dropdown>().ClearOptions();


        ingredientsDropdownOptions.Add(new Dropdown.OptionData("Click to Select"));

        switch (GameTime.currentSeason)
        {

            case GameTime.season.SPRING:

                for (int i = 0; i < region.ingredientTypes.Count; i++)
                {
                    if (region.raritiesSpring[i] != RegionHandler.rarity.NONE)
                    {
                        curatedIngredientTypeList.Add(region.ingredientTypes[i]);
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
                        ingredientsDropdownOptions.Add(new Dropdown.OptionData(region.ingredientTypes[i].ingredientName + " (" + RegionHandler.RarityToString(region.raritiesAutumn[i]) + ")"));
                    }

                }

                break;

            case GameTime.season.WINTER:

                for (int i = 0; i < region.ingredientTypes.Count; i++)
                {
                    if (region.raritiesAutumn[i] != RegionHandler.rarity.NONE)
                    {
                        curatedIngredientTypeList.Add(region.ingredientTypes[i]);
                        ingredientsDropdownOptions.Add(new Dropdown.OptionData(region.ingredientTypes[i].ingredientName + " (" + RegionHandler.RarityToString(region.raritiesWinter[i]) + ")"));
                    }

                }

                break;
        }



        UIDropdownIngredient.GetComponent<Dropdown>().AddOptions(ingredientsDropdownOptions);
    }

    public void RadioSpecificIngredientSelected()
    {
        UIRadioAllAvailableIngredients.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
        //UIRadioSpecificIngredient.GetComponent<Toggle>().SetIsOnWithoutNotify(true);

        UpdateVisibilityOfElements();
    }

    void StartTrip()
    {

    }
}
