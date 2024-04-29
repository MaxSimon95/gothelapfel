using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string forename;
    public string lastname;
    public string fullname;
    public string occupation;
    public Sprite imagePortrait;
    public Sprite imageLarge;
    public bool alive;


    // the health ranges from 0 to 100. When a vital health stat reaches 0, the character dies. When sanity reaches 0, the characters goes permanently insane.
    public float healthBrain;
    public float healthSkin;
    public float healthHeart;
    public float healthRespiration;
    public float healthStomach;
    public float healthInfection;
    public float healthEyes;
    public float healthSanity;

    // temp health effects slowly reset over time.
    public float tempHallucinations;
    public float tempDizziness;
    public float tempRelaxation;
    public float tempSensitivity;
    public float tempTiredness;
    public float tempChattiness;
    public float tempIntelligence;
    public float tempStrength;
    public float tempAgreeableness;
    public float tempOdor;
    public float tempLust;
    public float tempFear;
    public float tempHappiness;



    // Start is called before the first frame update
    void Start()
    {
      
        fullname = forename + " " + lastname;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConsumeInventoryItem(InventoryItemHandler inventoryItem, JobHandler.UseType useType)
    {
        for(int i=0; i< inventoryItem.IngredientEffects.Count; i++)
        {
            IngredientEffect tempEffect = inventoryItem.IngredientEffects[i];
            switch(tempEffect.name)
            {
                case "IE_acidity":
                    if(useType == JobHandler.UseType.CONSUMPTION)
                    {
                        healthRespiration = healthRespiration - inventoryItem.IngredientEffectIntensities[i];
                        healthStomach = healthStomach - inventoryItem.IngredientEffectIntensities[i];
                    }
                    else
                    {
                        healthSkin = healthSkin - inventoryItem.IngredientEffectIntensities[i];
                    }
                    break;

                case "IE_healthBrain":
                    healthBrain = healthBrain + inventoryItem.IngredientEffectIntensities[i];
                    break;

                case "IE_healthSkin":
                    healthSkin = healthSkin + inventoryItem.IngredientEffectIntensities[i];
                    break;

                case "IE_healthHeart":
                    healthHeart = healthHeart + inventoryItem.IngredientEffectIntensities[i];
                    break;

                case "IE_healthRespiration":
                    healthRespiration = healthRespiration + inventoryItem.IngredientEffectIntensities[i];
                    break;

                case "IE_healthStomach":
                    healthStomach = healthStomach + inventoryItem.IngredientEffectIntensities[i];
                    break;

                case "IE_healthInfection":
                    healthInfection = healthInfection + inventoryItem.IngredientEffectIntensities[i];
                    break;

                case "IE_healthEyes":
                    healthEyes = healthEyes + inventoryItem.IngredientEffectIntensities[i];
                    break;

                case "IE_healthSanity":
                    healthSanity = healthSanity + inventoryItem.IngredientEffectIntensities[i];
                    break;

                case "IE_tempHallucinations":
                    tempHallucinations = tempHallucinations + inventoryItem.IngredientEffectIntensities[i];
                    break;

                case "IE_tempDizziness":
                    tempDizziness = tempDizziness + inventoryItem.IngredientEffectIntensities[i];
                    break;

                case "IE_tempRelaxation":
                    tempRelaxation = tempRelaxation + inventoryItem.IngredientEffectIntensities[i];
                    break;

                case "IE_tempSensitivity":
                    tempSensitivity = tempSensitivity + inventoryItem.IngredientEffectIntensities[i];
                    break;

                case "IE_tempTiredness":
                    tempTiredness = tempTiredness + inventoryItem.IngredientEffectIntensities[i];
                    break;

                case "IE_tempChattiness":
                    tempChattiness = tempChattiness + inventoryItem.IngredientEffectIntensities[i];
                    break;

                case "IE_tempIntelligence":
                    tempIntelligence = tempIntelligence + inventoryItem.IngredientEffectIntensities[i];
                    break;

                case "IE_tempStrength":
                    tempStrength = tempStrength + inventoryItem.IngredientEffectIntensities[i];
                    break;

                case "IE_tempAgreeableness":
                    tempAgreeableness = tempAgreeableness + inventoryItem.IngredientEffectIntensities[i];
                    break;

                case "IE_tempLust":
                    tempLust = tempLust + inventoryItem.IngredientEffectIntensities[i];
                    break;

                case "IE_tempFear":
                    tempFear = tempFear + inventoryItem.IngredientEffectIntensities[i];
                    break;

                case "IE_tempHappiness":
                    tempHappiness = tempHappiness + inventoryItem.IngredientEffectIntensities[i];
                    break;

                case "IE_odor":
                    tempOdor = tempLust + inventoryItem.IngredientEffectIntensities[i];
                    break;

            }

            // check if the npc dies or goes insane
            if (healthBrain <= 0) Die();
            if (healthSkin <= 0) Die();
            if (healthHeart <= 0) Die();
            if (healthRespiration <= 0) Die();
            if (healthStomach <= 0) Die();
            if (healthInfection <= 0) Die();
            if (healthSanity <= 0) GoInsane();

        }
    }

    public void Die()
    {
        alive = false;
        Debug.Log(fullname + " has died.");
    }

    public void GoInsane()
    {
        // for the time being
        Die();
    }

    public void UpdateDailyHealth()
    {

    }
}
