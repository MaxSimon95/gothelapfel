using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AlchemyContainer : MonoBehaviour
{

    public List<IngredientType> ingredientTypes = new List<IngredientType>();
    public List<int> ingredientTypeAmounts = new List<int>();
    public float temperature;
    public int capacity;
    public int amountTotal;
    public AudioClip sound;
    private AudioSource source;
    private float updateWaitTime;



    // Start is called before the first frame update
    IEnumerator Start()
    {
        updateWaitTime = Random.Range(1.0f, 2.0f);
        //Debug.Log(updateWaitTime);

        while (true)
        {


            UpdateContent();

            yield return new WaitForSeconds(updateWaitTime);
            //Debug.Log("NAaNI");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddIngredient(IngredientType ingredientType, int amount)
    {

        ingredientTypes.Add(ingredientType);
        ingredientTypeAmounts.Add(amount);

        

        
        UpdateContent();
    }

    private void DeleteIngredientIfEmpty()
    {
        for (int index = ingredientTypeAmounts.Count - 1; index >= 0; index--)
        {
            if (ingredientTypeAmounts[index] <= 0)
            {
                ingredientTypeAmounts.RemoveAt(index);
                ingredientTypes.RemoveAt(index);
            }
        }
    }

    public void UpdateContent()
    {
        MergeIdenticalIngredients();
        GameObject.Find("AlchemyEngine").GetComponent<AlchemyEngineLogic>().CheckForFittingAlchemyReaction(ingredientTypes, ingredientTypeAmounts, temperature, gameObject.name);
        MergeIdenticalIngredients();
        DeleteIngredientIfEmpty();
        amountTotal = ingredientTypeAmounts.Sum();

        if(GetComponent<CauldronHandler>()!=null)
        {
            GetComponent<CauldronHandler>().UpdateUI();
        }

    }

    private void MergeIdenticalIngredients()
    {
        List<IngredientType> newIngredientTypeList = new List<IngredientType>(); 
        List<int> newIngredientTypeAmountsList = new List<int>(); 

        for (int index = 0; index < ingredientTypes.Count; index++)
        {
            IngredientType tempIngredientType = ingredientTypes[index];

            // if it isnt in new list yet, then start putting all ingredients of this type into the list (otherwise this has already been done, no need for further action)
            if (!newIngredientTypeList.Contains(tempIngredientType))
            {
                int amountTemp = 0;

                // increment for each time it occurs
                for (int index2 = 0; index2 < ingredientTypes.Count; index2++)
                {
                    
                    if(ingredientTypes[index2] == tempIngredientType)
                    {
                        amountTemp += ingredientTypeAmounts[index2];
                    }
                }

                // add into new list at end end
                newIngredientTypeList.Add(tempIngredientType);
                newIngredientTypeAmountsList.Add(amountTemp);
                
            }

        }

    // replace old list with new one
    ingredientTypes = newIngredientTypeList;
    ingredientTypeAmounts = newIngredientTypeAmountsList;

    
    }

    public void PlaySound()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(sound, 1f);
    }
}
