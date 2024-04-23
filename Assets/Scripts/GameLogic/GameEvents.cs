using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameEvents : MonoBehaviour
{
    public GameTime gametime;

    public static List<GameEventHandler> eventQueue = new List<GameEventHandler>();
    public int eventConflictHourOffset; // used when 2 events would be at the same time, to move the second event bis this amount of hours back(repeated until a free spot is found)
    public DoorKnockHandler doorKnockHandler;
    public SleepHandler sleepHandler;
    

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform eventTransform in transform)
        {
            if (eventTransform.GetComponent<GameEventHandler>().inEventQueue)
                AddEventToQueue(eventTransform.GetComponent<GameEventHandler>());
        }
    }

    // all other AddEventToQeue overloads will be translated into this one, which works with the GameEventHandler we will ultimately use. Here we do the final adjustments to make sure no Events overlap on the hour.
    public void AddEventToQueue(GameEventHandler gameEvent)
    {
        //Debug.Log("Adde Event To Queue");
        //check if there's already an event at that date+hour, if so, move this one X hours
        int tempDate = gameEvent.date;
        int tempHour = gameEvent.hour; 
        while(CheckForExecutableEvent(tempDate, tempHour, false)!=null)
        {
            
            if(tempHour<(24-eventConflictHourOffset))
            {
                
                tempHour = tempHour + eventConflictHourOffset;
            }
            else
            {
                
                tempHour = sleepHandler.wakeUpTime+1;
                tempDate = tempDate + 1;
            }
        }
        //now that a free date+hour slot has been found, the original gamevent gets adjusted.
        gameEvent.date = tempDate;
        gameEvent.hour = tempHour;

        //Debug.Log("Adding Event to Queue: " + gameEvent.name);
        gameEvent.inEventQueue = true;
        eventQueue.Add(gameEvent);
    }

    // Overload to allow directly inputting a Dialog as an Event
    public void AddEventToQueue(DialogHandler dialog, bool executeToday)
    {
        GameObject tempGO = (GameObject)Instantiate(Resources.Load("GameEventPrefab"), new Vector3(0, 0, 0), Quaternion.identity);
        GameEventHandler tempGameEvent = tempGO.GetComponent<GameEventHandler>();
        tempGameEvent.eventCode = "DIALOGUE " + dialog.name;
        //Debug.Log("tempGameEvent.eventCode: " + tempGameEvent.eventCode);
        tempGameEvent.description = "EventHandler created from code.";
        tempGameEvent.inEventQueue = false; // probably irrelevant, as this really gets only used at the Start()
        tempGameEvent.date = GameTime.daysSinceStart;
        tempGameEvent.hour = GameTime.hourOfTheDay;
        
        if (executeToday)
        {
            
            
            if (tempGameEvent.hour >= 24)
            {
                tempGameEvent.hour = sleepHandler.wakeUpTime+1;
                tempGameEvent.date = tempGameEvent.date + 1;
            }
        }
        else
        {
            
            tempGameEvent.date = tempGameEvent.date + 1;
        }
        

        tempGO.transform.SetParent(transform);

        AddEventToQueue(tempGameEvent);
    }

    public static GameEventHandler CheckForExecutableEvent(int date, int hour, bool executeEvent)
    {
        
        foreach (GameEventHandler gameEvent in eventQueue)
        {
            //Debug.Log("date: " + date + ", hour: " + hour + ", gameEvent.date " + gameEvent.date + ", gameEvent.hour " + gameEvent.hour);
            if ((gameEvent.date==date)&&(gameEvent.hour==hour))
            {
                if(executeEvent)
                gameEvent.ExecuteEvent();

                return gameEvent;
            }
        }
        return null;
    }

    public void DeleteCompletedEvents()
    {
        foreach (Transform gameEvent in transform)
        {
            if (gameEvent.GetComponent<GameEventHandler>().date < GameTime.daysSinceStart)
                Destroy(gameEvent.gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
