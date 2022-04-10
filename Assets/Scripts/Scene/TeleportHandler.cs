using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TeleportHandler : MonoBehaviour, IPointerClickHandler,
                                  IPointerEnterHandler,
                                  IPointerExitHandler
{
    public GameObject target;
    private RoomHandler targetRoom;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.Find("PlayerCharacter").GetComponent<PlayerCharacter>().RotateCharacterTowardsPoint(transform.position.x, transform.position.y);

        //containerCanvas.GetComponent<CanvasContainerHandler>().OpenContainerView();

        /*if (!GameObject.Find("Grid").GetComponent<Grid>().TryGoToWorldpos(new Vector2 (transform.position.x, transform.position.y)))
        {

        } */

        Vector3 targetVector = new Vector3(0, 0, 0);

        if (transform.GetChild(0).gameObject.GetComponent<PolygonCollider2D>() != null)
        {
            Debug.Log("Polycollider found");
        }
        targetVector = transform.GetChild(0).gameObject.GetComponent<PolygonCollider2D>().bounds.center;
        Debug.Log(targetVector);

        Node reachableTargetNode = GameObject.Find("Grid").GetComponent<Grid>().FindNearestAccessibleNodeFromWorldCoordinates(new Vector2(targetVector.x, targetVector.y));

        if (reachableTargetNode != null)
        {
            GameObject.Find("PlayerCharacter").GetComponent<PlayerCharacter>().currentMovementTargetGO = gameObject;
            GameObject.Find("Grid").GetComponent<Grid>().TryGoToWorldpos(GameObject.Find("Grid").GetComponent<Grid>().GridToWorld(new Point(reachableTargetNode.X, reachableTargetNode.Y)));
        }


    }

    public void Teleport()
    {
        Debug.Log("Teleport");
        GameObject.FindGameObjectsWithTag("PlayerCharacter")[0].transform.localPosition = target.transform.localPosition + targetRoom.transform.localPosition;
        targetRoom.OnEnter(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.color(gameObject, new Color(1f, 0.94f, 0.13f, 1f), 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.color(gameObject, GetComponent<RenderOrderAdjustment>().room.currentRoomColor, 0.1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        targetRoom = target.transform.parent.gameObject.GetComponent<RoomHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
