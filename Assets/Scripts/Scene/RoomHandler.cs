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
    public Vector2 cameraCenter;
    public float cameraZoom;
    private RoomManagement roomManagement;

    public float temperature;



    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Camera>();
        GetComponent<SpriteRenderer>().sprite = roomFull;
        GetComponent<SpriteRenderer>().sortingOrder = roomSortingOrderBase + roomIndex;


        roomManagement = GameObject.Find("RoomManagement").GetComponent<RoomManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnter()
    {
        Debug.Log("Entered " + roomName);
        GameObject.FindGameObjectsWithTag("PlayerCharacter")[0].GetComponent<PlayerCharacter>().currentRoom = this;
        LeanTween.move(mainCamera.gameObject, new Vector3(cameraCenter.x, cameraCenter.y, mainCamera.transform.position.z), 2f).setEaseInOutCubic();
        
        foreach (RoomHandler roomInList in roomManagement.rooms)
        {
            if(roomInList.roomIndex > roomIndex)
            {
                roomInList.GetComponent<SpriteRenderer>().sprite = roomInList.roomFloor;
            }
            else
            {
                roomInList.GetComponent<SpriteRenderer>().sprite = roomInList.roomFull;
            }
        }
    }
}
