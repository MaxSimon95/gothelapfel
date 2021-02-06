using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeaturedJobListSlotHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenJobDetails(int i)
    {
        //Debug.Log(i + openPage * 10);
        //Debug.Log(i + " " + JobsManagement.activeJobList[i + openPage * 10].title);
        //Debug.Log(jobList[i + openPage * 10]);
        //JobHandler.detailJob = JobsManagement.activeJobList[i + openPage * 10];
        GameObject.Find("CanvasNotebookJobDetails").GetComponent<JobDetails>().Open(JobsManagement.activeJobList[i]);

    }
}
