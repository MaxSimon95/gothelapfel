using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookJobs : MonoBehaviour
{
    // Start is called before the first frame update
    public static JobsManagement.sortingType noteBookActiveJobSorting = JobsManagement.sortingType.AGE;
    private List<Transform> jobPanels = new List<Transform>();
    private JobsManagement jobManagement;

    void Start()
    {
        jobManagement = GameObject.Find("JobManagement").GetComponent<JobsManagement>();

        foreach (Transform child in transform.GetChild(0))
        {
            //Debug.Log(child.gameObject.name);
            if (child.gameObject.name == "PanelJob")
            {
                jobPanels.Add(child);
            }
        }
        
    }

    public void Open()
    {
        jobManagement.OrderActiveJobs(noteBookActiveJobSorting);
        UpdateJobPanels();
    }

    public void UpdateJobPanels()
    {
        for (int i = 0; i < jobPanels.Count; i++)
        {
            jobPanels[i].localScale = new Vector3(0, 0, 0);

            //Debug.Log(JobsManagement.activeJobList.Count);
            if (i < JobsManagement.activeJobList.Count)
            {
                jobPanels[i].localScale = new Vector3(1, 1, 1);


                // CAREFUL: THIS STUFF IS ORDER SENSITIVE. YOU MESS WITH THE ORDER, YOU MESS WITH THE CONTENTS, YO! 

                jobPanels[i].GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = JobsManagement.activeJobList[i].title;
                jobPanels[i].GetChild(3).gameObject.GetComponent<UnityEngine.UI.Text>().text = JobsManagement.activeJobList[i].requestedAmount.ToString();
                jobPanels[i].GetChild(5).gameObject.GetComponent<UnityEngine.UI.Text>().text = JobsManagement.activeJobList[i].remainingDays.ToString();
                jobPanels[i].GetChild(7).gameObject.GetComponent<UnityEngine.UI.Text>().text = JobsManagement.activeJobList[i].payment.ToString();
                jobPanels[i].GetChild(9).gameObject.GetComponent<UnityEngine.UI.Text>().text = "TODO load requested effects!!! "; // JobsManagement.activeJobList[i].payment.ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
