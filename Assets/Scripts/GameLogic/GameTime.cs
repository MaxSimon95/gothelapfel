using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    public static int yearSinceStart;
    public enum season
    {
        spring,
        summer,
        autumn,
        winter
    }
    public static season currentSeason = season.spring;
    public static int daysSinceStart =0;
    public static int daysSinceYearStart = 0;
    public static int SecondinhosSinceStart = 0;
    public static float hourOfTheDay = 0;

    public static int yearLengthInDays = 60;
    public static int dayLengthInHours = 24;
    public static float hourLengthInSecondinhos = 60;
    public static float secondinhoLengthInSeconds = 0.625f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
