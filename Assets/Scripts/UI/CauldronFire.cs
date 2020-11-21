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
                currentFireLevel -= burnDownAmount;
            else
                currentFireLevel = 0;

            yield return new WaitForSeconds(burnDownFrequency);
        }
    }

    public void IncreaseFire()
    {
        if (currentFireLevel < 1)
            currentFireLevel += addFireAmount;
        else
            currentFireLevel = 1;

        UpdateFireVisuals();
    }

    private void UpdateFireVisuals()
    {
        contentImage.fillAmount = currentFireLevel;
    }

 
}
