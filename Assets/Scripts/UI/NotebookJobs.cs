using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookJobs : MonoBehaviour
{
    // Start is called before the first frame update
    //public static enum
    
    void Start()
    {
        transform.GetChild(0).localScale = new Vector3(0, 0, 0);
    }

    public void OpenNotebook()
    {
        transform.GetChild(0).localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
