using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyHandler : MonoBehaviour
{
    public int currentMoney;
    public GameObject UI_textMoney;


    // Start is called before the first frame update
    void Start()
    {
        UpdateMoneyDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMoneyDisplay()
    {
        UI_textMoney.GetComponent<UnityEngine.UI.Text>().text = currentMoney.ToString();
    }
}
