using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyHandler : MonoBehaviour
{
    public int moneyAtDayStart;
    public int currentMoney;
    public GameObject UI_textMoney;

    public int rent; 
    public List<int> moneyChangeAmountsToday;
    public List<string> moneyChangeLabelsToday;


    // Start is called before the first frame update
    void Start()
    {
        UpdateMoneyDisplay();
        UpdateMoneyAtDayStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMoneyAtDayStart()
    {
        moneyAtDayStart = currentMoney;
        ClearMoneyChanges();
        
    }

    public void UpdateMoneyDisplay()
    {
        Debug.Log("crrent muney " + currentMoney.ToString());
        UI_textMoney.GetComponent<UnityEngine.UI.Text>().text = currentMoney.ToString();
    }

    public void AddMoneyChange(string pLabel, int pAmount)
    {
        currentMoney = currentMoney + pAmount;
        UpdateMoneyDisplay();
        moneyChangeLabelsToday.Add(pLabel);
        moneyChangeAmountsToday.Add(pAmount);
    }

    public void ClearMoneyChanges()
    {
        moneyChangeLabelsToday.Clear();
        moneyChangeAmountsToday.Clear();
    }
}
