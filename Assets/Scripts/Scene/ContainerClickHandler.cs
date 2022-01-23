using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ContainerClickHandler : MonoBehaviour, IPointerClickHandler,
                                  IPointerDownHandler, IPointerEnterHandler,
                                  IPointerUpHandler, IPointerExitHandler 
{
    public string containerCanvasName;
    public Canvas containerCanvas;

    void Start()
    {
        containerCanvas = GameObject.Find(containerCanvasName).GetComponent<Canvas>(); ;
        //Camera.main.gameObject.AddComponent<Physics2DRaycaster>();

    }

    /*void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                //hit.collider.attachedRigidbody.AddForce(Vector2.up);
            }
        }
    }*/

    
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.Find("PlayerCharacter").GetComponent<PlayerCharacter>().RotateCharacterTowardsPoint(transform.position.x, transform.position.y) ;

        GameObject.Find("PlayerCharacter").GetComponent<PlayerCharacter>().currentMovementTargetGO = gameObject;

        //containerCanvas.GetComponent<CanvasContainerHandler>().OpenContainerView();

        if (!GameObject.Find("Grid").GetComponent<Grid>().TryGoToWorldpos(new Vector2 (transform.position.x, transform.position.y)))
        {

        }

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