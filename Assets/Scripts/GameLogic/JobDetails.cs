using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
