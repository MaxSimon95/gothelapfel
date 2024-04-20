using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEventCodeExecutor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string RepairEventCode(string eventCode)
    {
        string tempCode = eventCode;
        tempCode = tempCode.Replace("; ", ";");
        tempCode = tempCode.Replace(" ;", ";");
        tempCode = tempCode.Replace("  ", " ");

        return (tempCode);
    }

    public static void ExecuteEventCode(DialogHandler dialog, string pEventCode)
    {
        string eventCode = RepairEventCode(pEventCode);
        Debug.Log(eventCode);

        string[] actions = eventCode.Split(';');


        // go through each of the codeParameterSnippets
        for(int i=0; i< actions.Length; i++)
        {
            string[] codeParameters = actions[i].Split(' ');
   

            // parameter 0 is the Functionname if you will. All following parameters are function specific. The switch case here  handles the different function behaviours.
            switch (codeParameters[0])
            {
                case "ADD_EVENT_TO_QUEUE":
                    //Debug.Log("case ADD_EVENT_TO_QUEUE");
                    dialog.eventQueue.AddListener(delegate 
                    { 
                        Debug.Log("EVENT ADDEVENTTOQUEUE: " + codeParameters[1]);
                        throw new System.NotImplementedException();
                    });
                    
                    break;

                case "DISCOVER_RECIPE":
                    //Debug.Log("case EVENT DISCOVERRECIPE");
                    dialog.eventQueue.AddListener(delegate
                    {
                        Debug.Log("EVENT DISCOVERRECIPE: " + codeParameters[1]);
                        throw new System.NotImplementedException();
                    });
                    
                    break;

                case "DISCOVER_INGREDIENT":
                    dialog.eventQueue.AddListener(delegate
                    {
                        throw new System.NotImplementedException();
                    });

                    break;

                case "DISCOVER_INGREDIEsNT":
                    Debug.Log("TODO");
                    break;
            }
        }

        

        //for debugging
        //dialog.eventQueue.AddListener(delegate { Debug.Log("Externally Added eventcode : '" + eventCode + "' gets executed."); });
    }
}
