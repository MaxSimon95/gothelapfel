using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CauldronClickHandler : MonoBehaviour, IPointerClickHandler,
                                  IPointerDownHandler, IPointerEnterHandler,
                                  IPointerUpHandler, IPointerExitHandler
{

    void Start()
    {
        //Attach Physics2DRaycaster to the Camera
        Camera.main.gameObject.AddComponent<Physics2DRaycaster>();

        addEventSystem();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Mouse Clicked!");
        GameObject.Find("CanvasCauldronView").GetComponent<CanvasCauldronViewUIHandler>().OpenCauldronView();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Mouse Down!");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.color(gameObject, new Color(1f, 0.94f, 0.43f, 1f), 0.1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Mouse Up!");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.color(gameObject, Color.white, 0.1f);
    }

    //Add Event System to the Camera
    void addEventSystem()
    {
        GameObject eventSystem = null;
        GameObject tempObj = GameObject.Find("EventSystem");
        if (tempObj == null)
        {
            eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }
        else
        {
            if ((tempObj.GetComponent<EventSystem>()) == null)
            {
                tempObj.AddComponent<EventSystem>();
            }

            if ((tempObj.GetComponent<StandaloneInputModule>()) == null)
            {
                tempObj.AddComponent<StandaloneInputModule>();
            }
        }
    }

}