using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferAmountManagement : MonoBehaviour
{
    public static int transferAmount = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("+"))
        {
            RaiseIngredientTransferAmount();
        }
    }

    public void RaiseIngredientTransferAmount()
    {
        switch(transferAmount)
        {
            case 1:
                transferAmount = 10;
                break;
            case 10:
                transferAmount = 100;
                break;
            case 100:
                transferAmount = 100;
                break;
        }
    }

    public void LowerIngredientTransferAmount()
    {
        switch (transferAmount)
        {
            case 1:
                transferAmount = 1;
                break;
            case 10:
                transferAmount = 1;
                break;
            case 100:
                transferAmount = 10;
                break;
        }
    }
}
