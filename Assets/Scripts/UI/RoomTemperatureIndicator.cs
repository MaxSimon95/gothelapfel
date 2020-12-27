using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemperatureIndicator : MonoBehaviour
{
    public GameObject playerCharacter;
    // Start is called before the first frame update
    IEnumerator Start()
    {

        while (true)
        {
            playerCharacter = GameObject.FindGameObjectsWithTag("PlayerCharacter")[0];
            gameObject.GetComponent<UnityEngine.UI.Text>().text = "Room Temperature: " + (int)playerCharacter.GetComponent<PlayerCharacter>().currentRoom.GetComponent<RoomHandler>().temperature + "°";
            yield return new WaitForSeconds(1);
        }
       
    }

    


    // Update is called once per frame
    void Update()
    {
        
    }
}
