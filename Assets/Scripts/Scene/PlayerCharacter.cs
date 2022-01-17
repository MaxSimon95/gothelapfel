using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float movementSpeed;
    public GameObject currentRoom;
    public Sprite[] sprites;

    enum Direction { SW, S, SE, E, NE, N, NW, W};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // vertical and horizontal movement

        if ((Input.GetKey("w")) && !((Input.GetKey("a")|| Input.GetKey("s"))|| Input.GetKey("d")))
        {
            CharacterMovement(Direction.N, 0, movementSpeed * 1.0f);
           
        }

        if ((Input.GetKey("s")) && !((Input.GetKey("w") || Input.GetKey("a")) || Input.GetKey("d")))
        {

            CharacterMovement(Direction.S, 0, movementSpeed * -1.0f);
        }

        if ((Input.GetKey("a")) && !((Input.GetKey("w") || Input.GetKey("s")) || Input.GetKey("d")))
        {
            CharacterMovement(Direction.W, movementSpeed * -1.0f,0);

        }

        if ((Input.GetKey("d")) && !((Input.GetKey("w") || Input.GetKey("a")) || Input.GetKey("s")))
        {

            CharacterMovement(Direction.E, movementSpeed * 1.0f, 0);
        }

        // isometric movement

        if ((Input.GetKey("w") && Input.GetKey("a")) && !( Input.GetKey("s") || Input.GetKey("d")))
        {
            CharacterMovement(Direction.NW, movementSpeed * -1.0f, movementSpeed * 0.47f);
        }

        if ((Input.GetKey("w") && Input.GetKey("d")) && !(Input.GetKey("s") || Input.GetKey("a")))
        {
            CharacterMovement(Direction.NE, movementSpeed * 1.0f, movementSpeed * 0.47f);
        }

        if ((Input.GetKey("s") && Input.GetKey("a")) && !(Input.GetKey("w") || Input.GetKey("d")))
        {
            CharacterMovement(Direction.SW, movementSpeed * -1.0f, movementSpeed * -0.47f);
        }

        if ((Input.GetKey("s") && Input.GetKey("d")) && !(Input.GetKey("w") || Input.GetKey("a")))
        {
            CharacterMovement(Direction.SE, movementSpeed * 1.0f, movementSpeed * -0.47f);
        }

        GetComponent<RenderOrderAdjustment>().AdjustRenderOrder();

    }

    /*
     * 
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
     */

    void CharacterMovement(Direction direction, float distanceX, float distanceY)
    {
        switch(direction)
        {
            case Direction.SW:

                GetComponent<SpriteRenderer>().sprite = sprites[0];
                break;
            case Direction.S:
                GetComponent<SpriteRenderer>().sprite = sprites[1];
                break;
            case Direction.SE:
                GetComponent<SpriteRenderer>().sprite = sprites[2];
                break;
            case Direction.E:
                GetComponent<SpriteRenderer>().sprite = sprites[3];
                break;
            case Direction.NE:
                GetComponent<SpriteRenderer>().sprite = sprites[4];
                break;
            case Direction.N:
                GetComponent<SpriteRenderer>().sprite = sprites[5];
                break;
            case Direction.NW:
                GetComponent<SpriteRenderer>().sprite = sprites[6];
                break;
            case Direction.W:
                GetComponent<SpriteRenderer>().sprite = sprites[7];
                break;
        }

        this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(distanceX, distanceY, 0);
    }

    void UpdateSpriteDirection()
    {

    }

}
