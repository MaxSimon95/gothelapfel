using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobHandler : MonoBehaviour
{
    public string title;
    public int payment;

    public IngredientType requestedIngredientType;

    public int requestedAmount;

    public int startDays;
    public int remainingDays;
    public bool jobIsSaved;
    public int orderNumber;

    // Start is called before the first frame update
    void Awake()
    {
        remainingDays = startDays;
        orderNumber = calculateOrderNumber();
    }

    int calculateOrderNumber()
    {
        int tempNumber = 0;
        if (!jobIsSaved)
        {
            tempNumber += 999999;
        }

        tempNumber += remainingDays;

        return tempNumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
