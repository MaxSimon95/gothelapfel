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


    /*  IEnumerator Start()
      {
          updateItemContentWaitTime = Random.Range(8.0f, 12.0f);
          //Debug.Log(updateItemContentWaitTime);

          while (true)
          {

              UpdateTemperature();
              UpdateItemContent();

          yield return new WaitForSeconds(updateItemContentWaitTime);
          }
      } */


    void Start()
    {
        

        updateItemContentWaitTime = Random.Range(8.0f, 12.0f);
        //Debug.Log("Start");

        updateItemContentCoroutine = UpdateItemContentCoroutine(updateItemContentWaitTime);
        updateTemperatureCoroutine = UpdateTemperatureCoroutine(updateTemperatureWaitTime);

        StartCoroutine(updateItemContentCoroutine);
        StartCoroutine(updateTemperatureCoroutine);

        //Debug.Log("Start End");

    }

    private IEnumerator UpdateTemperatureCoroutine(float waitTime)
    {
       // Debug.Log("UpdateTemperatureCourtine START");
        while (true)
        {
            //Debug.Log("temperature Loop Start");
            if (name != "InventoryItemPrefab")
            {
                UpdateTemperature();
            }
            yield return new WaitForSeconds(waitTime);
            //print("WaitAndPrint " + Time.time);
        }
    }

    private IEnumerator UpdateItemContentCoroutine(float waitTime)
    {
        //Debug.Log("UpdateItemCourotine START");
        while (true)
        {
            if (name != "InventoryItemPrefab")
            {
                //Debug.Log("Item Loop start");
                UpdateItemContent();
            }
            yield return new WaitForSeconds(waitTime);
            //print("WaitAndPrint " + Time.time);
        }
    }
    
    void Update()
    {
        
    }
    /*
     * 
    public void TransferItemAutomatically(InventoryItemHandler transferItem, ItemSlotHandler originalSlot)
    {
        GameObject targetPanelInventorySlots = null;
        // check if its in the inventory or anywhere else, and depending on that were to put it now
        //Debug.Log(originalSlot.transform.parent.parent.gameObject.name);
        if (originalSlot.transform.parent.parent.gameObject.name == "PanelInventory")
        {
            // the clicked item is in the inventory, therefore transfer item in the inventory's current secondary inventoryslots-panel 
            // which should be set according to the opened canvas

            // TODO replace provisional solution with actual solution
            foreach (Transform child in GameObject.Find("PanelInventory").transform)
            {
                //Debug.Log(child.gameObject.name);
                if (child.gameObject.name == "PanelInventorySlots")
                {

                    targetPanelInventorySlots = child.gameObject;
                    //Debug.Log(targetPanelInventorySlots.name);
                }
            }
            // END TODO
        }
        else
        {
            // the clicked item is not in the inventory but in a secondary inventoryslots-panel. it shall be transferred to the inventory.


            foreach (Transform child in GameObject.Find("PanelInventory").transform)
            {
                //Debug.Log(child.gameObject.name);
                if (child.gameObject.name == "PanelInventorySlots")
                {

                    targetPanelInventorySlots = child.gameObject;
                    //Debug.Log(targetPanelInventorySlots.name);
                }
            }
        }

        ItemSlotHandler targetItemSlot = null;

        // step 1: iterate through all fitting slots and see if there is a free slot
        foreach (Transform child in targetPanelInventorySlots.transform)
        {
            //Debug.Log(child.gameObject.name);
            if (child.transform.childCount == 0)
            {

                targetItemSlot = child.gameObject.gameObject.GetComponent<ItemSlotHandler>();
                //Debug.Log(targetPanelInventorySlots.name);
                break;
            }
        }

        //  step 2: iterate through all fitting slots and see if there's any one which already has the correct ingredient inside so they can be merged, 
        // in that case overwrite previous targetSlot

        //check if the item to be moved has exactly 1 ingredient
        Debug.Log("check if the item to be moved has exactly 1 ingredient: " + (originalSlot.transform.GetChild(0).GetComponent<InventoryItemHandler>().ingredientTypes.Count == 1));
        Debug.Log(originalSlot.transform.GetChild(0));
        Debug.Log(originalSlot.transform.GetChild(0).GetComponent<InventoryItemHandler>().ingredientTypes.Count);
        if (originalSlot.transform.GetChild(0).GetComponent<InventoryItemHandler>().ingredientTypes.Count == 1)
        {
            foreach (Transform child in targetPanelInventorySlots.transform)
            {

                //check if the slot has an item
                if (child.transform.childCount == 1)
                {
                    // check if the item in the slot has exactly 1 ingredient
                    if (child.transform.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().ingredientTypes.Count == 1)
                    {
                        Debug.Log("check if the item in the target slot has exactly 1 ingredient: " + (child.transform.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().ingredientTypes.Count == 1));
                        //check if the ingredients match
                        if (child.transform.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().ingredientTypes[0] == originalSlot.transform.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().ingredientTypes[0])
                        {
                            //check that the item in slot is not the exact same item we want to reposition
                            if (child.transform.GetChild(0) != originalSlot.transform.GetChild(0))
                            {

                                targetItemSlot = child.gameObject.gameObject.GetComponent<ItemSlotHandler>();

                                Debug.Log("item slot applicable for merge found" + targetItemSlot);

                                break;
                            }
                        }
                    }
                }
            }
        }

        // step 3: move item into target slot
        if (targetItemSlot != null)
        {
            Debug.Log("target Itemslot for instant transfer set");
            Debug.Log(targetItemSlot);

    targetItemSlot.PutItemIntoSlot(transferItem);
        }
        else
        {
            Debug.Log("no applicable itemslot for instant transfer found");
        }

    }

     **/

    public void TransferItemAutomatically()
    {
        
        GameObject targetPanelInventorySlots = null;
        // check if its in the inventory or anywhere else, and depending on that were to put it now
        //Debug.Log(originalSlot.transform.parent.parent.gameObject.name);
        if (gameObject.transform.parent.parent.parent.gameObject.name == "PanelInventory")
        {
            // the clicked item is in the inventory, therefore transfer item in the inventory's current secondary inventoryslots-panel 
            // which should be set according to the opened canvas

            // TODO replace provisional solution with actual solution
            foreach (Transform child in GameObject.Find("PanelInventory").transform)
            {
                //Debug.Log(child.gameObject.name);
                if (child.gameObject.name == "PanelInventorySlots")
                {

                    targetPanelInventorySlots = child.gameObject;
                    //Debug.Log(targetPanelInventorySlots.name);
                }
            }
            // END TODO
        }
        else
        {
            // the clicked item is not in the inventory but in a secondary inventoryslots-panel. it shall be transferred to the inventory.


            foreach (Transform child in GameObject.Find("PanelInventory").transform)
            {
                //Debug.Log(child.gameObject.name);
                if (child.gameObject.name == "PanelInventorySlots")
                {

                    targetPanelInventorySlots = child.gameObject;
                    //Debug.Log(targetPanelInventorySlots.name);
                }
            }
        }

        ItemSlotHandler targetItemSlot = null;

        // step 1: iterate through all fitting slots and see if there is a free slot
        foreach (Transform child in targetPanelInventorySlots.transform)
        {
            //Debug.Log(child.gameObject.name);
            if (child.transform.childCount == 0)
            {

                targetItemSlot = child.gameObject.gameObject.GetComponent<ItemSlotHandler>();
                //Debug.Log(targetPanelInventorySlots.name);
                break;
            }
        }

        //  step 2: iterate through all fitting slots and see if there's any one which already has the correct ingredient inside so they can be merged, 
        // in that case overwrite previous targetSlot

        //check if the item to be moved has exactly 1 ingredient
        Debug.Log("check if the item to be moved has exactly 1 ingredient: " + (gameObject.GetComponent<InventoryItemHandler>().ingredientTypes.Count == 1));
        Debug.Log(gameObject);
        Debug.Log(gameObject.GetComponent<InventoryItemHandler>().ingredientTypes.Count);
        if (gameObject.GetComponent<InventoryItemHandler>().ingredientTypes.Count == 1)
        {
            foreach (Transform child in targetPanelInventorySlots.transform)
            {

                //check if the slot has an item
                if (child.transform.childCount == 1)
                {
                    // check if the item in the slot has exactly 1 ingredient
                    if (child.transform.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().ingredientTypes.Count == 1)
                    {
                        Debug.Log("check if the item in the target slot has exactly 1 ingredient: " + (child.transform.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().ingredientTypes.Count == 1));
                        //check if the ingredients match
                        if (child.transform.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().ingredientTypes[0] == gameObject.GetComponent<InventoryItemHandler>().ingredientTypes[0])
                        {
                            //check that the item in slot is not the exact same item we want to reposition
                            if (child.transform.GetChild(0) != gameObject.transform)
                            {

                                targetItemSlot = child.gameObject.gameObject.GetComponent<ItemSlotHandler>();

                                Debug.Log("item slot applicable for merge found" + targetItemSlot);

                                break;
                            }
                        }
                    }
                }
            }
        }

        // step 3: move item into target slot
        if (targetItemSlot != null)
        {
            Debug.Log("target Itemslot for instant transfer set");
            Debug.Log(targetItemSlot);
            /*var colors = GetComponent<Button>().colors;
            colors.normalColor = Color.red;
            GetComponent<Button>().colors = colors; */

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
            //Debug.Log(ingredientTypes.Count);
        }
    }

    public void UpdateTemperature()
    {
        float temperatureChange;
        float targetTemperature;

        //Debug.Log(transform.parent.gameObject.name);

        // adjust target temperature

        if (
            (transform.parent.GetComponent<ItemSlotHandler>() == null)
            ||(transform.parent.GetComponent<ItemSlotHandler>().associatedContainer == null)
            || (transform.parent.GetComponent<ItemSlotHandler>().associatedContainer.name == "PanelInventory")
            )
        {
            targetTemperature = GameObject.Find("PlayerCharacter").GetComponent<PlayerCharacter>().currentRoom.GetComponent<RoomHandler>().temperature;
            //Debug.Log(targetTemperature);
        }
        else
        {
            targetTemperature = transform.parent.GetComponent<ItemSlotHandler>().associatedContainer.GetComponent<AlchemyStorageContainer>().temperature;
            //Debug.Log(targetTemperature);
        }
        
        //targetTemperature = 30; 


        temperatureChange = (targetTemperature - temperature) * 0.1f;
        temperature += temperatureChange;

        // minimal unterschiedliche temperaturen gleichsetzen
        if (((targetTemperature - temperature) < 1) && ((targetTemperature - temperature) > -1))
        {
            temperature = targetTemperature;
        }



       /* // zu niedrige temperatur auf raumtemperatur setzen
        if (GetComponent<AlchemyContainer>().temperature <= fire.GetComponent<CauldronFire>().room.GetComponent<RoomHandler>().temperature)
        {
            GetComponent<AlchemyContainer>().temperature = fire.GetComponent<CauldronFire>().room.GetComponent<RoomHandler>().temperature;
        }

        // zu hohe temperatur auf max temperatur setzen

        if (GetComponent<AlchemyContainer>().temperature > fire.GetComponent<CauldronFire>().maxTemperature)
            GetComponent<AlchemyContainer>().temperature = fire.GetComponent<CauldronFire>().maxTemperature;
        */ 



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
                //Debug.Log("REMOVED EMPTY INGREDIENT");
            }
        }
    }

    public void AddIngredientAndUpdate(IngredientType ingredientType, int amount)
    {
        //Debug.Log("AddIngredientAndUpdate");
        AddIngredient(ingredientType, amount);
        UpdateItemContent();

    }

    public void AddIngredient(IngredientType ingredientType, int amount)
    {
        //Debug.Log("Add ingredient without temperature");
        AddIngredient(ingredientType, amount, temperature);
    }

    public void AddIngredient(IngredientType ingredientType, int amount, float addedIngredientTemperature)
    {
        //Debug.Log("Add ingredient with temperature: " + addedIngredientTemperature);
        //Debug.Log("Adding ingredient");
        //Debug.Log(ingredientType);
        //Debug.Log(amount);

        ingredientTypes.Add(ingredientType);
        ingredientTypeAmounts.Add(amount);

        //Debug.Log("Temperature Before " + temperature);
        //Debug.Log("amountTotal " + amountTotal);
        //Debug.Log("amount " + amount);
        //Debug.Log("addedIngredientTemperature  " + addedIngredientTemperature);
        temperature = ((float)amountTotal / ((float)amountTotal + (float)amount)) * temperature + ((float)amount / ((float)amountTotal + (float)amount)) * addedIngredientTemperature;
        //Debug.Log("Temperature After " + temperature);
        //Debug.Log(((float)10 / ((float)10 + (float)10)) * 50 + ((float)10 / ((float)10 + (float)10)) * 100);
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

}
