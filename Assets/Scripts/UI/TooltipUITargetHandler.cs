using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipUITargetHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string tooltipText;
    
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        TooltipHandler.tooltip.transform.position = transform.position + new Vector3(20, -20, 0); ;
        TooltipHandler.tooltip.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = tooltipText;
        TooltipHandler.RenderAgain();
        TooltipHandler.tooltip.transform.localScale = new Vector3(1, 1, 1);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipHandler.tooltip.transform.localScale = new Vector3(0, 0, 0);
        
    }

}
