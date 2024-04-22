using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;


public class GameTime : MonoBehaviour
{
    
    public enum season
    {
        SPRING,
        SUMMER,
        AUTUMN,
        WINTER
    }
    public static season currentSeason = season.SPRING;
    public static int seasonLengthSpring = 0;
    public static int seasonLengthSummer = 0;
    public static int seasonLengthAutumn = 0;
    public static int seasonLengthWinter = 0;

    public static int daysSinceStart = 0;
    public static int daysSinceYearStart = 0;
    public static int yearsSinceStart;
    //public static float secondinhosSinceStart = 0;
    public static int hourOfTheDay;
    public int hourOfTheDayOffset;

    public static int yearLengthInDays = 60;
    public static int dayLengthInHours = 24;
    public static float hourLengthInSecondinhos = 60;
    public float secondinhoLengthInSeconds; // 0.004 standard für fast tiem debug = 1f;//0.01f;//0.625f/1000;

    System.Random randomizer = new System.Random();

    public static UnityEvent eventDayChange = new UnityEvent();
    public ColorScreenHandler colorScreenHandler;

    public static bool timeIsStopped = false;

    public SleepHandler sleepHandler;

    public bool jumpToHourOfTheDayCurrentlyActive;

    void Start()
    {
        hourOfTheDay = hourOfTheDayOffset;
        StartCoroutine(GameTimeLoop());
    }

   

    IEnumerator GameTimeLoop()
    {
        float secondinhosSinceLastHourUpdate=0;

        CalculateSeasonsLength();
        //float timeSinceLastSecondinhoCalculation = 0;
        float currentTime;
        while (true)
        {
            
            currentTime = Time.time;
            if(!timeIsStopped)
            {
                //Debug.Log("Update Time UI");
                //secondinhosSinceStart += (currentTime - timeSinceLastSecondinhoCalculation) / secondinhoLengthInSeconds;
                secondinhosSinceLastHourUpdate++;
                if(secondinhosSinceLastHourUpdate>= hourLengthInSecondinhos)
                {
                    secondinhosSinceLastHourUpdate = secondinhosSinceLastHourUpdate - hourLengthInSecondinhos;
                    IncrementHourOfTheDay(true);
                }
                
            }
            //Debug.Log(secondinhosSinceLastHourUpdate);

            //timeSinceLastSecondinhoCalculation = currentTime;
            yield return new WaitForSeconds(secondinhoLengthInSeconds);
            
        }

    }

    void IncrementHourOfTheDay(bool colorChangeAnimationOverTime)
    {
        /*if(hourOfTheDayOffset > dayLengthInHours)
        {
            hourOfTheDayOffset = hourOfTheDayOffset - dayLengthInHours;
        }*/
        hourOfTheDay++;
        /*
        float tempHour = hourOfTheDayOffset + (secondinhosSinceStart - daysSinceStart * dayLengthInHours * hourLengthInSecondinhos) / hourLengthInSecondinhos;
        */
        if (hourOfTheDay >= dayLengthInHours)
        {
            hourOfTheDay -= dayLengthInHours;
            IncrementDay(1); 
        }
        //hourOfTheDay = tempHour;

        colorScreenHandler.UpdateColorScreen(colorChangeAnimationOverTime);
        //Debug.Log(hourOfTheDay);

        if((hourOfTheDay == sleepHandler.wakeUpTime)&&(!jumpToHourOfTheDayCurrentlyActive))
        {
            sleepHandler.Sleep(false);
        }

        GameEvents.CheckForExecutableEvent(daysSinceStart, hourOfTheDay, true);

    }

    public void IncrementDay(int dayAddition)
    {
        //GameObject.Find("JOB_PH3").GetComponent<JobHandler>().Activate();
        if (dayAddition < 0)
        {

            Debug.LogError("dayAddition MAY CURRENTLY NOT BE LESS THAN ZERO, FOOLS ");

        }

        daysSinceStart += dayAddition;
        daysSinceYearStart += dayAddition;

        UpdateSeason();

        if (daysSinceYearStart >= yearLengthInDays)
        {
            daysSinceYearStart -= yearLengthInDays;
            UpdateYear(1);
        }

        eventDayChange.Invoke();


    }

    void UpdateYear(int yearAddition)
    {
        yearsSinceStart ++;
        CalculateSeasonsLength();
    }

    void CalculateSeasonsLength()
    {
        int tempRemainingDays = yearLengthInDays;
        seasonLengthSpring = yearLengthInDays / 4 + randomizer.Next(-5, 5);
        tempRemainingDays -= seasonLengthSpring;

        seasonLengthSummer= yearLengthInDays / 4 + randomizer.Next(-5, 5);
        tempRemainingDays -= seasonLengthSummer;

        seasonLengthAutumn = yearLengthInDays / 4 + randomizer.Next(-5, 5);
        tempRemainingDays -= seasonLengthAutumn;

        seasonLengthWinter = tempRemainingDays;

    }

    void UpdateSeason()
    {
        if(daysSinceYearStart < seasonLengthSpring)
        {
            currentSeason = season.SPRING;
           
        }
        if ((daysSinceYearStart >= seasonLengthSpring) && (daysSinceYearStart < seasonLengthSpring + seasonLengthSummer ))
        {
            currentSeason = season.SUMMER;
            
        }
        if ((daysSinceYearStart >= seasonLengthSpring + seasonLengthSummer) && (daysSinceYearStart < seasonLengthSpring + seasonLengthSummer + seasonLengthAutumn ))
        {
            currentSeason = season.AUTUMN;
            
        }
        if (daysSinceYearStart >= seasonLengthSpring + seasonLengthSummer + seasonLengthAutumn)
        {
            currentSeason = season.WINTER;
            Debug.Log(currentSeason);
        }
    }

    public void JumpToHourOfTheDay(int targetHour)
    {
        /*
        // no day change if we dont need to change the day, but day change if the target time is on the next day
        if(targetHour > hourOfTheDay)
        {
            Debug.Log("Move forward same day"); //+ targetHour + ", hour of the day: " + hourOfTheDay  + ", hourOfTheDayOffset Addition: " + (hourOfTheDay - targetHour));
            //hourOfTheDayOffset += (targetHour - hourOfTheDay);
        }
        else
        {
            Debug.Log("Move forward NEXT day");
            //Debug.Log("Move forward new day, target hour: " + targetHour + ", hour of the day: " + hourOfTheDay + ", hourOfTheDayOffset Addition: " + (targetHour + dayLengthInHours - hourOfTheDay));
            //hourOfTheDayOffset += 10; // (targetHour + dayLengthInHours - hourOfTheDay);
            IncrementDay(1);
        }


        hourOfTheDay = targetHour;
        */
        jumpToHourOfTheDayCurrentlyActive = true;
        while (hourOfTheDay != targetHour)
        {
            Debug.Log("hourIncrement starting from " + hourOfTheDay);
            IncrementHourOfTheDay(false);
        }
        jumpToHourOfTheDayCurrentlyActive = false;
        colorScreenHandler.UpdateColorScreen(false);
        //UpdateHourOfTheDay(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
