using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorKnockHandler : MonoBehaviour
{

    public bool knockingActive;
    public DialogHandler impendingDialog;
    public DialogUIHandler dialogUI;

    public int MaxKnockingDurationInSeconds;
    private int MaxKnockingDurationInIterations=400;

    private AudioSource source;
    public AudioClip sound;

    // Start is called before the first frame update
    void Start()
    {
        //for debugging
        AddImpedingDialog(GameObject.Find("TESTNPC_1").GetComponent<DialogHandler>());
    }

    public void AddImpedingDialog(DialogHandler pImpedingDialog)
    {
        impendingDialog = pImpedingDialog;
        knockingActive = true;
        StartCoroutine(Knocking());
    }

    public IEnumerator Knocking()
    {
        // knocking can stop manually (opening the door) or after the timeout has been reached (maxknockingdurations)
        for(int i=0; knockingActive&&(i < MaxKnockingDurationInIterations); i++)
        {
            //sound
            source = GetComponent<AudioSource>();
            source.PlayOneShot(sound, 1f);

            //animation

            //wait before repeat

            yield return new WaitForSeconds(4);
            
        }
        StopKnocking();
    }

    public void StopKnocking()
    {
        knockingActive = false;
    }

    public void OpenDoor()
    {
        StopKnocking();
        dialogUI.StartDialog(impendingDialog);
        impendingDialog = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
