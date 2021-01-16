using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientType : MonoBehaviour
{

    public string ingredientName;
    public Color color;
    public Sprite inventorySprite;
    public AudioClip ingredientSound;
    public bool burnsToAsh;

    public float meltingTemperature;
    public float boilingTemperature;

    // 0 = no change, +100 = lethal dosis; -100 = enough to reverse lethal dosis
    public int acidity;  
    public int healthBrain;
    public int healthSkin;
    public int healthHeart;
    public int healthRespiration;
    public int healthStomach;
    public int healthInfection;
    public int healthEyes;
    public int healthSanity;

    public float durationTempEffects; // duration in hours

    // -100 removes effect completely, 0 doesn't change effect, +100 increases to maximum
    public int tempHallucinations;
    public int tempDizzyness;
    public int tempRelaxation;
    public int tempSensitivity;

    // -100 to +100, 0 doesn't change.
    public int tempChattiness;
    public int tempIntelligence;
    public int tempStrength;
    public int tempNervosity;
    public int tempAgreeableness;
    public int tempLust;




    // 0 = tasteless, -100 = YUCK, +100 tasty
    public int taste;

    // 0 = nothing, 100 = daylight
    public int luminosity;

    



    


  
    void Start()
    {
    }

    void Update()
    {
        
    }
}
