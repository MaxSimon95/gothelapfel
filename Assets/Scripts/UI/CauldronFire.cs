using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CauldronFire : MonoBehaviour
{
    public float burnDownAmount;
    public float burnDownFrequency;
    public float addFireAmount;
    public float currentFireLevel;
    private Image contentImage;
    public float currentTemperature;
    public float maxTemperature;
    //private public GameObject room;
    public GameObject container;
    // Start is called before the first frame update
    IEnumerator Start()
    {

        foreach (Transform child in transform)
        {

            if (child.gameObject.name == "Content")
            {
                contentImage = child.gameObject.GetComponent<Image>();
            }
        }

        while (true)
        {

            UpdateFireVisuals();

            if (currentFireLevel > 0)
            {
                currentFireLevel -= burnDownAmount;
                currentTemperature = maxTemperature * currentFireLevel;

                
            }
            else
            {
                currentFireLevel = 0;
               

            }

            if(container.GetComponent<AlchemyContainer>() != null)
            {
                if (currentTemperature < container.GetComponent<AlchemyContainer>().room.GetComponent<RoomHandler>().temperature)
                    currentTemperature = container.GetComponent<AlchemyContainer>().room.GetComponent<RoomHandler>().temperature;
            }

            if (container.GetComponent<AlchemyStorageContainer>() != null)
            {
                if (currentTemperature < container.GetComponent<AlchemyStorageContainer>().room.GetComponent<RoomHandler>().temperature)
                    currentTemperature = container.GetComponent<AlchemyStorageContainer>().room.GetComponent<RoomHandler>().temperature;
            }





            yield return new WaitForSeconds(burnDownFrequency);
        }
    }

    public void IncreaseFire()
    {
        
            currentFireLevel += addFireAmount;

        if (currentFireLevel > 1)
            currentFireLevel = 1;

        UpdateFireVisuals();
    }

    private void UpdateFireVisuals()
    {
        contentImage.fillAmount = currentFireLevel;
    }

 
}
