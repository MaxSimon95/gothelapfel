using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class JobsManagement : MonoBehaviour
{

    public Transform panelFeaturedJobs;
    public Transform transformActiveJobs;
    public Transform transformUpcomingJobs;
    public Transform transformDoneJobs;

    public List<JobHandler> activeJobList;

    private List<Transform> featuredJobSlots;


    // Start is called before the first frame update
    void Start()
    {
        OrderActiveJobs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateJobs()
    {
        
    }

    void OrderActiveJobs()
    {
        List<JobHandler> activeJobList = new List<JobHandler>();

        foreach (Transform jobTransform in transformActiveJobs)
        {
            activeJobList.Add(jobTransform.gameObject.GetComponent<JobHandler>());
        }

        
        activeJobList = activeJobList.OrderBy(e => e.orderNumber).ToList();

       for (int i=0; i< activeJobList.Count; i++)
        {
            Debug.Log(activeJobList[i]);
            Debug.Log(activeJobList[i].orderNumber);
            Debug.Log("_______________________");

        }

        UpdateFeaturedJobs();


    }

    void UpdateFeaturedJobs()
    {

    }
    
}
