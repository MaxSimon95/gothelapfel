using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobDetails : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(JobHandler pJob)
    {
        GetComponent<AutoTransferItemCapability>().PrepareAutoTransferTarget();
        UpdateSubmitButton();
        job = pJob;
        UpdateJobDetails();
        UpdateItemSubmissionInfoText();
    }

    public void UpdateJobDetails()
    {
        Debug.Log("details update");
 
        UIjobTitle.GetComponent<UnityEngine.UI.Text>().text = job.title;
        UIjobDescription.GetComponent<UnityEngine.UI.Text>().text = job.description;
        UIclientName.GetComponent<UnityEngine.UI.Text>().text = job.client.fullname;
        UIclientOccupation.GetComponent<UnityEngine.UI.Text>().text = job.client.occupation;
        UIclientImage.GetComponent<UnityEngine.UI.Image>().sprite = job.client.image;
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
        GameObject tempItem = InventoryItemSlot.GetChild(0).gameObject;
        JobsManagement.CompleteJob(job, tempItem.GetComponent<InventoryItemHandler>());


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

            if (itemAmountChecked != JobHandler.ItemAmountSuitable.TOO_LITTLE)
            {
                switch (itemTypeChecked)
                {
                    case JobHandler.ItemTypeSuitable.CORRECT_EFFECT_WITHOUT_UNWANTED_HARMFUL_SIDEFFECTS:
                        SubmitButton.GetComponent<Button>().interactable = true;

                        break;

                    case JobHandler.ItemTypeSuitable.CORRECT_EFFECT_WITH_UNWANTED_HARMFUL_SIDEFFECTS:
                        SubmitButton.GetComponent<Button>().interactable = true;

                        break;

                    case JobHandler.ItemTypeSuitable.WRONG_EFFECT:

                        SubmitButton.GetComponent<Button>().interactable = true;

                        break;

                    case JobHandler.ItemTypeSuitable.CORRECT_INVENTORYITEM:

                        SubmitButton.GetComponent<Button>().interactable = true;

                        break;

                    case JobHandler.ItemTypeSuitable.WRONG_INVENTORYITEM:

                        SubmitButton.GetComponent<Button>().interactable = false;

                        break;
                }
            }
            else
            {
                SubmitButton.GetComponent<Button>().interactable = false;

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
                switch (itemTypeChecked)
                {
                    case JobHandler.ItemTypeSuitable.CORRECT_EFFECT_WITHOUT_UNWANTED_HARMFUL_SIDEFFECTS:

                        ItemSubmissionInfoText.GetComponent<UnityEngine.UI.Text>().text = "";

                        break;

                    case JobHandler.ItemTypeSuitable.CORRECT_EFFECT_WITH_UNWANTED_HARMFUL_SIDEFFECTS:

                        ItemSubmissionInfoText.GetComponent<UnityEngine.UI.Text>().text = "This alchemicum meets the desired effects, but it also has side effects which were not requested. You can submit this alchemicum, but doing so may lead to unexpected consequences. ";

                        break;

                    case JobHandler.ItemTypeSuitable.WRONG_EFFECT:

                        if (job.requestedEffects.Count == 1)
                            ItemSubmissionInfoText.GetComponent<UnityEngine.UI.Text>().text = "This alchemicum does not have the desired effect. You can submit this alchemicum, but you likely won't be paid and the client's opinion of you will decrease.";

                        else
                            ItemSubmissionInfoText.GetComponent<UnityEngine.UI.Text>().text = "This alchemicum does not have all the desired effects. You can submit this alchemicum, but you likely won't be paid and the client's opinion of you will decrease.";

                        break;

                    case JobHandler.ItemTypeSuitable.CORRECT_INVENTORYITEM:

                        ItemSubmissionInfoText.GetComponent<UnityEngine.UI.Text>().text = "";

                        break;

                    case JobHandler.ItemTypeSuitable.WRONG_INVENTORYITEM:

                        ItemSubmissionInfoText.GetComponent<UnityEngine.UI.Text>().text = "This is not the alchemicum the client asked for.";

                        break;
                }

            }
        }
        
    }
}
