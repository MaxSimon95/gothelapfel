using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogUIHandler : MonoBehaviour
{
    public GameObject dialogPanel;
    public GameObject panelActionable;
    public GameObject panelNotActionable;
    public GameObject panelContinue;
    public GameObject labelTextActionable;
    public GameObject labelTextNotActionable;
    public GameObject labelNPCnameTextActionable;
    public GameObject labelNPCnameNotActionable;
    public GameObject labelNPCtitlelabelTextActionable;
    public GameObject labelNPCtitleNotActionable;
    public GameObject pcImage;
    public GameObject npcImage;
    public List<GameObject> buttons = new List<GameObject>();

    private bool dialogIsOpen = false;

    DialogHandler activeDialog;
    DialogSectionHandler activeSection;
    DialogSubSectionHandler activeSubSection;

    private AudioSource source;
    public AudioClip clickSound;

    // Start is called before the first frame update
    void Start()
    {
        dialogPanel.transform.localScale = new Vector3(0, 0, 0);
        panelNotActionable.transform.localScale = new Vector3(0, 0, 0);
        panelActionable.transform.localScale = new Vector3(0, 0, 0);


        //for debugging
        StartDialog(GameObject.Find("TESTNPC_1").GetComponent<DialogHandler>());

    }

    // Update is called once per frame
    void Update()
    {
        if (!activeSubSection.actionable && dialogIsOpen && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)))
        {
            source = GetComponent<AudioSource>();
            source.PlayOneShot(clickSound, 1f);

            if (activeSubSection.finalSubSection)
            {
                EndDialog();
            }
            else
            {
                GoToNextSubSection();
            }
        }
    }

    public void StartDialog(DialogHandler dialog)
    {
        RenderOrderAdjustment.anyOverlayOpen = true;
        dialogIsOpen = true;
        GameTime.timeIsStopped = true;
        activeDialog = dialog;
        activeSection = activeDialog.transform.GetChild(0).GetComponent<DialogSectionHandler>();
        activeSubSection = activeSection.transform.GetChild(0).GetComponent<DialogSubSectionHandler>();
        OpenSubSection(activeSubSection);

        labelNPCnameTextActionable.GetComponent<UnityEngine.UI.Text>().text = activeDialog.npc.fullname;
        labelNPCnameNotActionable.GetComponent<UnityEngine.UI.Text>().text = activeDialog.npc.fullname; 
        labelNPCtitlelabelTextActionable.GetComponent<UnityEngine.UI.Text>().text = activeDialog.npc.occupation; 
        labelNPCtitleNotActionable.GetComponent<UnityEngine.UI.Text>().text = activeDialog.npc.occupation; 

        dialogPanel.transform.localScale = new Vector3(1, 1, 1);

    }

    public void EndDialog()
    {
        RenderOrderAdjustment.anyOverlayOpen = false;
        dialogIsOpen = false;
        GameTime.timeIsStopped = false;
        dialogPanel.transform.localScale = new Vector3(0, 0, 0);
    }
    public void GoToNextSubSection()
    {
        OpenSubSection(activeSection.transform.GetChild(activeSubSection.transform.GetSiblingIndex() + 1).GetComponent<DialogSubSectionHandler>());
    }

    public void OpenSubSection(DialogSubSectionHandler subsection)
    {
        activeSubSection = subsection;

        if (activeSubSection.actionable)
        {
            npcImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
            pcImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

            labelTextActionable.GetComponent<UnityEngine.UI.Text>().text = activeSubSection.text;
            panelActionable.transform.localScale = new Vector3(1, 1, 1);
            panelNotActionable.transform.localScale = new Vector3(0, 0, 0);

            for(int i=0; i< buttons.Count; i++)
            {
                if(i<activeSubSection.buttonLabels.Count)
                {
                    buttons[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = activeSubSection.buttonLabels[i];
                    buttons[i].transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    buttons[i].transform.localScale = new Vector3(0, 0, 0);
                }
            }
        }
        else
        {
            npcImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            pcImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
            if (activeSubSection.finalSubSection)
            {
                panelContinue.transform.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                panelContinue.transform.localScale = new Vector3(1, 1, 1);
            }
            labelTextNotActionable.GetComponent<UnityEngine.UI.Text>().text = activeSubSection.text;
            panelActionable.transform.localScale = new Vector3(0, 0, 0);
            panelNotActionable.transform.localScale = new Vector3(1, 1, 1);

        }
    }

    public void GoToSection(DialogSectionHandler section)
    {
        activeSection = section;
        OpenSubSection(activeSection.transform.GetChild(0).GetComponent<DialogSubSectionHandler>());
        
    }

    public void DialogOptionSelected(int i)
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(clickSound, 1f);
        GoToSection(activeSubSection.buttonFollowUpDialogSections[i]);
    }
}
