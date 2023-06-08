using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookRegionDetails : MonoBehaviour
{

    public RegionHandler region;
    public Transform IngredientsPanelsParent;
    public List<Transform> ingredientPanels;

    public GameObject UIRegionName;
    public GameObject UIRegionImage;
    public GameObject UIRegionDescription;
    public GameObject UITimeToGetThere;
    public GameObject UIIngredientsDiscovered;

    public NotebookIngredientDetails ingredientDetailPage;

    public List<Sprite> abundanceSprites;




    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in IngredientsPanelsParent)
        {
            ingredientPanels.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(RegionHandler pRegion)
    {
        region = pRegion;

        NotebookBaseUI.AddToHistory(this.gameObject, region.gameObject);

        UpdateRegionDetails();

        GetComponent<NotebookBaseUI>().Open();
    }
    private int CalculateIngredientsDiscovered()
    {
        int counterKnown = 0;
        foreach (IngredientType iT in region.ingredientTypes)
        {
            if (iT.knownToPlayer) counterKnown++;

        }
        return (100*counterKnown/region.ingredientTypes.Count);
    }

    public void UpdateRegionDetails()
    {
        UIRegionName.GetComponent<UnityEngine.UI.Text>().text = region.regionName;
        UIRegionImage.GetComponent<UnityEngine.UI.Image>().sprite = region.image;
        UIRegionDescription.GetComponent<UnityEngine.UI.Text>().text = region.description;
        UITimeToGetThere.GetComponent<UnityEngine.UI.Text>().text = (int)region.travelTimeToHome + " Hours";
        UIIngredientsDiscovered.GetComponent<UnityEngine.UI.Text>().text = CalculateIngredientsDiscovered() + " %";

        // set ingredient panels
        for (int i=0; i< ingredientPanels.Count; i++)
        {
            
            // visiblize and fill the panels for which there is an igredient
            if(i< region.ingredientTypes.Count)
            {
                ingredientPanels[i].localScale = new Vector3(1, 1, 1);
                ingredientPanels[i].GetChild(0).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = region.ingredientTypes[i].inventorySprite;
                ingredientPanels[i].GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = region.ingredientTypes[i].ingredientName;

                // abundance indicators
                
                switch(region.raritiesSpring[i])
                {
                    case RegionHandler.rarity.NONE: 
                        ingredientPanels[i].GetChild(2).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[0];
                        ingredientPanels[i].GetChild(2).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "None";
                        break; 
                    case RegionHandler.rarity.VERY_RARE: 
                        ingredientPanels[i].GetChild(2).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[1];
                        ingredientPanels[i].GetChild(2).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "Very Rare";
                        break;
                    case RegionHandler.rarity.RARE: 
                        ingredientPanels[i].GetChild(2).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[2];
                        ingredientPanels[i].GetChild(2).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "Rare";
                        break;
                    case RegionHandler.rarity.MEDIUM: 
                        ingredientPanels[i].GetChild(2).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[3];
                        ingredientPanels[i].GetChild(2).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "Medium";
                        break;
                    case RegionHandler.rarity.COMMON: 
                        ingredientPanels[i].GetChild(2).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[4];
                        ingredientPanels[i].GetChild(2).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "Common";
                        break;
                    case RegionHandler.rarity.ABUNDAND: 
                        ingredientPanels[i].GetChild(2).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[5];
                        ingredientPanels[i].GetChild(2).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "Abundant";
                        break;
                }

                switch (region.raritiesSummer[i])
                {
                    case RegionHandler.rarity.NONE:
                        ingredientPanels[i].GetChild(3).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[0];
                        ingredientPanels[i].GetChild(3).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "None";
                        break;
                    case RegionHandler.rarity.VERY_RARE:
                        ingredientPanels[i].GetChild(3).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[1];
                        ingredientPanels[i].GetChild(3).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "Very Rare";
                        break;
                    case RegionHandler.rarity.RARE:
                        ingredientPanels[i].GetChild(3).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[2];
                        ingredientPanels[i].GetChild(3).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "Rare";
                        break;
                    case RegionHandler.rarity.MEDIUM:
                        ingredientPanels[i].GetChild(3).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[3];
                        ingredientPanels[i].GetChild(3).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "Medium";
                        break;
                    case RegionHandler.rarity.COMMON:
                        ingredientPanels[i].GetChild(3).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[4];
                        ingredientPanels[i].GetChild(3).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "Common";
                        break;
                    case RegionHandler.rarity.ABUNDAND:
                        ingredientPanels[i].GetChild(3).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[5];
                        ingredientPanels[i].GetChild(3).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "Abundant";
                        break;
                }

                switch (region.raritiesAutumn[i])
                {
                    case RegionHandler.rarity.NONE:
                        ingredientPanels[i].GetChild(4).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[0];
                        ingredientPanels[i].GetChild(4).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "None";
                        break;
                    case RegionHandler.rarity.VERY_RARE:
                        ingredientPanels[i].GetChild(4).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[1];
                        ingredientPanels[i].GetChild(4).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "Very Rare";
                        break;
                    case RegionHandler.rarity.RARE:
                        ingredientPanels[i].GetChild(4).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[2];
                        ingredientPanels[i].GetChild(4).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "Rare";
                        break;
                    case RegionHandler.rarity.MEDIUM:
                        ingredientPanels[i].GetChild(4).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[3];
                        ingredientPanels[i].GetChild(4).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "Medium";
                        break;
                    case RegionHandler.rarity.COMMON:
                        ingredientPanels[i].GetChild(4).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[4];
                        ingredientPanels[i].GetChild(4).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "Common";
                        break;
                    case RegionHandler.rarity.ABUNDAND:
                        ingredientPanels[i].GetChild(4).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[5];
                        ingredientPanels[i].GetChild(4).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "Abundant";
                        break;
                }

                switch (region.raritiesWinter[i])
                {
                    case RegionHandler.rarity.NONE:
                        ingredientPanels[i].GetChild(5).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[0];
                        ingredientPanels[i].GetChild(5).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "None";
                        break;
                    case RegionHandler.rarity.VERY_RARE:
                        ingredientPanels[i].GetChild(5).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[1];
                        ingredientPanels[i].GetChild(5).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "Very Rare";
                        break;
                    case RegionHandler.rarity.RARE:
                        ingredientPanels[i].GetChild(5).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[2];
                        ingredientPanels[i].GetChild(5).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "Rare";
                        break;
                    case RegionHandler.rarity.MEDIUM:
                        ingredientPanels[i].GetChild(5).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[3];
                        ingredientPanels[i].GetChild(5).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "Medium";
                        break;
                    case RegionHandler.rarity.COMMON:
                        ingredientPanels[i].GetChild(5).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[4];
                        ingredientPanels[i].GetChild(5).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "Common";
                        break;
                    case RegionHandler.rarity.ABUNDAND:
                        ingredientPanels[i].GetChild(5).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = abundanceSprites[5];
                        ingredientPanels[i].GetChild(5).gameObject.GetComponent<TooltipUITargetHandler>().tooltipText = "Abundant";
                        break;
                }

            }
            else
            {
                // hide all the other panels for which no ingredient exists
                ingredientPanels[i].localScale = new Vector3(0, 0, 0);
                
            }
        }
    }

    public void OpenIngredientDetails(int i)
    {
        GetComponent<NotebookBaseUI>().Close();
        ingredientDetailPage.Open(region.ingredientTypes[i]);
    }

}
