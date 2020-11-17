using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TooltipHandler : MonoBehaviour
{
    public static string tooltipText;
    public static GameObject tooltip;

    void Start()
    {
        tooltip = gameObject;
        tooltipText = tooltip.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text;


    }

    public static void RenderAgain()
    {


        tooltip.GetComponent<RectTransform>().sizeDelta = new Vector2(tooltip.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta.x + 20, 50);

    }

    void Update()
    {
        
    }
}
