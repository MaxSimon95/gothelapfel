using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using LayoutRebuilder;

public class TooltipHandler : MonoBehaviour
{
    public static string tooltipText;
    public static GameObject tooltip;
   // public HorizontalLayoutGroup horizLayoutGroup;
    // Start is called before the first frame update
    void Start()
    {
        tooltip = gameObject;
        tooltipText = tooltip.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text;
        //horizLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        //LayoutRebuilder.ForceRebuildLayoutImmediate();
        //transform.localScale = new Vector3(0, 0, 0);

        //horizLayoutGroup



        // LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent);

    }

    public static void RenderAgain()
    {
        //Debug.Log("RenderAgain Called");
        /*  horizLayoutGroup.CalculateLayoutInputHorizontal();
          horizLayoutGroup.CalculateLayoutInputVertical();
          horizLayoutGroup.SetLayoutHorizontal();
          horizLayoutGroup.SetLayoutVertical(); */

        tooltip.GetComponent<RectTransform>().sizeDelta = new Vector2(tooltip.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta.x + 20, 50);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
