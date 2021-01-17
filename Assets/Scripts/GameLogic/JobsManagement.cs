using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class JobsManagement : MonoBehaviour
{

    public Transform panelFeaturedJobsInner;
    //public Transform panelFeaturedJobsOuter;
    


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
        //Debug.Log("JOBSMANAGEMENT GOT THE EVENT");
    }

    public void UpdateJobs(bool dayHasChanged)
    {

        activeJobList = new List<JobHandler>();

        foreach (Transform jobTransform in transformJobList)
        {
            if (jobTransform.GetComponent<JobHandler>().currentState == JobHandler.state.ACTIVE)
                activeJobList.Add(jobTransform.gameObject.GetComponent<JobHandler>());
        }

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

        OrderActiveJobs();
    }

    public void OrderActiveJobs()
    {



        activeJobList = activeJobList.OrderBy(e => e.orderNumber).ToList();
        /*
       for (int i=0; i< activeJobList.Count; i++)
        {
            Debug.Log(activeJobList[i]);
            Debug.Log(activeJobList[i].orderNumber);
            Debug.Log("_______________________");

        } */

        panelFeaturedJobsInner.GetComponent<FeaturedJobs>().UpdateFeaturedJobs();


    }


}