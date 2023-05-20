using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookRegions : MonoBehaviour
{
    private List<Transform> regionPanels = new List<Transform>();
    private List<RegionHandler> regions = new List<RegionHandler>();

    public Transform parentRegionsTransform;
    public Transform parentRegionPanelsTransform;

    public NotebookRegionDetails notebookRegionDetails;

    int openPage = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        // jobManagement.OrderActiveJobs(noteBookActiveJobSorting);
        // jobList = JobsManagement.activeJobList;
        UpdateRegionPanels();
        GetComponent<NotebookBaseUI>().Open();
    }

    public void UpdateRegionPanels()
    {
        LoadRegionPanels();
        LoadRegions();

        for (int i = 0; i < regionPanels.Count; i++)
        {

            regionPanels[i].gameObject.SetActive(false);

            if (i < regions.Count)
            {

                regionPanels[i].gameObject.SetActive(true);

                // CAREFUL: THIS STUFF IS ORDERING SENSITIVE. YOU MESS WITH THE ORDERING, YOU MESS WITH THE CONTENTS, YO! 

                // setting sprite: 
                //recipePanels[i].GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = recipesList[i + openPage * 100].inventorySprite;

                regionPanels[i].GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = regions[i + openPage * 100].regionName;


            }
        }
    }

    public void LoadRegionPanels()
    {
        regionPanels.Clear();
        foreach (Transform tempRegionPanel in parentRegionPanelsTransform)
        {
            regionPanels.Add(tempRegionPanel);
        }
    }

    public void LoadRegions()
    {
        //TODO
        regions.Clear();
        foreach (Transform tempRegionTransform in parentRegionsTransform)
        {
            regions.Add(tempRegionTransform.gameObject.GetComponent<RegionHandler>());
        }
    }

    public void OpenRegionDetails(int i)
    {
        //Debug.Log(i + openPage * 10);
        //Debug.Log(i + " " + JobsManagement.activeJobList[i + openPage * 10].title);
        //Debug.Log(jobList[i + openPage * 10]);
        //JobHandler.detailJob = JobsManagement.activeJobList[i + openPage * 10];
        //jobDetails.Open(jobList[i + openPage * 10]);

        //TODO

        GetComponent<NotebookBaseUI>().Close();
        notebookRegionDetails.Open(regions[i]);

    }


}
