﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockDayDisplay : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(DayDisplayLoop());
    }

    IEnumerator DayDisplayLoop()
    {

        while (true)
        {
            UpdateDayText();
            yield return new WaitForSeconds(1);
        }

    }

    void UpdateDayText()
    {
        gameObject.GetComponent<UnityEngine.UI.Text>().text = "Day: " + GameTime.daysSinceStart;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
