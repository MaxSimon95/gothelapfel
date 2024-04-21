using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification //: MonoBehaviour
{
    // Start is called before the first frame update
    public enum NotificationType
    {
        ALCHEMYREACTION,
        INGREDIENT,
        JOB,
        SEASON

    }
    public string textTitle;
    public string textContent;
    public string textButton;
    public NotificationType notificationType;
    public Sprite notificationImage;
    public AlchemyReaction alchemyReaction;
    public IngredientType ingredientType;
    public JobHandler job;

    public NotificationFlagHandler notificationFlagHandler;

    void Start()
    {
        notificationFlagHandler = GameObject.Find("PanelNotificationFlag").GetComponent<NotificationFlagHandler>();


    }

    /*  public Notification(GameTime.season pSeason)
      {
          notificationType = NotificationType.JOB;

          switch (pSeason)
          {
              case GameTime.season.SPRING:
                  textTitle = "Spring has come!";
                  textButton = "The days are growing longer, and ";
                  break;
          }
          textTitle = "Spring has come!";
          textButton = "Show Job Details";
          textContent = job.title;
          notificationImage = notificationFlagHandler.jobSprite;
      } */

    public Notification(JobHandler pJob)
    {
        job = pJob;
        notificationType = NotificationType.JOB;
        textTitle = "New Quest Obtained";
        textButton = "Show Details";
        textContent = job.title;
        //notificationImage = notificationFlagHandler.jobSprite;
        notificationImage = pJob.client.imagePortrait;
    }

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

 

    // Update is called once per frame
    void Update()
    {
        
    }
}
