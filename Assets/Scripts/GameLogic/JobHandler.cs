using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobHandler : MonoBehaviour
{
    public string title;
    public int payment;

    public IngredientType requestedIngredientType;

    public int requestedAmount;

    public int startPointInTime;
    public int startDays;
    public int remainingDays;
    public bool jobIsSaved;
    public int orderNumber;

    public enum state { UPCOMING, ACTIVE, COMPLETED, EXPIRED}
    public state currentState;
    // Start is called before the first frame update
    void Awake()
    {
        remainingDays = startDays;
        orderNumber = CalculateOrderNumber();
    }

    int CalculateOrderNumber()
    {
        int tempNumber = 0;
        if (!jobIsSaved)
        {
            tempNumber += 999999;
        }

        tempNumber += remainingDays;

        return tempNumber;
    }

    public void Expire()
    {
        currentState = JobHandler.state.EXPIRED;
        jobIsSaved = false;

    }

    public void Complete()
    {
        currentState = JobHandler.state.COMPLETED;
        jobIsSaved = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
