using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorKnockHandler : MonoBehaviour
{
    public GameEvents gameEvents;

    public bool knockingActive;
    public DialogHandler impendingDialog;
    public DialogUIHandler dialogUI;

    public int MaxKnockingDurationInSeconds;
    private int MaxKnockingDurationInIterations=10;

    private AudioSource source;
    public AudioClip sound;

    public List<GameObject> EffectObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //for debugging
        //AddImpedingDialog(GameObject.Find("TESTNPC_1").GetComponent<DialogHandler>());
        foreach (GameObject specialeffect in EffectObjects)
        {
            LeanTween.alpha(specialeffect, 0, 0);
        }
    }

    public void AddImpedingDialog(DialogHandler pImpedingDialog)
    {
        

        if(knockingActive)
        {
            gameEvents.AddEventToQueue(pImpedingDialog, true);
            
        }
        else
        {
            impendingDialog = pImpedingDialog;
            knockingActive = true;
            StartCoroutine(Knocking());
        }

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
            foreach(GameObject specialeffect in EffectObjects)
            {
                LeanTween.cancel(specialeffect);
                LeanTween.alpha(specialeffect, 1, 0);
                LeanTween.alpha(specialeffect, 0f, 0.5f).setDelay(0.5f); ;
            }

            //wait before repeat

            yield return new WaitForSeconds(4);
            
        }
        StopKnocking();
        gameEvents.AddEventToQueue(impendingDialog, false);

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
