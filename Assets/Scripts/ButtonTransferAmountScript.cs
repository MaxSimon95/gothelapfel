using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTransferAmountScript : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource source;
    bool isActivePanelSecondaryTransferAmountSelection = false;
    private GameObject panelSecondaryTransferAmountSelection;
    // Start is called before the first frame update
    void Start()
    {
        panelSecondaryTransferAmountSelection = GameObject.Find("PanelSecondaryTransferAmountSelection");
        panelSecondaryTransferAmountSelection.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPress()
    {

        
        //if (panelSecondaryTransferAmountSelection.activeInHierarchy)
        if(isActivePanelSecondaryTransferAmountSelection)
        {
            panelSecondaryTransferAmountSelection.transform.localScale = new Vector3(0, 0, 0);
            isActivePanelSecondaryTransferAmountSelection = false;
        }
        else
        {
            panelSecondaryTransferAmountSelection.transform.localScale = new Vector3(1, 1, 1);
            isActivePanelSecondaryTransferAmountSelection = true;
        }

        source = GetComponent<AudioSource>();
        source.PlayOneShot(clickSound, 1f);

    }
}
