using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTransferAmountScript : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource source;
    private GameObject panelSecondaryTransferAmountSelection;

    void Start()
    {
        panelSecondaryTransferAmountSelection = GameObject.Find("PanelSecondaryTransferAmountSelection");
        panelSecondaryTransferAmountSelection.transform.localScale = new Vector3(0, 0, 0);
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {

            if (
                !RectTransformUtility.RectangleContainsScreenPoint
                (
                panelSecondaryTransferAmountSelection.GetComponent<RectTransform>(),
                Input.mousePosition
                )
               )
            {
                panelSecondaryTransferAmountSelection.transform.localScale = new Vector3(0, 0, 0);
            }
        }
    }

    public void ButtonPress()
    {


        if(panelSecondaryTransferAmountSelection.transform.localScale.y > 0)
        {
            panelSecondaryTransferAmountSelection.transform.localScale = new Vector3(0, 0, 0);


            
        }
        else
        {
            panelSecondaryTransferAmountSelection.transform.localScale = new Vector3(1, 1, 1);

        }

        source = GetComponent<AudioSource>();
        source.PlayOneShot(clickSound, 1f);

    }
}
