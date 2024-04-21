using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobDetails : MonoBehaviour
{
    public JobsManagement jobsManagement;

    private JobHandler job;

    public GameObject UIjobTitle;
    public GameObject UIjobDescription;
    public GameObject UIclientName;
    public GameObject UIclientOccupation;
    public GameObject UIclientImage;
    public GameObject UIjobDateOfInquiry;
    public GameObject UILabelRequestedAlchemicum;
    public GameObject UIjobRequestedAlchemicum;
    public GameObject UIjobAmount;
    public GameObject UIjobDaysLeft;
    public GameObject UIjobCompensation;

    public Transform InventoryItemSlot;
    public GameObject SubmitButton;
    public GameObject ItemSubmissionInfoText;

    public JobFinishedScreen jobFinishedScreen;

    // Start is called before the first frame update
    void Start()
    {
        jobsManagement = GameObject.Find("JobManagement").GetComponent<JobsManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(JobHandler pJob)
    {

        Debug.Log("Inside JobDetails.Open() " + pJob.name);
        GetComponent<AutoTransferItemCapability>().PrepareAutoTransferTarget();
        UpdateSubmitButton();
        job = pJob;

        NotebookBaseUI.AddToHistory(this.gameObject, job.gameObject);

        UpdateJobDetails();
        UpdateItemSubmissionInfoText();

        GetComponent<NotebookBaseUI>().Open();
    }

    public void UpdateJobDetails()
    {
        //Debug.Log("details update");
 
        UIjobTitle.GetComponent<UnityEngine.UI.Text>().text = job.title;
        UIjobDescription.GetComponent<UnityEngine.UI.Text>().text = job.description;
        UIclientName.GetComponent<UnityEngine.UI.Text>().text = job.client.fullname;
        UIclientOccupation.GetComponent<UnityEngine.UI.Text>().text = job.client.occupation;
        UIclientImage.GetComponent<UnityEngine.UI.Image>().sprite = job.client.imagePortrait;
        UIjobDateOfInquiry.GetComponent<UnityEngine.UI.Text>().text = job.startPointInTime.ToString();

        if (job.requestedIngredientType != null)
        {
            UILabelRequestedAlchemicum.GetComponent<UnityEngine.UI.Text>().text = "Requested Alchemicum:";
            UIjobRequestedAlchemicum.gameObject.GetComponent<UnityEngine.UI.Text>().text = job.requestedIngredientType.ingredientName; // JobsManagement.activeJobList[i].payment.ToString();

        }
        else
        {
            UILabelRequestedAlchemicum.GetComponent<UnityEngine.UI.Text>().text = "Requested Effects:";
            UIjobRequestedAlchemicum.GetComponent<UnityEngine.UI.Text>().text = job.requestedEffectsString;
            // todo effekte
        }

        UIjobAmount.GetComponent<UnityEngine.UI.Text>().text = job.requestedAmount.ToString();
        UIjobDaysLeft.GetComponent<UnityEngine.UI.Text>().text = job.remainingDays.ToString();
        UIjobCompensation.GetComponent<UnityEngine.UI.Text>().text = job.payment.ToString();

    }

    public void SubmitInventoryItem()
    {
        // wenn wir in diese Methode gelangen, müssen wir nicht mehr prüfen ob das Item erfolgreich submitted wurde - das wird vorher abgefangen und nur bei korrektem Item darf man überhaupt hier hin.

        GameObject tempItem = InventoryItemSlot.GetChild(0).gameObject;
        jobsManagement.CompleteJob(job, tempItem.GetComponent<InventoryItemHandler>());

        // Finished Screen öffnen
        jobFinishedScreen.ShowScreen(job);

        // Item in Slot vernichten
        tempItem.transform.SetParent(GameObject.Find("CanvasDragItem").transform);
        Debug.Log(tempItem);
        GameObject.Destroy(tempItem);

        UpdateSubmitButton();
        UpdateItemSubmissionInfoText();

        
    }

    public void UpdateSubmitButton()
    {
        JobHandler.ItemAmountSuitable itemAmountChecked;
        JobHandler.ItemTypeSuitable itemTypeChecked;

        if (InventoryItemSlot.childCount == 0)
        {
            SubmitButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            itemAmountChecked = job.CheckItemAmoundSuitable(InventoryItemSlot.GetChild(0).gameObject.GetComponent<InventoryItemHandler>());
            itemTypeChecked = job.CheckItemTypeSuitable(InventoryItemSlot.GetChild(0).gameObject.GetComponent<InventoryItemHandler>());

            
            switch (itemTypeChecked)
                {
                    case JobHandler.ItemTypeSuitable.CORRECT_EFFECT_WITHOUT_UNWANTED_HARMFUL_SIDEFFECTS:
                    if (itemAmountChecked != JobHandler.ItemAmountSuitable.TOO_LITTLE)
                    {
                        SubmitButton.GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        SubmitButton.GetComponent<Button>().interactable = false;

                    }

                    break;

                    case JobHandler.ItemTypeSuitable.CORRECT_EFFECT_WITH_UNWANTED_HARMFUL_SIDEFFECTS:
                    if (itemAmountChecked != JobHandler.ItemAmountSuitable.TOO_LITTLE)
                    {
                        SubmitButton.GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        SubmitButton.GetComponent<Button>().interactable = false;

                    }

                    break;

                    case JobHandler.ItemTypeSuitable.WRONG_EFFECT:

                        SubmitButton.GetComponent<Button>().interactable = false;

                        break;

                    case JobHandler.ItemTypeSuitable.CORRECT_INVENTORYITEM:

                    if (itemAmountChecked != JobHandler.ItemAmountSuitable.TOO_LITTLE)
                    {
                        SubmitButton.GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        SubmitButton.GetComponent<Button>().interactable = false;

                    }

                    break;

                    case JobHandler.ItemTypeSuitable.WRONG_INVENTORYITEM:

                        SubmitButton.GetComponent<Button>().interactable = false;

                        break;
                }
            

        }
    }

    public void UpdateItemSubmissionInfoText()
    {
        JobHandler.ItemAmountSuitable itemAmountChecked;
        JobHandler.ItemTypeSuitable itemTypeChecked;
        if (InventoryItemSlot.childCount == 0)
        {
            ItemSubmissionInfoText.GetComponent<UnityEngine.UI.Text>().text = "";
        }
        else
        {
           
            itemAmountChecked = job.CheckItemAmoundSuitable(InventoryItemSlot.GetChild(0).gameObject.GetComponent<InventoryItemHandler>());
            itemTypeChecked = job.CheckItemTypeSuitable(InventoryItemSlot.GetChild(0).gameObject.GetComponent<InventoryItemHandler>());

            
            
                switch (itemTypeChecked)
                {
                    case JobHandler.ItemTypeSuitable.CORRECT_EFFECT_WITHOUT_UNWANTED_HARMFUL_SIDEFFECTS:

                    if (itemAmountChecked == JobHandler.ItemAmountSuitable.TOO_LITTLE)
                    {
                        ItemSubmissionInfoText.GetComponent<UnityEngine.UI.Text>().text =
                            "The amount of the alchemicum is too little. We provide an amount of "
                            + InventoryItemSlot.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().amountTotal
                            + ", but an amount of "
                            + job.requestedAmount
                            + " was requested. ";
                    }
                    else
                    {
                        ItemSubmissionInfoText.GetComponent<UnityEngine.UI.Text>().text = "";
                    }
                    break;

                    case JobHandler.ItemTypeSuitable.CORRECT_EFFECT_WITH_UNWANTED_HARMFUL_SIDEFFECTS:

                    if (itemAmountChecked == JobHandler.ItemAmountSuitable.TOO_LITTLE)
                    {
                        ItemSubmissionInfoText.GetComponent<UnityEngine.UI.Text>().text =
                            "The amount of the alchemicum is too little. We provide an amount of "
                            + InventoryItemSlot.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().amountTotal
                            + ", but an amount of "
                            + job.requestedAmount
                            + " was requested. ";
                    }
                    else
                    {
                        ItemSubmissionInfoText.GetComponent<UnityEngine.UI.Text>().text = "This alchemicum meets the desired effects, but it also has side effects which were not requested. You can submit this alchemicum, but doing so may lead to unexpected consequences. ";
                    }
                    break;


                case JobHandler.ItemTypeSuitable.WRONG_EFFECT:

                        if (job.requestedEffects.Count == 1)
                            ItemSubmissionInfoText.GetComponent<UnityEngine.UI.Text>().text = "This alchemicum does not have the requested effect. ";

                        else
                            ItemSubmissionInfoText.GetComponent<UnityEngine.UI.Text>().text = "This alchemicum does not have all the requested effects. ";

                        break;

                    case JobHandler.ItemTypeSuitable.CORRECT_INVENTORYITEM:

                    if (itemAmountChecked == JobHandler.ItemAmountSuitable.TOO_LITTLE)
                    {
                        ItemSubmissionInfoText.GetComponent<UnityEngine.UI.Text>().text =
                            "The amount of the alchemicum is too little. We provide an amount of "
                            + InventoryItemSlot.GetChild(0).gameObject.GetComponent<InventoryItemHandler>().amountTotal
                            + ", but an amount of "
                            + job.requestedAmount
                            + " was requested. ";
                    }
                    else
                    {
                        ItemSubmissionInfoText.GetComponent<UnityEngine.UI.Text>().text = "";
                    }  
                        break;

                    case JobHandler.ItemTypeSuitable.WRONG_INVENTORYITEM:

                        ItemSubmissionInfoText.GetComponent<UnityEngine.UI.Text>().text = "This is not the alchemicum the client asked for.";

                        break;
                }

            
        }
        
    }
}
