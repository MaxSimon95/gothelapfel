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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
                Close();
        }
    }

    public void Open()
    {
        transform.GetChild(0).localScale = new Vector3(1, 1, 1);
        notebookIsOpen = true;
        RenderOrderAdjustment.anyOverlayOpen = true;

    }

    public void Close()
    {
        Debug.Log("CLOSE");
        transform.GetChild(0).localScale = new Vector3(0, 0, 0);

        Debug.Log(transform.GetChild(0).localScale);
        notebookIsOpen = false;
        RenderOrderAdjustment.anyOverlayOpen = false;
        // 
        GameObject.Find("JobManagement").GetComponent<JobsManagement>().UpdateActiveJobs(false);
    }

    public void SwitchToTab()
    {
        // close this one
        // open the target
    }
    
}
