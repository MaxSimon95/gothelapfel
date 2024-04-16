using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobFinishedScreen : MonoBehaviour
{
    public GameObject panel;
    public GameObject panelMoneyChange;
    public GameObject textMoneyChangeAmount;
    public GameObject textJobTitle;
    public JobDetails jobDetails;
    public MoneyHandler moneyHandler;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        panel.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowScreen(JobHandler pJob)
    {
        textJobTitle.GetComponent<UnityEngine.UI.Text>().text = pJob.title;
        textMoneyChangeAmount.GetComponent<UnityEngine.UI.Text>().text = pJob.payment.ToString();
        LayoutRebuilder.ForceRebuildLayoutImmediate(panelMoneyChange.GetComponent<RectTransform>());
        panel.transform.localScale = new Vector3(1, 1, 1);
    }

    public void CloseScreen()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(moneyHandler.moneySound, 1f);
        jobDetails.GetComponent<NotebookBaseUI>().Close();
        panel.transform.localScale = new Vector3(0, 0, 0);
    }



}
