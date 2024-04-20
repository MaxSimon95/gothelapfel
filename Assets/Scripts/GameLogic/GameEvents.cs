using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameEvents : MonoBehaviour
{
    public GameTime gametime;

    public static List<GameEventHandler> eventQueue = new List<GameEventHandler>();

    public DoorKnockHandler doorKnockHandler;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform eventTransform in transform)
        {
            if (eventTransform.GetComponent<GameEventHandler>().inEventQueue)
                AddEventToQueue(eventTransform.GetComponent<GameEventHandler>());
        }
    }

    void AddEventToQueue(GameEventHandler gameEvent)
    {
        //Debug.Log("Adding Event to Queue: " + gameEvent.name);
        gameEvent.inEventQueue = true;
        eventQueue.Add(gameEvent);
    }

    public static void CheckForExecutableEvent(int date, int hour)
    {
        
        foreach (GameEventHandler gameEvent in eventQueue)
        {
            //Debug.Log("date: " + date + ", hour: " + hour + ", gameEvent.date " + gameEvent.date + ", gameEvent.hour " + gameEvent.hour);
            if ((gameEvent.date==date)&&(gameEvent.hour==hour))
            {
                gameEvent.ExecuteEvent();
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
