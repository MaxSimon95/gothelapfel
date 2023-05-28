using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    public string roomName;
    public static int roomSortingOrderBase = -10000;
    public int roomIndex;
    public Sprite roomFull;
    public Sprite roomFloor;
    public GameObject cameraTarget;
    //public Vector2 cameraCenter;
    public float cameraZoom;
    private RoomManagement roomManagement;
    public List<GameObject> movementBlockers;

    public float temperature;

    public Color currentRoomColor;



    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Camera>();
        GetComponent<SpriteRenderer>().sprite = roomFull;
        GetComponent<SpriteRenderer>().sortingOrder = roomSortingOrderBase + roomIndex;


        roomManagement = GameObject.Find("RoomManagement").GetComponent<RoomManagement>();

        foreach (Transform child in transform)
        {
            if (child.tag == "MovementTargetBlocker")
                movementBlockers.Add(child.gameObject);
        }
    }

    private void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("PlayerCharacter")[0].GetComponent<PlayerCharacter>().currentRoom != this)
        {
            ToggleRoomLitUp(false);
        }
        else
        {
            ToggleRoomLitUp(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowWalls(bool wallsUp)
    {
        if (wallsUp)
        {
            GetComponent<SpriteRenderer>().sprite = roomFull;

            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent<WallElementHandler>() != null)
                    child.gameObject.GetComponent<SpriteRenderer>().sprite = child.gameObject.GetComponent<WallElementHandler>().wallUpSprite;
            }


        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = roomFloor;

            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent<WallElementHandler>() != null)
                    child.gameObject.GetComponent<SpriteRenderer>().sprite = child.gameObject.GetComponent<WallElementHandler>().wallDownSprite;
            }
        }
    }

    public void ToggleRoomLitUp(bool roomLitUp)
    {
        ToggleRoomLitUp(roomLitUp, false);
    }

    public void ToggleRoomLitUp(bool roomLitUp, bool withAnimation)
    {
        Color targetColor;

        // determine targetColor
        if(roomLitUp)
        {
            targetColor = new Color(1f, 1f, 1f);
        }
        else
        {
            targetColor = new Color(0.58f, 0.62f, 0.76f);
        }

        //color this room graphic

        currentRoomColor = targetColor;
        ApplyRoomColor(withAnimation);

    }

    public void ApplyRoomColor()
    {
        ApplyRoomColor(false);
    }

    public void ApplyRoomColor(bool withAnimation)
    {

        
        if(withAnimation)
        {
            if (GetComponent<SpriteRenderer>() != null)
                LeanTween.color(gameObject, currentRoomColor, 0.4f);

            // color graphic of components in this room
            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent<SpriteRenderer>() != null)
                    LeanTween.color(child.gameObject, currentRoomColor, 0.4f);
            }
        }
        else
        {
            if (GetComponent<SpriteRenderer>() != null)
                GetComponent<SpriteRenderer>().color = currentRoomColor;

            // color graphic of components in this room
            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent<SpriteRenderer>() != null)
                    child.gameObject.GetComponent<SpriteRenderer>().color = currentRoomColor;
            }
        }
        

    }

    void AdjustZoom()
    {
        mainCamera.orthographicSize = cameraZoom;
    }

    public void OnEnter(bool teleportPlayer)
    {
        Debug.Log("Entered " + roomName);
        GameObject.FindGameObjectsWithTag("PlayerCharacter")[0].GetComponent<PlayerCharacter>().currentRoom = this;

        LeanTween.cancel(mainCamera.gameObject);
        //AdjustZoom();
        

            LeanTween.value(mainCamera.gameObject, mainCamera.orthographicSize, cameraZoom, 2f).setOnUpdate((float val) => {
                //Debug.Log("tweened val:" + val);
                mainCamera.orthographicSize = val;
            }).setEaseInOutCubic();

        if (teleportPlayer)
        {
            LeanTween.move(mainCamera.gameObject, new Vector3(cameraTarget.transform.localPosition.x + cameraTarget.transform.parent.localPosition.x, cameraTarget.transform.localPosition.y + cameraTarget.transform.parent.localPosition.y, mainCamera.transform.position.z), 0f);
        }
        else
        {
            
            LeanTween.move(mainCamera.gameObject, new Vector3(cameraTarget.transform.localPosition.x + cameraTarget.transform.parent.localPosition.x, cameraTarget.transform.localPosition.y + cameraTarget.transform.parent.localPosition.y, mainCamera.transform.position.z), 2f).setEaseInOutCubic();
        }

        
        
        foreach (RoomHandler roomInList in roomManagement.rooms)
        {
            if(roomInList.roomIndex > roomIndex)
            {
                roomInList.ShowWalls(false);
                
            }
            else
            {
                roomInList.ShowWalls(true);
                
            }
            roomInList.ToggleRoomLitUp(false,true);
        }

        ToggleRoomLitUp(true,true);
    }
}
