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

    private List<Transform> featuredJobSlots = new List<Transform>();


    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in panelFeaturedJobs)
        {
            //Debug.Log(child.gameObject.name);
            if (child.gameObject.name == "JobListSlot")
            {
                featuredJobSlots.Add(child);
            }
        }



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
        activeJobList = new List<JobHandler>();

        foreach (Transform jobTransform in transformActiveJobs)
        {
            activeJobList.Add(jobTransform.gameObject.GetComponent<JobHandler>());
        }

        
        activeJobList = activeJobList.OrderBy(e => e.orderNumber).ToList();

       for (int i=0; i< activeJobList.Count; i++)
        {
            //Debug.Log(activeJobList[i]);
            //Debug.Log(activeJobList[i].orderNumber);
            //Debug.Log("_______________________");

        }

        UpdateFeaturedJobs();


    }

    void UpdateFeaturedJobs()
    {

       // Debug.Log(activeJobList.Count);
        //Debug.Log(featuredJobSlots.Count);
        for (int i=0; (i < activeJobList.Count) && (i < featuredJobSlots.Count); i++)
        {
           // Debug.Log("Nutmeg");
           // Debug.Log(featuredJobSlots[i].GetChild(1));
            featuredJobSlots[i].GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = activeJobList[i].title;

            if(activeJobList[i].startDays != 0)
            {
                featuredJobSlots[i].GetChild(2).gameObject.GetComponent<UnityEngine.UI.Text>().text = activeJobList[i].remainingDays.ToString();
            }
            else
            {
                featuredJobSlots[i].GetChild(2).gameObject.GetComponent<UnityEngine.UI.Text>().text = "-";
            }
            
        }
    }
    
}
