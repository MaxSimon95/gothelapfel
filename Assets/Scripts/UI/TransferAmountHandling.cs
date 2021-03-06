﻿using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class TransferAmountHandling : MonoBehaviour
{
    public static int currentTransferAmount = 10;
    public AudioClip clickSound;
    private AudioSource source;


    void Start()
    {
        GameObject.Find("PanelTransferAmount").transform.localScale = new Vector3(0, 0, 0);
        AdjustLabelTransferAmount();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            RaiseIngredientTransferAmount();
        }

        if (Input.GetKeyDown("-"))
        {
            LowerIngredientTransferAmount();
        }
    }

    public void RaiseIngredientTransferAmount()
    {
        switch (currentTransferAmount)
        {
            case 1:
                currentTransferAmount = 5;
                break;
            case 5:
                currentTransferAmount = 10;
                break;
            case 10:
                currentTransferAmount = 50;
                break;
            case 50:
                currentTransferAmount = 100;
                break;
            case 100:
                currentTransferAmount = 100;
                break;
        }
        AdjustLabelTransferAmount();
        source = GetComponent<AudioSource>();
        source.PlayOneShot(clickSound, 1f);
    }

    public void LowerIngredientTransferAmount()
    {
        switch (currentTransferAmount)
        {
            case 1:
                currentTransferAmount = 1;
                break;
            case 5:
                currentTransferAmount = 1;
                break;
            case 10:
                currentTransferAmount = 5;
                break;
            case 50:
                currentTransferAmount = 10;
                break;
            case 100:
                currentTransferAmount = 50;
                break;
        }
        AdjustLabelTransferAmount();
        source = GetComponent<AudioSource>();
        source.PlayOneShot(clickSound, 1f);
    }

    public void setTransferAmount (int pTransferAmount)
    {
        currentTransferAmount = pTransferAmount;
        source = GetComponent<AudioSource>();
        source.PlayOneShot(clickSound, 1f);
        AdjustLabelTransferAmount();
    }

    private void AdjustLabelTransferAmount()
    {
        GameObject.Find("LabelTransferAmount").GetComponent<UnityEngine.UI.Text>().text = currentTransferAmount.ToString();
    }
    
}
