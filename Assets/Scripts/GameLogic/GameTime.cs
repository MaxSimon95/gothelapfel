using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameTime : MonoBehaviour
{
    
    public enum season
    {
        spring,
        summer,
        autumn,
        winter
    }
    public static season currentSeason = season.spring;
    public static int seasonLengthSpring = 0;
    public static int seasonLengthSummer = 0;
    public static int seasonLengthAutumn = 0;
    public static int seasonLengthWinter = 0;

    public static int daysSinceStart = 0;
    public static int daysSinceYearStart = 0;
    public static int yearsSinceStart;
    public static float secondinhosSinceStart = 0;
    public static float hourOfTheDay = 0;

    public static int yearLengthInDays = 60;
    public static int dayLengthInHours = 24;
    public static float hourLengthInSecondinhos = 60;
    public static float secondinhoLengthInSeconds = 1;//0.625f/1000;

    System.Random randomizer = new System.Random();

    IEnumerator Start()
    {
        CalculateSeasonsLength();
        float timeSinceLastSecondinhoCalculation = 0;
        float currentTime;
        while (true)
        {
            currentTime = Time.time;
            secondinhosSinceStart += (currentTime - timeSinceLastSecondinhoCalculation) / secondinhoLengthInSeconds;
            UpdateHourOfTheDay();
            timeSinceLastSecondinhoCalculation = currentTime;
            //Debug.Log(secondinhosSinceStart);
            yield return new WaitForSeconds(secondinhoLengthInSeconds);
        }

    }

    void UpdateHourOfTheDay()
    {
        float tempHour = (secondinhosSinceStart - daysSinceStart * dayLengthInHours * hourLengthInSecondinhos) / hourLengthInSecondinhos;
        //float tempHour = secondinhosSinceStart / hourLengthInSecondinhos;
        //Debug.Log(tempHour);
        if (tempHour > dayLengthInHours)
        {
            tempHour -= dayLengthInHours;
            UpdateDay(1); 
        }
        hourOfTheDay = tempHour;
        //Debug.Log(hourOfTheDay);
    }

    void UpdateDay(int dayAddition)
    {
        if(dayAddition < 0)
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

        Debug.Log("Spring: " + seasonLengthSpring + "; Summer: " + seasonLengthSummer + "; Autumn: " + seasonLengthAutumn + "; Winter: " + seasonLengthWinter);
    }

    void UpdateSeason()
    {
        Debug.Log("update season");
        if(daysSinceYearStart < seasonLengthSpring)
        {
            currentSeason = season.spring;
           
        }
        if ((daysSinceYearStart >= seasonLengthSpring) && (daysSinceYearStart < seasonLengthSpring + seasonLengthSummer ))
        {
            currentSeason = season.summer;
            
        }
        if ((daysSinceYearStart >= seasonLengthSpring + seasonLengthSummer) && (daysSinceYearStart < seasonLengthSpring + seasonLengthSummer + seasonLengthAutumn ))
        {
            currentSeason = season.autumn;
            
        }
        if (daysSinceYearStart >= seasonLengthSpring + seasonLengthSummer + seasonLengthAutumn)
        {
            currentSeason = season.winter;
            Debug.Log(currentSeason);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
