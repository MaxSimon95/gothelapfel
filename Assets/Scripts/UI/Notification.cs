using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification //: MonoBehaviour
{
    // Start is called before the first frame update
    public enum NotificationType
    {
        ALCHEMYREACTION,
        INGREDIENT
    }
    public string textTitle;
    public string textContent;
    public string textButton;
    public NotificationType notificationType;
    public Sprite notificationImage;
    public AlchemyReaction alchemyReaction;
    public IngredientType ingredientType;

    public Notification ( AlchemyReaction pAlchemyReaction)
    {
        alchemyReaction = pAlchemyReaction;
        notificationType = NotificationType.ALCHEMYREACTION;
        textTitle = "New Recipe discovered";
        textButton = "Show Recipe";
        textContent = alchemyReaction.outputIngredientTypes[0].ingredientName;
        notificationImage = alchemyReaction.outputIngredientTypes[0].inventorySprite;
    }

    public Notification(IngredientType pIngredientType)
    {
        ingredientType = pIngredientType;
        notificationType = NotificationType.INGREDIENT;
        textTitle = "New Substance discovered";
        textButton = "Show Details";
        textContent = pIngredientType.ingredientName;
        notificationImage = pIngredientType.inventorySprite;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
