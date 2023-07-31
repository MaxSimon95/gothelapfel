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

    public string description;
    public Sprite image;
    public enum rarity
    {
        NONE,  
        VERY_RARE, 
        RARE, 
        MEDIUM, 
        COMMON, 
        ABUNDAND 
    }

    public List<IngredientType> ingredientTypes;

   /* public List<int> amountsMinSpring;
    public List<int> amountsMinSummer;
    public List<int> amountsMinAutumn;
    public List<int> amountsMinWinter;

    public List<int> amountsMaxSpring;
    public List<int> amountsMaxSummer;
    public List<int> amountsMaxAutumn;
    public List<int> amountsMaxWinter; */

    public List<rarity> raritiesSpring;
    public List<rarity> raritiesSummer;
    public List<rarity> raritiesAutumn;
    public List<rarity> raritiesWinter;
    /*
    public List<int> specificSearchChancesSpring;
    public List<int> specificSearchChancesSummer;
    public List<int> specificSearchChancesAutumn;
    public List<int> specificSearchChancesWinter;
    */ 
   
    public static string RarityToString(rarity pRarity)
    {
        switch(pRarity)
        {
            case rarity.NONE:
                return ("Not Available");
                break;

            case rarity.VERY_RARE:
                return ("Very Rare");
                break;

            case rarity.RARE:
                return ("Rare");
                break;

            case rarity.MEDIUM:
                return ("Medium");
                break;

            case rarity.COMMON:
                return ("Common");
                break;

            case rarity.ABUNDAND:
                return ("Abundand");
                break;

            default:
                return ("ERROR");
                break;

        }
    }

    public static float RarityToFloat(rarity pRarity)
    {
        switch (pRarity)
        {
            case rarity.NONE:
                return (0f);
                break;

            case rarity.VERY_RARE:
                return (0.08f);
                break;

            case rarity.RARE:
                return (0.16f);
                break;

            case rarity.MEDIUM:
                return (0.32f);
                break;

            case rarity.COMMON:
                return (0.64f);
                break;

            case rarity.ABUNDAND:
                return (1f);
                break;

            default:
                Debug.Log("Error, invalid Rarity (public static float RarityToFloat): " + pRarity);
                return (0f);
                break;

        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
