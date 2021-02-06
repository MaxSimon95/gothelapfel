using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientType : MonoBehaviour
{

    public bool knownToPlayer;

    public string ingredientName;
    public Color color;
    public Sprite inventorySprite;
    public AudioClip ingredientSound;
    public bool burnsToAsh;

    public float meltingTemperature;
    public float boilingTemperature;

    /*// labels: <=-100 = lethal poison (____) , -99 to -50 strong poison, -49 to 0 weak poison (___), 0 to 49 light medicine (___), 50 to 99 potent medicine (___) , 100+ extremely potent medicine (___)

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
    public int tempDizziness;   


    public int tempRelaxation;  // opposite: tension
    public int tempSensitivity; // opposite: numbness
    public int tempTiredness;

    // 1 to 50 slightly amplify/reduce ___, 51 to 100 greatly amplify/reduce


    // -100 to +100, 0 doesn't change.
    public int tempChattiness;
    public int tempIntelligence;  
    public int tempStrength; 
    public int tempAgreeableness;
    public int tempLust;
    public int tempFear; // opposite: Bravery
    public int tempHappiness;

    // labels = <=-100 to -50 disgusting ___ , -50 to 0 bad ___ , 0 ___less, 0 to 50 good ____ , 50 to 100 amazing ____
    // 0 = tasteless, -100 = YUCK, +100 tasty
    public int taste;
    public int odor;

    // 0 = nothing, 100 = daylight
    public int luminosity; */

    public List<IngredientEffect> effects = new List<IngredientEffect>();
    public List<int> effectIntensities = new List<int>();






    void Start()
    {
       
    }

    void Update()
    {
        
    }
}
