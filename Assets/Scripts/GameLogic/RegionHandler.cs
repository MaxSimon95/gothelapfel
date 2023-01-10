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

    public List<int> randomSearchChanceSpring;
    public List<int> randomSearchChanceSummer;
    public List<int> randomSearchChanceAutumn;
    public List<int> randomSearchChanceWinter;

    public List<int> specificSearchChanceSpring;
    public List<int> specificSearchChanceSummer;
    public List<int> specificSearchChanceAutumn;
    public List<int> specificSearchChanceWinter;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
