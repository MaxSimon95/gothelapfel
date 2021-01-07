using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    public int yearSinceStart;
    public enum Season
    {
        spring,
        summer,
        autumn,
        winter
    }
    public int daysSinceStart;
    public int daysSinceYearStart;
    public int SecondinhosSinceStart;
    public int hourOfTheDay;

    public int yearLengthInDays;
    public int dayLengthInHours;
    public float hourLengthInSecondinhos;
    public float secondinhoLengthInSeconds;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
