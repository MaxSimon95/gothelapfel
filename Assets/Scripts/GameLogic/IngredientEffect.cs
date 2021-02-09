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

    public Color positiveColor;
    public Color negativeColor;
    // using an enum effectinstensity for including the intensity in jobs. it would be instransparent to the player to work with freely chosen ints there

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
    
    public enum IntensityType
    {
        MAXIMUM_IS_HEALTHY,
        ZERO_IS_HEALTHY,
        MINIMUM_IS_HEALTHY,
        IRRELEVANT
    }

    public IntensityType intensityType;


    // Start is called before the first frame update
    void Start()
    {
        SetColors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColors()
    {
        switch(intensityType)
        {
            case IntensityType.MAXIMUM_IS_HEALTHY:

                positiveColor = new Color(0.3384212f, 0.6132076f, 0.3411817f);
                negativeColor = new Color(0.764151f, 0.3280082f, 0.3280082f);
                break;

            case IntensityType.ZERO_IS_HEALTHY:

                positiveColor = new Color(0, 0, 0);
                negativeColor = new Color(0, 0, 0);
                break;

            case IntensityType.IRRELEVANT:

                positiveColor = new Color(0, 0, 0);
                negativeColor = new Color(0, 0, 0);
                break;

            case IntensityType.MINIMUM_IS_HEALTHY:

                positiveColor = new Color(0.764151f, 0.3280082f, 0.3280082f);
                negativeColor = new Color(0.3384212f, 0.6132076f, 0.3411817f);
                break;

        }
    }

    public static EffectIntensity IntensityTypeIntToEnum(float intensityInt)
    {
        // <=-100 = lethal poison (____) , -99 to -50 strong poison, -49 to 0 weak poison (___), 
        //0 to 49 light medicine (___), 50 to 99 potent medicine (___) , 100+ extremely potent medicine (___)

        EffectIntensity tempIntensityType ;

        if (intensityInt <= -100)
            tempIntensityType = EffectIntensity.EXTREME_NEGATIVE;
        else
        {
            if (intensityInt > -100 && intensityInt <= -50)
                tempIntensityType = EffectIntensity.STRONG_NEGATIVE;
            else
            {
                if (intensityInt > -50 && intensityInt < 0)
                    tempIntensityType = EffectIntensity.SLIGHT_NEGATIVE;
                else
                {

                    if (intensityInt == 0)
                        tempIntensityType = EffectIntensity.NEUTRAL;
                    else
                    {
                        if (intensityInt > 0 && intensityInt < 50)
                            tempIntensityType = EffectIntensity.SLIGHT_POSITIVE;
                        else
                        {
                            if (intensityInt >= 50 && intensityInt < 100)
                                tempIntensityType = EffectIntensity.STRONG_POSITIVE;
                            else
                            {
                                tempIntensityType = EffectIntensity.EXTREME_POSITIVE;
                            }
                        }
                    }
                }
            }
        }

        return tempIntensityType;








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
