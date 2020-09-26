using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSecondaryTransfer : MonoBehaviour
{
    
    public int buttonTransferAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonPress()
    {
        Debug.Log("Button press secondary transferamount for " + buttonTransferAmount);
        GameObject.Find("PanelTransferAmount").GetComponent<TransferAmountHandling>().setTransferAmount(buttonTransferAmount);


        GameObject.Find("PanelSecondaryTransferAmountSelection").transform.localScale = new Vector3(0, 0, 0);
    }
}
