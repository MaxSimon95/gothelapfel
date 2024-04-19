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
    public static void ExecuteEventCode(DialogHandler dialog, string eventCode)
    {
        string[] actions = eventCode.Split(';');
        /*
        if (actions.Length > 0 + 1) ;
        string[] codeParameters0 = actions[0].Split(' ');
        if (actions.Length > 1 + 1) ;
        string[] codeParameters1 = actions[1].Split(' ');
        if (actions.Length > 2 + 1) ;
        string[] codeParameters2 = actions[2].Split(' ');
        if (actions.Length > 3 + 1) ;
        string[] codeParameters3 = actions[3].Split(' ');
        if (actions.Length > 4 + 1) ;
        string[] codeParameters4 = actions[4].Split(' ');
        if (actions.Length > 5 + 1) ;
        string[] codeParameters5 = actions[5].Split(' ');
        if (actions.Length > 6 + 1) ;
        string[] codeParameters6 = actions[6].Split(' ');
        if (actions.Length > 7 + 1) ;
        string[] codeParameters7 = actions[7].Split(' ');
        if (actions.Length > 8 + 1) ;
        string[] codeParameters8 = actions[8].Split(' ');
        if (actions.Length > 9 + 1) ;
        string[] codeParameters9 = actions[9].Split(' ');
        */
        //foreach (string codeSnippet in codeParameters) Debug.Log(codeSnippet);

        // go through each of the max. 10 codeParameterSnippets
        for(int i=0; i< actions.Length; i++)
        {
            string[] codeParameters = actions[i].Split(' ');
            /*
            switch (i)
            {
                case 0: codeParameters = codeParameters0;  break;
                case 1: codeParameters = codeParameters1; break;
                case 2: codeParameters = codeParameters2; break;
                case 3: codeParameters = codeParameters3; break;
                case 4: codeParameters = codeParameters4; break;
                case 5: codeParameters = codeParameters5; break;
                case 6: codeParameters = codeParameters6; break;
                case 7: codeParameters = codeParameters7; break;
                case 8: codeParameters = codeParameters8; break;
                case 9: codeParameters = codeParameters9; break;
                default: codeParameters = codeParameters0; break;
            }*/

            // parameter 0 is the Functionname if you will. All following parameters are function specific. The switch case here  handles the different function behaviours.
            switch (codeParameters[0])
            {
                case "ADDEVENTTOQUEUE":
                    Debug.Log("EVENT ADDEVENTTOQUEUE: " + codeParameters[1]);
                    break;

                case "DISCOVERRECIPE":
                    Debug.Log("EVENT DISCOVERRECIPE: " + codeParameters[1]);
                    break;

                case "DISCOVERINGREDIENT":
                    Debug.Log("TODO");
                    break;

                case "DISCOVERINGREDIEsNT":
                    Debug.Log("TODO");
                    break;
            }
        }

        

        //for debugging
        //dialog.eventQueue.AddListener(delegate { Debug.Log("Externally Added eventcode : '" + eventCode + "' gets executed."); });
    }
}
