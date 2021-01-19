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
        REWARD
    }

    public Transform transformJobList;
    public static List<JobHandler> activeJobList = new List<JobHandler>();
    public static List<JobHandler> upcomingJobList = new List<JobHandler>();
    public static List<JobHandler> doneJobsList = new List<JobHandler>();



    UnityEvent eventDayChange;



    // Start is called before the first frame update
    void Start()
    {
        eventDayChange = GameTime.eventDayChange;
        eventDayChange.AddListener(ReceivedDayChangeEvent);






        UpdateJobs(false);
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ReceivedDayChangeEvent()
    {
        UpdateJobs(true);
       
    }

    public void UpdateJobs(bool dayHasChanged)
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

                if (job.remainingDays <= 1)
                {
                    job.Expire();
                }
                //Debug.Log(job.title + " " + job.remainingDays + " State: " + job.currentState);

            }
        }

        OrderActiveJobs(sortingType.MARKED);
        
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

        }
        panelFeaturedJobsInner.GetComponent<FeaturedJobs>().UpdateFeaturedJobs();


        /*
       for (int i=0; i< activeJobList.Count; i++)
        {
            Debug.Log(activeJobList[i]);
            Debug.Log(activeJobList[i].orderNumber);
            Debug.Log("_______________________");

        } */




    }


}