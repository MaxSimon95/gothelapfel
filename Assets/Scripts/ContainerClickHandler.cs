using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ContainerClickHandler : MonoBehaviour, IPointerClickHandler,
                                  IPointerDownHandler, IPointerEnterHandler,
                                  IPointerUpHandler, IPointerExitHandler
{
    public Canvas containerCanvas;

    void Start()
    {

        Camera.main.gameObject.AddComponent<Physics2DRaycaster>();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        containerCanvas.GetComponent<CanvasContainerHandler>().OpenContainerView();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.color(gameObject, new Color(1f, 0.94f, 0.43f, 1f), 0.1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.color(gameObject, Color.white, 0.1f);
    }

}