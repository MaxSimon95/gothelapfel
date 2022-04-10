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

        //containerCanvas.GetComponent<CanvasContainerHandler>().OpenContainerView();

        /*if (!GameObject.Find("Grid").GetComponent<Grid>().TryGoToWorldpos(new Vector2 (transform.position.x, transform.position.y)))
        {

        } */

        Vector3 targetVector = new Vector3(0,0,0);

        if (transform.GetChild(0).gameObject.GetComponent<PolygonCollider2D>() != null)
        {

        }
            targetVector = transform.GetChild(0).gameObject.GetComponent<PolygonCollider2D>().bounds.center;

        Node reachableTargetNode = GameObject.Find("Grid").GetComponent<Grid>().FindNearestAccessibleNodeFromWorldCoordinates(new Vector2(targetVector.x, targetVector.y));

        if (reachableTargetNode != null)
        {
            GameObject.Find("PlayerCharacter").GetComponent<PlayerCharacter>().currentMovementTargetGO = gameObject;
            GameObject.Find("Grid").GetComponent<Grid>().TryGoToWorldpos(GameObject.Find("Grid").GetComponent<Grid>().GridToWorld(new Point(reachableTargetNode.X, reachableTargetNode.Y)));
        }

        
    }


    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.color(gameObject, new Color(1f, 0.94f, 0.13f, 1f), 0.1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.color(gameObject, GetComponent<RenderOrderAdjustment>().room.currentRoomColor, 0.1f);
        
    }

    



}