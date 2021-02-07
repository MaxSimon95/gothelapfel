using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemHandler : MonoBehaviour
{

    public List<IngredientType> ingredientTypes = new List<IngredientType>();
    public List<int> ingredientTypeAmounts = new List<int>();
    public int amountTotal;
    public float temperature;
    private float updateItemContentWaitTime;
    private float updateTemperatureWaitTime = 0.5f;

    private IEnumerator updateItemContentCoroutine;
    private IEnumerator updateTemperatureCoroutine;

    private static Transform autoTransferTargetParent;

    public List<IngredientEffect> IngredientEffects;
    public List<float> IngredientEffectIntensities;

   


    void Start()
    {
 
        updateItemContentWaitTime = Random.Range(8.0f, 12.0f);


        updateItemContentCoroutine = UpdateItemContentCoroutine(updateItemContentWaitTime);
        updateTemperatureCoroutine = UpdateTemperatureCoroutine(updateTemperatureWaitTime);

        StartCoroutine(updateItemContentCoroutine);
        StartCoroutine(updateTemperatureCoroutine);


        

    }

    private IEnumerator UpdateTemperatureCoroutine(float waitTime)
    {
 
        while (true)
        {
 
            if (name != "InventoryItemPrefab")
            {
                UpdateTemperature();
            }
            yield return new WaitForSeconds(waitTime);
  
        }
    }

    private IEnumerator UpdateItemContentCoroutine(float waitTime)
    {

        while (true)
        {
            if (name != "InventoryItemPrefab")
            {

                UpdateItemContent();
            }
            yield return new WaitForSeconds(waitTime);
 
        }
    }
    
    void Update()
    {
        
    }

    public static void ResetAutoTransferTargetParent()
    {
        foreach (Transform child in GameObject.Find("PanelInventory").transform)
        {

            if (child.gameObject.name == "PanelInventorySlots")
            {

                autoTransferTargetParent = child;
 
            }
        }
    }

    public static void SetAutoTransferTargetParent(GameObject target)
    {
        autoTransferTargetParent = target.transform;
    }

    public void TransferItemAutomatically()
    {
        
        GameObject targetPanelInventorySlots = null;
        // check if its in the inventory or anywhere else, and depending on that were to put it now

        if (gameObject.transform.parent.parent.parent.gameObject.name == "PanelInventory")
        {
            // the clicked item is in the inventory, therefore transfer item in the autoTransferTargetParent's slots
            // which should be set according to the opened canvas

            targetPanelInventorySlots = autoTransferTargetParent.gameObject;

        }
        else
        {
            // the clicked item is not in the inventory but in a itemslot somewhere else. it shall be transferred to the inventory.

            foreach (Transform child in GameObject.Find("PanelInventory").transform)
            {
 
                if (child.gameObject.name == "PanelInventorySlots")
                {

                    targetPanelInventorySlots = child.gameObject;

                }
            }
        }



        ItemSlotHandler targetItemSlot = null;
        bool itemMergeSlotFound = false;

        //  step 1: iterate through all fitting slots and see if there's any one which already has the correct ingredient inside so they can be merged, 
       

        //check if the item to be moved has exactly 1 ingredient

        if (gameObject.GetComponent<InventoryItemHandler>().ingredientTypes.Count == 1)
        {
            foreach (Transform child in targetPanelInventorySlots.transform)
            {
                //check if slot really is a slot 
                if (child.GetComponent<ItemSlotHandler>() != null)
                {
                    //check if the slot has an item
                    if (child.transform.childCount == 1)
                    {
                        // check if the item in the slot has exactly 1 ingredient
                        if (child.transform.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().ingredientTypes.Count == 1)
                        {
                            //check if the ingredients match
                            if (child.transform.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().ingredientTypes[0] == gameObject.GetComponent<InventoryItemHandler>().ingredientTypes[0])
                            {
                                //check that the item in slot is not the exact same item we want to reposition
                                if (child.transform.GetChild(0) != gameObject.transform)
                                {

                                    targetItemSlot = child.gameObject.gameObject.GetComponent<ItemSlotHandler>();
                                    itemMergeSlotFound = true;

                                    break;
                                }
                            }
                        }
                    }
                }

            }
        }

        // step 2: iterate through all fitting slots and see if there is a free slot (if there is no slot for merging)
        if(!itemMergeSlotFound)
        {
            foreach (Transform child in targetPanelInventorySlots.transform)
            {
                
                //check if thr loop comes across the origin slot before it reaches any free slot --> in that case stop right there and don't switch anything.
                // this only works before we do not even reach this loop if we found something appicable for a merge. That would have a higher priority.

                if(child.transform == gameObject.transform.parent)
                {
                    return;
                }
                
                //check if slot is free
                if (child.transform.childCount == 0)
                {
                    //check if slot really is a slot 
                    if (child.GetComponent<ItemSlotHandler>() != null)
                    {
                        targetItemSlot = child.gameObject.gameObject.GetComponent<ItemSlotHandler>();
                        break;
                    }

                }
            }
        }

        

        

        // step 3: move item into target slot
        if (targetItemSlot != null)
        {
            Debug.Log("target Itemslot for instant transfer set");


            targetItemSlot.PutItemIntoSlot(gameObject.GetComponent<InventoryItemHandler>());
        }
        else
        {
            Debug.Log("no applicable itemslot for instant transfer found");
        }

    }

    private void SetToolTipText()
    {


        // just one ingredienttype: use its ingredienttype name for tooltip text
        if (ingredientTypes.Count == 1)
        {
            GetComponent<TooltipUITargetHandler>().tooltipText = ingredientTypes[0].GetComponent<IngredientType>().ingredientName;
        }

        // more than one: Say "Unknown Mixture" --> Can be fancied up later on, with all sorts of fun special cases we cover here (any mix of salt + water gets called salt water for example)
        else
        {
            GetComponent<TooltipUITargetHandler>().tooltipText = "Mysterious Mixture";
        }
    }

    public void UpdateTemperature()
    {
        float temperatureChange;
        float targetTemperature;


        // adjust target temperature

        if (
            (transform.parent.GetComponent<ItemSlotHandler>() == null)
            ||(transform.parent.GetComponent<ItemSlotHandler>().associatedContainer == null)
            || (transform.parent.GetComponent<ItemSlotHandler>().associatedContainer.name == "PanelInventory")
            )
        {
            targetTemperature = GameObject.Find("PlayerCharacter").GetComponent<PlayerCharacter>().currentRoom.GetComponent<RoomHandler>().temperature;
        }
        else
        {
            targetTemperature = transform.parent.GetComponent<ItemSlotHandler>().associatedContainer.GetComponent<AlchemyStorageContainer>().temperature;
        }
        


        temperatureChange = (targetTemperature - temperature) * 0.1f;
        temperature += temperatureChange;

        // minimal unterschiedliche temperaturen gleichsetzen
        if (((targetTemperature - temperature) < 1) && ((targetTemperature - temperature) > -1))
        {
            temperature = targetTemperature;
        }

    }

    public void UpdateItemContent()
    {
        
        MergeIdenticalIngredients();

        


        if ((transform.parent.GetComponent<ItemSlotHandler>() != null)&&(transform.parent.GetComponent<ItemSlotHandler>().associatedContainer != null))
        {
            GameObject.Find("AlchemyEngine").GetComponent<AlchemyEngineLogic>().CheckForFittingAlchemyReaction(ingredientTypes, ingredientTypeAmounts, temperature, transform.parent.GetComponent<ItemSlotHandler>().associatedContainer.name);
            //Debug.Log("PARENTAL: " + transform.parent.GetComponent<ItemSlotHandler>().associatedContainer.name);
        }
        else
        {
            GameObject.Find("AlchemyEngine").GetComponent<AlchemyEngineLogic>().CheckForFittingAlchemyReaction(ingredientTypes, ingredientTypeAmounts, temperature, "surrounding-in-which-reaction-happened-not-set");
            //Debug.Log("PARENTAL: " + "surrounding-in-which-reaction-happened-not-set");
        }
        

        DeleteIngredientIfEmpty();
        MergeIdenticalIngredients();

        amountTotal = ingredientTypeAmounts.Sum();

        if (amountTotal > 0)
            transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = amountTotal.ToString();
        else
        {
            Destroy(gameObject);
        }
            

        // adjust inventoryitem sprite
        if (ingredientTypes.Count > 0)
        {
            // if just one ingredienttype , take it's ingredienttype sprite
            if (ingredientTypes.Count == 1)
                transform.GetChild(0).gameObject.GetComponent<Image>().sprite = ingredientTypes[0].GetComponent<IngredientType>().inventorySprite;
            
            // if more than one ingredienttype, take a designated mixture sprite, or do something fancy later on. here we can add FUN little mechanics to make it more fancy as we see fit.
            else
            {
                transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("ph_ingredient_mix");
            }
        }
            
        else
        {
            Debug.Log("Destroyed because ingredientTypes count 0");
            DeleteInstanceOfInventoryItem();
        }

        UpdateIngredientEffects();
        SetToolTipText();

    }

    private void DeleteIngredientIfEmpty()
    {
        for (int index = ingredientTypeAmounts.Count-1; index >= 0; index--)
        {
            if(ingredientTypeAmounts[index] <= 0 )
            {
                ingredientTypeAmounts.RemoveAt(index);
                ingredientTypes.RemoveAt(index);
            }
        }
    }

    public void AddIngredientAndUpdate(IngredientType ingredientType, int amount)
    {
        AddIngredient(ingredientType, amount);
        UpdateItemContent();

    }

    public void AddIngredient(IngredientType ingredientType, int amount)
    {
        AddIngredient(ingredientType, amount, temperature);
    }

    public void AddIngredient(IngredientType ingredientType, int amount, float addedIngredientTemperature)
    {
  

        ingredientTypes.Add(ingredientType);
        ingredientTypeAmounts.Add(amount);

    
        temperature = ((float)amountTotal / ((float)amountTotal + (float)amount)) * temperature + ((float)amount / ((float)amountTotal + (float)amount)) * addedIngredientTemperature;
    
    }

    private void MergeIdenticalIngredients()
    {
        List<IngredientType> newIngredientTypeList = new List<IngredientType>();
        List<int> newIngredientTypeAmountsList = new List<int>();

        for (int index = 0; index < ingredientTypes.Count; index++)
        {
            IngredientType tempIngredientType = ingredientTypes[index];

            // wenn nicht in neuerliste, dann alle vorkommen dieses typs jetzt in diese liste überführen
            if (!newIngredientTypeList.Contains(tempIngredientType))
            {
                int amountTemp = 0;

                // hochzählen für jedes vorkommen
                for (int index2 = 0; index2 < ingredientTypes.Count; index2++)
                {

                    if (ingredientTypes[index2] == tempIngredientType)
                    {
                        amountTemp += ingredientTypeAmounts[index2];
                    }
                }

                newIngredientTypeList.Add(tempIngredientType);
                newIngredientTypeAmountsList.Add(amountTemp);
                // am ende in neue liste hinzufügen mit zusammengezähltem vorkommen
            }

        }

        // alte liste durch neue ersetzen
        ingredientTypes = newIngredientTypeList;
        ingredientTypeAmounts = newIngredientTypeAmountsList;
        

    }

    public void DeleteInstanceOfInventoryItem()
    {
        // this ensures that the transfer buttons dont get into trouble with their buttonactive checks when they check whether their connected item slot content is == 0
        transform.SetParent(GameObject.Find("CanvasDragItem").transform);

        Destroy(gameObject);
    }

    public void ButtonPress()
    {

    }

    public void UpdateIngredientEffects()
    {
        // cleanse old list
        IngredientEffects = new List<IngredientEffect>();
        IngredientEffectIntensities = new List<float>();

        // go through all ingredients
        for(int i=0; i<ingredientTypes.Count; i++)
        {
            // go through all effects: if its already been logged, add into that slot, if not, log it newly
            for(int e = 0; e < ingredientTypes[i].effects.Count; e++)
            {
                if(IngredientEffects.Contains(ingredientTypes[i].effects[e]))
                {
                    int t = IngredientEffects.IndexOf(ingredientTypes[i].effects[e]);
                    IngredientEffectIntensities[t] += ingredientTypes[i].effectIntensities[e] * ingredientTypeAmounts[i];
                }
                else
                {
                    IngredientEffects.Add(ingredientTypes[i].effects[e]);
                    IngredientEffectIntensities.Add(ingredientTypes[i].effectIntensities[e] * ingredientTypeAmounts[i]);
                }
            }
        }
        
        for(int j = 0; j < IngredientEffectIntensities.Count; j++)
        {
            IngredientEffectIntensities[j] = IngredientEffectIntensities[j] / ingredientTypeAmounts.Sum();
        }

    }

}
