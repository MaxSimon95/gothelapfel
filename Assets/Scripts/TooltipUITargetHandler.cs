using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipUITargetHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string tooltipText;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(TooltipHandler.tooltip);

        TooltipHandler.tooltip.transform.position = transform.position + new Vector3(105, -20, 0); ;
        TooltipHandler.tooltip.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = tooltipText;
        TooltipHandler.tooltip.transform.localScale = new Vector3(1, 1, 1);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse is no longer on GameObject.");
        TooltipHandler.tooltip.transform.localScale = new Vector3(0, 0, 0);
        
    }


}
