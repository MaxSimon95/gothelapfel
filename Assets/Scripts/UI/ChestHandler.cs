using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHandler : MonoBehaviour
{
    private float updateTemperatureWaitTime = 0.5f;
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

        targetTemperature = GetComponent<AlchemyStorageContainer>().room.temperature;

        temperatureChange = (targetTemperature - GetComponent<AlchemyStorageContainer>().temperature) * 0.01f;
        GetComponent<AlchemyStorageContainer>().temperature += temperatureChange;

        // zu niedrige temperatur auf raumtemperatur setzen
        /*if (GetComponent<AlchemyContainer>().temperature <= fire.GetComponent<CauldronFire>().room.GetComponent<RoomHandler>().temperature)
        {
            GetComponent<AlchemyContainer>().temperature = fire.GetComponent<CauldronFire>().room.GetComponent<RoomHandler>().temperature;
        }*/


        //UITemperatureIndicator.GetComponent<UnityEngine.UI.Text>().text = "Temperature: " + (int)GetComponent<AlchemyContainer>().temperature + "°";




    }
}
