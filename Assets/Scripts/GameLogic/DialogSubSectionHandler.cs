using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DialogSubSectionHandler : MonoBehaviour
{
    public string text;
    // both  and finalSubSection gets automatically set.
    public bool actionable=false;
    public bool finalSubSection = false;
    
    public List<string> buttonLabels = new List <string>();
    public List<DialogSectionHandler> buttonFollowUpDialogSections = new List<DialogSectionHandler>();
    public List<string> buttonDialogEventCodes = new List<string>();
    /*
    private GameObject panelActionable;
    private GameObject panelNotActionable;
    private GameObject labelText; */

    // Start is called before the first frame update
    void Start()
    {
        PopulateActionListIfEmpty();

    }
    void Awake()
    {
        if (buttonLabels.Count > 0) actionable = true;
        if ((transform.parent.childCount-1) == transform.GetSiblingIndex()) finalSubSection = true;
        /*
        panelActionable = GameObject.Find("PanelActionable");
        panelNotActionable = GameObject.Find("PanelNotActionable");
        if (buttonActions.Count > 0) actionable = true;

        if(actionable)
        {
            labelText = GameObject.Find("ActionableText");
        }
        else
        {
            labelText = GameObject.Find("NotActionableText");
        } */
    }

    private void PopulateActionListIfEmpty()
    {
        while(buttonDialogEventCodes.Count < buttonLabels.Count)
        {
            buttonDialogEventCodes.Add("");
        }

       /* foreach(string e in buttonDialogEventCodes)
        {
          
            Debug.Log(e);
        } */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
