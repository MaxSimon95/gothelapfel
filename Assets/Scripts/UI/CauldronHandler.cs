using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CauldronHandler : MonoBehaviour
{
    public GameObject fire;
    private float updateTemperatureWaitTime = 0.5f;
    public GameObject UITemperatureIndicator;

    // Start is called before the first frame update
    IEnumerator Start()
    {

        while (true)
        {
            UpdateTemperature();

            yield return new WaitForSeconds(updateTemperatureWaitTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        UpdateContentColor();
        UpdateContentLevel();
    }

    public void UpdateContentColor()
    {
        if(GetComponent<AlchemyContainer>().amountTotal>0)
        {
            // determine color
            float r = 0f;
            float g = 0f;
            float b = 0f;
            float a = 0f;
            

            for(int index = 0; index < GetComponent<AlchemyContainer>().ingredientTypes.Count; index++)
            {
                r += GetComponent<AlchemyContainer>().ingredientTypes[index].color.r * GetComponent<AlchemyContainer>().ingredientTypeAmounts[index];
                g += GetComponent<AlchemyContainer>().ingredientTypes[index].color.g * GetComponent<AlchemyContainer>().ingredientTypeAmounts[index];
                b += GetComponent<AlchemyContainer>().ingredientTypes[index].color.b * GetComponent<AlchemyContainer>().ingredientTypeAmounts[index];
                a += GetComponent<AlchemyContainer>().ingredientTypes[index].color.a * GetComponent<AlchemyContainer>().ingredientTypeAmounts[index];
            }

   

            r = r / GetComponent<AlchemyContainer>().amountTotal;
            g = g / GetComponent<AlchemyContainer>().amountTotal;
            b = b / GetComponent<AlchemyContainer>().amountTotal;
            a = a / GetComponent<AlchemyContainer>().amountTotal;


            foreach (Transform child in transform)
            {

                if (child.gameObject.name == "Content")
                {
  
                    child.gameObject.GetComponent<Image>().color = new Color(r, g, b, a);
                }
            }
        }
        

    }

    public void UpdateContentLevel()
    {
        if (GetComponent<AlchemyContainer>().amountTotal <= 0)
        {
            foreach (Transform child in transform)
            {

                if (child.gameObject.name == "Content")
                {
                    child.gameObject.GetComponent<Image>().fillAmount = 0;
                }
            }
        }
        else
        {
            foreach (Transform child in transform)
            {

                if (child.gameObject.name == "Content")
                {
                   
                    child.gameObject.GetComponent<Image>().fillAmount = (float)GetComponent<AlchemyContainer>().amountTotal / (float)GetComponent<AlchemyContainer>().capacity;
                }
            }
        }
    }

    public void UpdateTemperature()
    {
        float temperatureChange;
        float targetTemperature;

        targetTemperature = fire.GetComponent<CauldronFire>().currentFireLevel * fire.GetComponent<CauldronFire>().maxTemperature + GetComponent<AlchemyContainer>().room.GetComponent<RoomHandler>().temperature;

        // feuer ist an
        if (fire.GetComponent<CauldronFire>().currentFireLevel > 0)
        {
            targetTemperature = fire.GetComponent<CauldronFire>().currentFireLevel * fire.GetComponent<CauldronFire>().maxTemperature;

        }

        // feuer ist aus
        else
        {
            //temperatureChange = fire.GetComponent<CauldronFire>().maxTemperature * 0.03f;
            //GetComponent<AlchemyContainer>().temperature -= temperatureChange;
        }

        temperatureChange = (targetTemperature - GetComponent<AlchemyContainer>().temperature) * 0.01f;
        GetComponent<AlchemyContainer>().temperature += temperatureChange;

        // zu niedrige temperatur auf raumtemperatur setzen  (obsolet, da schon das feuer nicht unter raumtemperatur fallen kann?)
      /*  if (GetComponent<AlchemyContainer>().temperature <= fire.GetComponent<CauldronFire>().room.GetComponent<RoomHandler>().temperature)
        {
            GetComponent<AlchemyContainer>().temperature = fire.GetComponent<CauldronFire>().room.GetComponent<RoomHandler>().temperature;
        } */ 

        // zu hohe temperatur auf max temperatur setzen

        if (GetComponent<AlchemyContainer>().temperature > fire.GetComponent<CauldronFire>().maxTemperature)
            GetComponent<AlchemyContainer>().temperature = fire.GetComponent<CauldronFire>().maxTemperature;

        UITemperatureIndicator.GetComponent<UnityEngine.UI.Text>().text = "Temperature: " + (int)GetComponent<AlchemyContainer>().temperature + "°";

       


    }
}
