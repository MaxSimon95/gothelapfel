using UnityEngine;
using System.Collections;

namespace Alchemy.Namespace
{

    public class IngredientType : MonoBehaviour
    {
        public string ingredientName;
        public Color color;
        public Sprite inventorySprite;
        public AudioClip ingredientSound;
    }

    public class Ingredient : MonoBehaviour
    {

        public IngredientType ingredientTypeID;
        public int amount;
        public float temperature;

        void Start()
        {

        }
    }



}