using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float movementSpeed;
    public GameObject currentRoom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // vertical and horizontal movement

        if ((Input.GetKey("w")) && !((Input.GetKey("a")|| Input.GetKey("s"))|| Input.GetKey("d")))
        {


            Debug.Log(this.gameObject.transform.position);
            this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0, movementSpeed * 1.0f * Time.deltaTime, 0);
        }

        if ((Input.GetKey("s")) && !((Input.GetKey("w") || Input.GetKey("a")) || Input.GetKey("d")))
        {

            Debug.Log(this.gameObject.transform.position);
            this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0, movementSpeed * -1.0f * Time.deltaTime, 0);
        }

        if ((Input.GetKey("a")) && !((Input.GetKey("w") || Input.GetKey("s")) || Input.GetKey("d")))
        {

            Debug.Log(this.gameObject.transform.position);
            this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(movementSpeed * -1.0f * Time.deltaTime, 0, 0);
        }

        if ((Input.GetKey("d")) && !((Input.GetKey("w") || Input.GetKey("a")) || Input.GetKey("s")))
        {

            Debug.Log(this.gameObject.transform.position);
            this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(movementSpeed * 1.0f * Time.deltaTime, 0, 0);
        }

        // isometric movement

        if ((Input.GetKey("w") && Input.GetKey("a")) && !( Input.GetKey("s") || Input.GetKey("d")))
        {
            Debug.Log(this.gameObject.transform.position);
            this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(movementSpeed * -1.0f * Time.deltaTime, movementSpeed * 0.47f * Time.deltaTime, 0);
        }

        if ((Input.GetKey("w") && Input.GetKey("d")) && !(Input.GetKey("s") || Input.GetKey("a")))
        {
            Debug.Log(this.gameObject.transform.position);
            this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(movementSpeed * 1.0f * Time.deltaTime, movementSpeed * 0.47f * Time.deltaTime, 0);
        }

        if ((Input.GetKey("s") && Input.GetKey("a")) && !(Input.GetKey("w") || Input.GetKey("d")))
        {
            Debug.Log(this.gameObject.transform.position);
            this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(movementSpeed * -1.0f * Time.deltaTime, movementSpeed * -0.47f * Time.deltaTime, 0);
        }

        if ((Input.GetKey("s") && Input.GetKey("d")) && !(Input.GetKey("w") || Input.GetKey("a")))
        {
            Debug.Log(this.gameObject.transform.position);
            this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(movementSpeed * 1.0f * Time.deltaTime, movementSpeed * -0.47f * Time.deltaTime, 0);
        }

        GetComponent<RenderOrderAdjustment>().AdjustRenderOrder();

    }
}
