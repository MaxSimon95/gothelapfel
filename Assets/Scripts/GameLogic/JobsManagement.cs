using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class JobsManagement : MonoBehaviour
{

    public Transform panelFeaturedJobsInner;
    public Transform panelFeaturedJobsOuter;
    public GameObject featuredJobsToggleButton;
    public bool panelFeaturedJobsOuterVisible = true;

    public Transform transformJobList;
    public static List<JobHandler> activeJobList = new List<JobHandler>();
    public static List<JobHandler> upcomingJobList = new List<JobHandler>();
    public static List<JobHandler> doneJobsList = new List<JobHandler>();

    private List<Transform> featuredJobSlots = new List<Transform>();

    UnityEvent eventDayChange;



    // Start is called before the first frame update
    void Start()
    {
        eventDayChange = GameTime.eventDayChange;
        eventDayChange.AddListener(ReceivedDayChangeEvent);
        

        foreach (Transform child in panelFeaturedJobsInner)
        {
            //Debug.Log(child.gameObject.name);
            if (child.gameObject.name == "JobListSlot")
            {
                featuredJobSlots.Add(child);
            }
        }



        UpdateJobs(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReceivedDayChangeEvent()
    {
        UpdateJobs(true);
        Debug.Log("JOBSMANAGEMENT GOT THE EVENT");
    }

    public void UpdateJobs(bool dayHasChanged)
    {

        activeJobList = new List<JobHandler>();

        foreach (Transform jobTransform in transformJobList)
        {
            if(jobTransform.GetComponent<JobHandler>().currentState == JobHandler.state.ACTIVE)
            activeJobList.Add(jobTransform.gameObject.GetComponent<JobHandler>());
        }

        if (dayHasChanged)
        {
            foreach(JobHandler job in activeJobList)
            {
                job.remainingDays--;
                
                if (job.remainingDays <= 1)
                {
                    job.Expire();
                }
                Debug.Log(job.title + " " + job.remainingDays + " State: " + job.currentState);

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

        UpdateFeaturedJobs();


    }

    public void UpdateFeaturedJobs()
    {

       // Debug.Log(activeJobList.Count);
        //Debug.Log(featuredJobSlots.Count);
        for (int i=0; i < featuredJobSlots.Count; i++)
        {
            featuredJobSlots[i].GetChild(0).transform.localScale = new Vector3(0, 0, 0);
            featuredJobSlots[i].GetChild(2).transform.localScale = new Vector3(0, 0, 0);
            featuredJobSlots[i].GetChild(1).transform.localScale = new Vector3(0, 0, 0);


            if (i < activeJobList.Count)
            {
                featuredJobSlots[i].GetChild(1).transform.localScale = new Vector3(1, 1, 1);
                featuredJobSlots[i].GetChild(2).transform.localScale = new Vector3(1, 1, 1);

                // set job title
                featuredJobSlots[i].GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = activeJobList[i].title;

                // set day text
                if (activeJobList[i].startDays != 0)
                {
                   
                    featuredJobSlots[i].GetChild(2).gameObject.GetComponent<UnityEngine.UI.Text>().text = activeJobList[i].remainingDays.ToString();
                }
                else
                {
                    
                    featuredJobSlots[i].GetChild(2).gameObject.GetComponent<UnityEngine.UI.Text>().text = "-";
                }

                // show / hide star
                if (activeJobList[i].jobIsSaved)
                {
                    featuredJobSlots[i].GetChild(0).transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    featuredJobSlots[i].GetChild(0).transform.localScale = new Vector3(0, 0, 0);
                }
            }


          
            

        }
    }

    public void ToggleFeaturedJobsVisibility()
    {
        
        if (panelFeaturedJobsOuterVisible) HideFeaturedJobs();
        else ShowFeaturedJobs();

        panelFeaturedJobsOuterVisible = !panelFeaturedJobsOuterVisible;
    }

    public void ShowFeaturedJobs()
    {
        panelFeaturedJobsInner.localScale = new Vector3(1, 1, 1);
        featuredJobsToggleButton.GetComponent<UnityEngine.UI.Image>().color = Color.clear;
    }

    public void HideFeaturedJobs()
    {
        panelFeaturedJobsInner.localScale = new Vector3(0, 0, 0);
        featuredJobsToggleButton.GetComponent<UnityEngine.UI.Image>().color = new Color32(255, 255, 255, 100);
    }

    }
