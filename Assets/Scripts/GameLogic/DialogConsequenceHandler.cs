using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogConsequenceHandler : MonoBehaviour
{

    public delegate void DialogConsequence();
    public DialogConsequence dialogConsequence;

    // Start is called before the first frame update
    void Start()
    {
        dialogConsequence = AddEventToQueue;
        dialogConsequence();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEventToQueue()
    {
        Debug.Log("AddEventToQueue");
    }
}
