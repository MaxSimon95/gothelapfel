using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookBaseUI : MonoBehaviour
{
    public static bool notebookIsOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        transform.GetChild(0).localScale = new Vector3(1, 1, 1);
        notebookIsOpen = true;
    }

    public void Close()
    {
        transform.GetChild(0).localScale = new Vector3(0, 0, 0);
        notebookIsOpen = false;
        // 
        GameObject.Find("JobManagement").GetComponent<JobsManagement>().UpdateActiveJobs(false);
    }

    public void SwitchToTab()
    {
        // close this one
        // open the target
    }
    
}
