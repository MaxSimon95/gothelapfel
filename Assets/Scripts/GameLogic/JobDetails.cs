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
    }

    public void UpdateSubmitButton()
    {
        if (InventoryItemSlot.childCount == 0)
        {
            SubmitButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            SubmitButton.GetComponent<Button>().interactable = true;
            Debug.Log(job.CheckItemAmoundSuitable(InventoryItemSlot.GetChild(0).gameObject.GetComponent<InventoryItemHandler>()));
            Debug.Log(job.CheckItemTypeSuitable(InventoryItemSlot.GetChild(0).gameObject.GetComponent<InventoryItemHandler>()));
        }
    }
}
