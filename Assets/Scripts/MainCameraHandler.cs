using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraHandler : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    void Update()
    {
        
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {

           

            if (Input.GetMouseButton(0))
            {
                //Debug.Log(hit.collider.name);

            }
        }

    }
}
