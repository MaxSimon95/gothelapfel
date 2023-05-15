using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class JobsManagement : MonoBehaviour
{

    public Transform panelFeaturedJobsInner;
    //public Transform panelFeaturedJobsOuter;
    
    public enum sortingType
    {
        MARKED,
        TITLE,
        TIMELEFT,
        REWARD,
        AGE // the seconinho-time when the job has turned active
    }

    public Transform transformJobList;
    public static List<JobHandler> activeJobList = new List<JobHandler>();
    public static List<JobHandler> upcomingJobList = new List<JobHandler>();
    public static List<JobHandler> doneJobsList = new List<JobHandler>();

    UnityEvent eventDayChange;
    //public NotificationFlagHandler notificationFlagHandler;

    // Start is called before the first frame update
    void Start()
    {
        eventDayChange = GameTime.eventDayChange;
        eventDayChange.AddListener(ReceivedDayChangeEvent);






        UpdateActiveJobs(false);
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddJob(string pTitle, int pPayment, IngredientType pRequestedIngredientType, int pRequestedAmount, int pStartDays, string pDescription, NPC pClient)
    {
        GameObject tempJobGO = (GameObject)Instantiate(Resources.Load("JobItemPrefab"), new Vector3(0, 0, 0), Quaternion.identity);
        JobHandler jobItem = tempJobGO.GetComponent<JobHandler>();
        jobItem.transform.SetParent(this.transform.GetChild(0));

        jobItem.title = pTitle;
        jobItem.payment = pPayment;
        jobItem.requestedIngredientType = pRequestedIngredientType;
        jobItem.requestedAmount = pRequestedAmount;
        jobItem.startDays = pStartDays;
        jobItem.description = pDescription;
        jobItem.client = pClient;

        jobItem.Activate();
        //notificationFlagHandler.AddNotificationToQueue(new Notification(jobItem));
    }

    public void AddJob(string pTitle, int pPayment, List<IngredientEffect> pRequestedEffects, List<IngredientEffect.EffectIntensity> pRequestedEffectIntensities, List<JobHandler.ExpectedEffectIntensityOperator> pRequestedEffectIntensityOperators, int pRequestedAmount, bool pIgnoreSideffectsForItemSuitableChecks, int pStartDays, string pDescription, NPC pClient)
    {
        GameObject tempJobGO = (GameObject)Instantiate(Resources.Load("JobItemPrefab"), new Vector3(0, 0, 0), Quaternion.identity);
        JobHandler jobItem = tempJobGO.GetComponent<JobHandler>();
        jobItem.transform.SetParent(this.transform.GetChild(0));

        jobItem.title = pTitle;
        jobItem.payment = pPayment;
        jobItem.requestedEffects = pRequestedEffects;
        jobItem.requestedEffectIntensities = pRequestedEffectIntensities;
        jobItem.requestedEffectIntensityOperators = pRequestedEffectIntensityOperators;
        jobItem.requestedAmount = pRequestedAmount;
        jobItem.ignoreSideffectsForItemSuitableChecks = pIgnoreSideffectsForItemSuitableChecks;
        jobItem.startDays = pStartDays;
        jobItem.description = pDescription;
        jobItem.client = pClient;

        jobItem.Activate();
        //notificationFlagHandler.AddNotificationToQueue(new Notification(jobItem));
    }

    private void ReceivedDayChangeEvent()
    {
        UpdateActiveJobs(true);
       
    }

    public void UpdateActiveJobs(bool dayHasChanged)
    {

        activeJobList = new List<JobHandler>();

        foreach (Transform jobTransform in transformJobList)
        {
            //Debug.Log(jobTransform.GetComponent<JobHandler>().currentState);
            if (jobTransform.GetComponent<JobHandler>().currentState == JobHandler.state.ACTIVE)
                activeJobList.Add(jobTransform.gameObject.GetComponent<JobHandler>());
        }

        //Debug.Log(activeJobList.Count);

        if (dayHasChanged)
        {
            foreach (JobHandler job in activeJobList)
            {
                job.remainingDays--;

                if (job.remainingDays < 1)
                {
                    job.Expire();
                    UpdateActiveJobs(false);
                }
                //Debug.Log(job.title + " " + job.remainingDays + " State: " + job.currentState);

            }
        }

        if (!NotebookBaseUI.notebookIsOpen)
        {
            OrderActiveJobs(sortingType.MARKED);
            panelFeaturedJobsInner.GetComponent<FeaturedJobs>().UpdateFeaturedJobs();
        }
            
    }

    public void OrderActiveJobs(sortingType sorting)
    {
        switch(sorting)
        {
            case sortingType.MARKED:
                activeJobList = activeJobList.OrderBy(e => e.orderNumber).ToList();
                break;

            case sortingType.TIMELEFT:
                activeJobList = activeJobList.OrderBy(e => e.remainingDays).ToList();
                break;

            case sortingType.REWARD:
                activeJobList = activeJobList.OrderByDescending(e => e.payment).ToList();
                break;

            case sortingType.TITLE:
                activeJobList = activeJobList.OrderBy(e => e.title).ToList();
                break;

            case sortingType.AGE:
                activeJobList = activeJobList.OrderBy(e => e.startPointInTime).ToList();
                break;

        }
        


        /*
       for (int i=0; i< activeJobList.Count; i++)
        {
            Debug.Log(activeJobList[i]);
            Debug.Log(activeJobList[i].orderNumber);
            Debug.Log("_______________________");

        } */




    }

    public static void CompleteJob(JobHandler job, InventoryItemHandler inventoryItem)
    {
        Debug.Log("job completed: " + job.title);
        Debug.Log(inventoryItem);
    }
}