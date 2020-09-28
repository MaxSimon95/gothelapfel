using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipHandler : MonoBehaviour
{
    public static string tooltipText;
    public static GameObject tooltip;
    // Start is called before the first frame update
    void Start()
    {
        tooltip = gameObject;
        tooltipText = tooltip.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text;
        //transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
