using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenHandler : MonoBehaviour
{
    private float updateTemperatureWaitTime = 0.5f;
    public GameObject fire;
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

    public void UpdateTemperature()
    {
        float temperatureChange;
        float targetTemperature;

        targetTemperature = fire.GetComponent<CauldronFire>().currentFireLevel * fire.GetComponent<CauldronFire>().maxTemperature + GetComponent<AlchemyStorageContainer>().room.GetComponent<RoomHandler>().temperature;

        // feuer ist an
        if (fire.GetComponent<CauldronFire>().currentFireLevel > 0)
        {
            targetTemperature = fire.GetComponent<CauldronFire>().currentFireLevel * fire.GetComponent<CauldronFire>().maxTemperature;



            //if (temperatureChange < 0) temperatureChange = 0;





        }

        // feuer ist aus
        else
        {
            //temperatureChange = fire.GetComponent<CauldronFire>().maxTemperature * 0.03f;
            //GetComponent<AlchemyContainer>().temperature -= temperatureChange;
        }

        temperatureChange = (targetTemperature - GetComponent<AlchemyStorageContainer>().temperature) * 0.01f;
        GetComponent<AlchemyStorageContainer>().temperature += temperatureChange;

        // zu niedrige temperatur auf raumtemperatur setzen  (obsolet, da schon das feuer nicht unter raumtemperatur fallen kann?)
        /*  if (GetComponent<AlchemyContainer>().temperature <= fire.GetComponent<CauldronFire>().room.GetComponent<RoomHandler>().temperature)
          {
              GetComponent<AlchemyContainer>().temperature = fire.GetComponent<CauldronFire>().room.GetComponent<RoomHandler>().temperature;
          } */

        // zu hohe temperatur auf max temperatur setzen

        if (GetComponent<AlchemyStorageContainer>().temperature > fire.GetComponent<CauldronFire>().maxTemperature)
            GetComponent<AlchemyStorageContainer>().temperature = fire.GetComponent<CauldronFire>().maxTemperature;

        UITemperatureIndicator.GetComponent<UnityEngine.UI.Text>().text = "Temperature: " + (int)GetComponent<AlchemyStorageContainer>().temperature + "°";




    }
}
