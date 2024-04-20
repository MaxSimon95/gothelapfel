using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventHandler : MonoBehaviour
{
    public string eventCode;
    public string description;

    // used for setting some events already to be put into the eventqueue from the beginning, in the unity editor
    public bool inEventQueue;
    public int date;
    public int hour;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExecuteEvent()
    {
        eventCode = DialogEventCodeExecutor.RepairEventCode(eventCode);
        string[] actions = eventCode.Split(';');

        for (int i = 0; i < actions.Length; i++)
        {
            string[] codeParameters = actions[i].Split(' ');

            // parameter 0 is the Functionname if you will. All following parameters are function specific. The switch case here  handles the different function behaviours.
            switch (codeParameters[0])
            {
                case "TEST":
                    Debug.Log("Event TEST in Qeue execeuted");
                    throw new System.NotImplementedException();

                    break;

                case "TEST2":
                    //Debug.Log("case ADD_EVENT_TO_QUEUE");
                    throw new System.NotImplementedException();


                    break;

              
            }
        }
    }
}
