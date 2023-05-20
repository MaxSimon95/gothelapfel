using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedHandler : MonoBehaviour
{
    public SleepHandler sleepHandler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToSleep()
    {
        GetComponent<CanvasContainerHandler>().CloseContainerView();
        sleepHandler.Sleep();
    }

    /*public void OpenBedCanvas()
    {

    }*/




}
