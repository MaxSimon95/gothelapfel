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

    // labels: <=-100 = lethal ___ poison, -99 to -50 strong ___ poison, -49 to 0 weak ___ poison, 0 to 49 light ___ medicine, 50 to 99 ___ potent medicine, 100+ extremely potent medicine

    // 0 = no change, +100 = lethal dosis; -100 = enough to reverse lethal dosis
    public int acidity;  
    public int healthBrain;  //
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
    public int tempRelaxation;  // opposite: tension
    public int tempSensitivity; // opposite: numbness
    public int tempTieredness;

    // 1 to 33 slightly amplify/reduce ___, 34 to 66 moderately amplify/reduce ____, 67 to 100 greatly amplify/reduce

    // -100 to +100, 0 doesn't change.
    public int tempChattiness;
    public int tempIntelligence;  
    public int tempStrength; 
    //public int tempNervosity;
    public int tempAgreeableness;
    public int tempLust;
    public int tempFear; // opposite: Bravery
    



    // labels = <=-100 to -50 disgusting taste, -50 to 0 unappetizing taste, 0 tasteless, 0 to 50 agreeable taste, 50 to 100 delicious taste
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
