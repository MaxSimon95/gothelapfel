using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookJobs : MonoBehaviour
{
    // Start is called before the first frame update
    public static JobsManagement.sortingType noteBookActiveJobSorting = JobsManagement.sortingType.AGE;
    private List<Transform> jobPanels = new List<Transform>();
    private JobsManagement jobManagement;
    private List <JobHandler> jobList = new List<JobHandler>();

    public JobDetails jobDetails;

    int openPage=0;

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
        jobList = JobsManagement.activeJobList;

        NotebookBaseUI.AddToHistory(this.gameObject);

        UpdateJobPanels();

        GetComponent<NotebookBaseUI>().Open();
    }
    
    public void UpdateJobPanels()
    {
        for (int i = 0; i < jobPanels.Count; i++)
        {
            jobPanels[i].localScale = new Vector3(0, 0, 0);

            //Debug.Log(i);

            //Debug.Log(JobsManagement.activeJobList.Count);
            if (i < jobList.Count)
            {
                jobPanels[i].localScale = new Vector3(1, 1, 1);

                //Debug.Log(i);
                // CAREFUL: THIS STUFF IS ORDERING SENSITIVE. YOU MESS WITH THE ORDERING, YOU MESS WITH THE CONTENTS, YO! 
                //Debug.Log(i);
                jobPanels[i].GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = jobList[i + openPage * 10].title;
                jobPanels[i].GetChild(3).gameObject.GetComponent<UnityEngine.UI.Text>().text = jobList[i + openPage * 10].requestedAmount.ToString();
                jobPanels[i].GetChild(5).gameObject.GetComponent<UnityEngine.UI.Text>().text = jobList[i + openPage * 10].remainingDays.ToString();
                jobPanels[i].GetChild(7).gameObject.GetComponent<UnityEngine.UI.Text>().text = jobList[i + openPage * 10].payment.ToString();
                //Debug.Log(i);
                if (jobList[i].requestedIngredientType != null)
                {
                    jobPanels[i].GetChild(8).gameObject.GetComponent<UnityEngine.UI.Text>().text = "Requested Alchemicum:"; 
                    jobPanels[i].GetChild(9).gameObject.GetComponent<UnityEngine.UI.Text>().text = jobList[i + openPage * 10].requestedIngredientType.ingredientName; // JobsManagement.activeJobList[i].payment.ToString();
                    //Debug.Log(i);
                }
                else
                {
                    jobPanels[i].GetChild(8).gameObject.GetComponent<UnityEngine.UI.Text>().text = "Requested Effects:";
                    jobPanels[i].GetChild(9).gameObject.GetComponent<UnityEngine.UI.Text>().text = jobList[i + openPage * 10].requestedEffectsString;
                    // todo effekte
                    //Debug.Log(i);
                }
                //Debug.Log(i);

            }
        }
    }

    public void OpenJobDetails(int i)
    {
        //Debug.Log(i + openPage * 10);
        //Debug.Log(i + " " + JobsManagement.activeJobList[i + openPage * 10].title);
        //Debug.Log(jobList[i + openPage * 10]);
        //JobHandler.detailJob = JobsManagement.activeJobList[i + openPage * 10];
        jobDetails.Open(jobList[i + openPage * 10]);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
