using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        //Debug.Log("Door Handler Touched");
        if(GameObject.FindGameObjectsWithTag("PlayerCharacter")[0].GetComponent<PlayerCharacter>().currentRoom != gameObject.transform.parent.gameObject.GetComponent<RoomHandler>())
        {
            gameObject.transform.parent.gameObject.GetComponent<RoomHandler>().OnEnter();
        }
    }
}
