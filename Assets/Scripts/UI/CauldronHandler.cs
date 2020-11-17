using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CauldronHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
}
