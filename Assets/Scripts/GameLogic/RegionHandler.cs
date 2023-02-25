using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public string regionName;
    public float distanceToHome;
    public float travelTimeToHome;
    public bool currentlyAccessible;
    public enum rarity
    {
        VERY_RARE,
        RARE,
        MEDIUM,
        COMMON,
        ABUNDAND
    }

    public List<IngredientType> ingredientTypesSpring;
    public List<IngredientType> ingredientTypesSummer;
    public List<IngredientType> ingredientTypesAutumn;
    public List<IngredientType> ingredientTypesWinter;

    public List<int> amountsMinSpring;
    public List<int> amountsMinSummer;
    public List<int> amountsMinAutumn;
    public List<int> amountsMinWinter;

    public List<int> amountsMaxSpring;
    public List<int> amountsMaxSummer;
    public List<int> amountsMaxAutumn;
    public List<int> amountsMaxWinter;

    public List<rarity> randomSearchChancesSpring;
    public List<rarity> randomSearchChancesSummer;
    public List<rarity> randomSearchChancesAutumn;
    public List<rarity> randomSearchChancesWinter;

    public List<int> specificSearchChancesSpring;
    public List<int> specificSearchChancesSummer;
    public List<int> specificSearchChancesAutumn;
    public List<int> specificSearchChancesWinter;

   



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
