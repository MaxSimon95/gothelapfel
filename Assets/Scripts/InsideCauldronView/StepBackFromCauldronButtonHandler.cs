using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepBackFromCauldronButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StepBackFromCauldron()
    {

        GameObject.Find("CanvasCauldronView").GetComponent<CanvasContainerHandler>().CloseContainerView();
    }
}
