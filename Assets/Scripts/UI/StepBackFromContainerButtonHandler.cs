using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepBackFromContainerButtonHandler : MonoBehaviour
{
    public Canvas containerCanvas;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StepBackFromContainer()
    {

        containerCanvas.GetComponent<CanvasContainerHandler>().CloseContainerView();
    }
}
