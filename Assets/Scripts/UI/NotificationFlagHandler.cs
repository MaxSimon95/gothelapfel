using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationFlagHandler : MonoBehaviour
{
    public List<Notification> notificationsInQueue = new List<Notification>();
    private bool notificationActive = false;

    public AudioClip sound;
    private AudioSource source;
    private float displayTime = 3.2f;
    public GameObject UI_textTitle;
    public GameObject UI_textBody;
    public GameObject UI_image;
    public GameObject UI_textButton;
    public GameObject UI_panel;
    public GameObject UI_buttonLink;
    public GameObject UI_buttonClose;
    public Sprite jobSprite;

    Notification currentNotification;

    // Start is called before the first frame update
    void Start()
    {
        PrepareUIElements();
        //AddNotificationToQueue(new Notification());

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void PrepareUIElements()
    {
        LeanTween.alpha(UI_panel.GetComponent<RectTransform>(), 0f, 0f);
    }

    public void AddNotificationToQueue(Notification notificationObject)
    {

        /*  int id = LeanTween.moveX(gameObject, 1f, 3f).id;
if(LeanTween.isTweening( id ))
     Debug.Log("I am tweening!"); */

        // Add to List
        notificationsInQueue.Add(notificationObject);

        if (!notificationActive)
        {
            ShowNotification();
        }

        // If no Notifcation active, get going (recursive as long as there is a follow up notification)

    }

    public void ShowNotification()
    {

        currentNotification = notificationsInQueue[0];
        notificationActive = true;

        // vorderstes Queue element rausnehmen

        notificationsInQueue.RemoveAt(0);

        // UI Elemente befüllen

        UI_textTitle.gameObject.GetComponent<UnityEngine.UI.Text>().text = currentNotification.textTitle;
        UI_textBody.gameObject.GetComponent<UnityEngine.UI.Text>().text = currentNotification.textContent;
        UI_textButton.gameObject.GetComponent<UnityEngine.UI.Text>().text = currentNotification.textButton;
        UI_image.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = currentNotification.notificationImage;



        // einblenden

        LeanTween.alpha(UI_panel.GetComponent<RectTransform>(), 1f, 0f);

        // X Sekunden (displayTime) warten und ausblenden
        LeanTween.alpha(UI_panel.GetComponent<RectTransform>(), 0f, 1f).setDelay(displayTime).setOnComplete(AfterFadeOutNotification);

        // wenn queue nicht leer, nächstes show starten

      
    }

    public void AfterFadeOutNotification()
    {
        notificationActive = false;

        // if queue empty, notification active = false

        // if queue not empty, shownotification for next

        if (notificationsInQueue.Count > 0)
        {
            ShowNotification();
        }
        

    }

    public void OnClickButtonLink()
    {

        if (RenderOrderAdjustment.anyOverlayOpen)
        {
            CanvasContainerHandler.currentCanvasContainer.CloseContainerView();
        }

        switch (currentNotification.notificationType)
        {
            case Notification.NotificationType.ALCHEMYREACTION:

                GameObject.Find("CanvasNotebookRecipeDetails").GetComponent<NotebookRecipeDetails>().Open(currentNotification.alchemyReaction);
                break;

            case Notification.NotificationType.INGREDIENT:
                GameObject.Find("CanvasNotebookIngredientDetails").GetComponent<NotebookIngredientDetails>().Open(currentNotification.ingredientType);
                break;

            case Notification.NotificationType.JOB:
                Debug.Log("Open JobDetails from Notification");
                GameObject.Find("CanvasNotebookJobDetails").GetComponent<JobDetails>().Open(currentNotification.job);
                //GameObject.Find("CanvasNotebookJobDetails").GetComponent<JobDetails>().Open(JobsManagement.activeJobList[1]);
                break;
        }
    }

    public void OnClickButtonClose()
    {
        LeanTween.alpha(UI_panel.GetComponent<RectTransform>(), 0f, 0f);
        
        notificationActive = false;

        if (notificationsInQueue.Count > 0)
        {
            ShowNotification();
        }
    }

    public void HideNotification()
    {
        // (durch x button, direkt ausblenden)

        // if queue empty, notification active = false

        // if queue not empty, shownotification for next
    }

}
