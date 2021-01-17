using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeaturedJobs : MonoBehaviour
{

    private List<Transform> featuredJobSlots = new List<Transform>();
    public bool panelFeaturedJobsOuterVisible = true;
    public GameObject featuredJobsToggleButton;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            //Debug.Log(child.gameObject.name);
            if (child.gameObject.name == "JobListSlot")
            {
                featuredJobSlots.Add(child);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlipArrow()
    {
        Vector3 scale = featuredJobsToggleButton.transform.GetChild(1).transform.localScale;
        scale *= -1;
        featuredJobsToggleButton.transform.GetChild(1).transform.localScale = scale;
    }

    public void UpdateFeaturedJobs()
    {

        // Debug.Log(activeJobList.Count);
        //Debug.Log(featuredJobSlots.Count);
        for (int i = 0; i < featuredJobSlots.Count; i++)
        {
            featuredJobSlots[i].GetChild(0).transform.localScale = new Vector3(0, 0, 0);
            featuredJobSlots[i].GetChild(2).transform.localScale = new Vector3(0, 0, 0);
            featuredJobSlots[i].GetChild(1).transform.localScale = new Vector3(0, 0, 0);


            if (i < JobsManagement.activeJobList.Count)
            {
                featuredJobSlots[i].GetChild(1).transform.localScale = new Vector3(1, 1, 1);
                featuredJobSlots[i].GetChild(2).transform.localScale = new Vector3(1, 1, 1);

                // set job title
                featuredJobSlots[i].GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = JobsManagement.activeJobList[i].title;

                // set day text
                if (JobsManagement.activeJobList[i].startDays != 0)
                {

                    featuredJobSlots[i].GetChild(2).gameObject.GetComponent<UnityEngine.UI.Text>().text = JobsManagement.activeJobList[i].remainingDays.ToString();
                }
                else
                {

                    featuredJobSlots[i].GetChild(2).gameObject.GetComponent<UnityEngine.UI.Text>().text = "-";
                }

                // show / hide star
                if (JobsManagement.activeJobList[i].jobIsSaved)
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

        FlipArrow();
    }

    public void ShowFeaturedJobs()
    {
        transform.localScale = new Vector3(1, 1, 1);
        featuredJobsToggleButton.GetComponent<UnityEngine.UI.Image>().color = Color.clear;
    }

    public void HideFeaturedJobs()
    {
        transform.localScale = new Vector3(0, 0, 0);
        featuredJobsToggleButton.GetComponent<UnityEngine.UI.Image>().color = new Color32(255, 255, 255, 100);
    }

}


