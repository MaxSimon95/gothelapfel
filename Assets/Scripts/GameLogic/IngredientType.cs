using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientType : MonoBehaviour
{

    public bool knownToPlayer;

    public string ingredientName;
    public Color color;
    public Sprite inventorySprite;
    public AudioClip ingredientSound;
    public string description;
    public bool burnsToAsh;

    public float meltingTemperature;
    public float boilingTemperature;


    public List<IngredientEffect> effects = new List<IngredientEffect>();
    public List<int> effectIntensities = new List<int>();






    void Start()
    {
       
    }

    void Update()
    {
        
    }
}
