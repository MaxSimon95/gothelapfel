using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientEffect : MonoBehaviour
{
    public string effectName;
    public Sprite sprite;

    public string stringLowerM100;
    public string stringM100ToM50;
    public string stringM50To0;
    public string string0;
    public string string0To50;
    public string string50To100;
    public string stringHigher100;

    public enum EffectIntensity
    {
        EXTREME_NEGATIVE = -100,
        STRONG_NEGATIVE = -50,
        SLIGHT_NEGATIVE = -1,
        NEUTRAL = 0,
        SLIGHT_POSITIVE = 1,
        STRONG_POSITIVE = 50,
        EXTREME_POSITIVE = 100
    }
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public string GetEffectDescriptionString(int value)
    {
        string tempString;

        if (value <= -100)
            tempString = stringLowerM100;
        else
                 if (value <= -50)
            tempString = stringM100ToM50;
        else
                 if (value < 0)
            tempString = stringM50To0;
        else
                 if (value == 0)
            tempString = string0;
        else
                 if (value < 50)
            tempString = string0To50;
        else
                 if (value < 100)
            tempString = string50To100;
        else
            tempString = stringHigher100;
        

        return tempString;
    }
    
    

}
