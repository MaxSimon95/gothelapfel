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
    public GameObject room;
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
            if (currentTemperature < room.GetComponent<RoomHandler>().temperature)
                currentTemperature = room.GetComponent<RoomHandler>().temperature;

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
