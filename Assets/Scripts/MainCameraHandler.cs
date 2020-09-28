using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraHandler : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {

                Debug.Log(hit.collider.name);

            }
        }

    }
}
