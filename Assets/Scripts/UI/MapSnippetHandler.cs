using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSnippetHandler : MonoBehaviour
{
    public RegionHandler region;
    public GameObject dropdownRegions;
    public TripPlanningHandler tripPlanningHelper;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.9f;
        SetToolTipText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TripPlannerClickHandler()
    {
        dropdownRegions.GetComponent<Dropdown>().value = dropdownRegions.GetComponent<Dropdown>().options.FindIndex(option => option.text == region.regionName);
        tripPlanningHelper.UpdateVisibilityOfElements();
    }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject." + region.regionName);
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject." + region.regionName);
    }

    private void SetToolTipText()
    {
            GetComponent<TooltipUITargetHandler>().tooltipText = region.regionName;
    }


}
