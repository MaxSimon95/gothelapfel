using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float movementSpeed;
    public RoomHandler currentRoom;
    public Sprite[] sprites;

    public Grid grid;

    public bool currentlyMovingTowardsPoint = false;
    public GameObject currentMovementTargetGO = null;
    public Vector2 currentDestinationPoint;

    enum Direction { SW, S, SE, E, NE, N, NW, W};

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    float CheckDistanceToGO(GameObject go)
    {
        //  Debug.Log("go: " + go.transform.position.x + " " + go.transform.position.y);
        //  Debug.Log("pc: " + transform.position.x + " " + transform.position.y);




        // float distance = Mathf.Sqrt(Mathf.Pow((go.transform.position.x - transform.position.x), 2) + Mathf.Pow((go.transform.position.y - transform.position.y), 2));

        float distance = Mathf.Sqrt(Mathf.Pow((go.transform.GetChild(0).GetComponent<PolygonCollider2D>().bounds.center.x - transform.position.x), 2) + Mathf.Pow((go.transform.GetChild(0).GetComponent<PolygonCollider2D>().bounds.center.y - transform.position.y), 2));

        return distance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (currentMovementTargetGO != null)
        {
            if (CheckDistanceToGO(currentMovementTargetGO) < 1)
            {
                //Debug.Log("Movement target reached");
                RotateCharacterTowardsPoint(currentMovementTargetGO.transform.position.x, currentMovementTargetGO.transform.position.y);

                if(currentMovementTargetGO.GetComponent<ContainerClickHandler>() != null)
                {
                    currentMovementTargetGO.GetComponent<ContainerClickHandler>().containerCanvas.GetComponent<CanvasContainerHandler>().OpenContainerView();
                }
                else if (currentMovementTargetGO.GetComponent<TeleportHandler>() != null)
                {
                    currentMovementTargetGO.GetComponent<TeleportHandler>().Teleport();
                }

                currentMovementTargetGO = null;
                currentlyMovingTowardsPoint = false;
            }
        }

        if (currentlyMovingTowardsPoint)
        {
            MovingTowardsPoint();
        }

        if(Input.GetKey("d") || Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s"))
        {
            KeyInputMovement();
        }
        

        GetComponent<RenderOrderAdjustment>().AdjustRenderOrder();

    }

    void KeyInputMovement()
    {
        currentlyMovingTowardsPoint = false;
        currentMovementTargetGO = null;
        //currentMovementTargetGO = null;

        // vertical and horizontal movement through key input

        if ((Input.GetKey("w")) && !((Input.GetKey("a") || Input.GetKey("s")) || Input.GetKey("d")))
        {
            //CharacterMovement(Direction.N, 0, movementSpeed * 1.0f);
            CharacterMovement(new Vector2(0, 1));

        }

        if ((Input.GetKey("s")) && !((Input.GetKey("w") || Input.GetKey("a")) || Input.GetKey("d")))
        {

            //CharacterMovement(Direction.S, 0, movementSpeed * -1.0f);
            CharacterMovement(new Vector2(0, -1));
        }

        if ((Input.GetKey("a")) && !((Input.GetKey("w") || Input.GetKey("s")) || Input.GetKey("d")))
        {
            //CharacterMovement(Direction.W, movementSpeed * -1.0f,0);
            CharacterMovement(new Vector2(-1, 0));

        }

        if ((Input.GetKey("d")) && !((Input.GetKey("w") || Input.GetKey("a")) || Input.GetKey("s")))
        {

            //CharacterMovement(Direction.E, movementSpeed * 1.0f, 0);
            CharacterMovement(new Vector2(1, 0));
        }

        // isometric movement

        if ((Input.GetKey("w") && Input.GetKey("a")) && !(Input.GetKey("s") || Input.GetKey("d")))
        {
            //CharacterMovement(Direction.NW, movementSpeed * -1.0f, movementSpeed * 0.47f);
            CharacterMovement(new Vector2(-1, 0.47f));
        }

        if ((Input.GetKey("w") && Input.GetKey("d")) && !(Input.GetKey("s") || Input.GetKey("a")))
        {
            //CharacterMovement(Direction.NE, movementSpeed * 1.0f, movementSpeed * 0.47f);
            CharacterMovement(new Vector2(1, 0.47f));
        }

        if ((Input.GetKey("s") && Input.GetKey("a")) && !(Input.GetKey("w") || Input.GetKey("d")))
        {
            //CharacterMovement(Direction.SW, movementSpeed * -1.0f, movementSpeed * -0.47f);
            CharacterMovement(new Vector2(-1, -0.47f));
        }

        if ((Input.GetKey("s") && Input.GetKey("d")) && !(Input.GetKey("w") || Input.GetKey("a")))
        {
            //CharacterMovement(Direction.SE, movementSpeed * 1.0f, movementSpeed * -0.47f);
            CharacterMovement(new Vector2(1, -0.47f));
        }
    }



    void CharacterMovement(Vector2 direction)
    {
        direction.Normalize();
        float distanceX = direction.x * movementSpeed;
        float distanceY = direction.y * movementSpeed;
        //RotateCharacterTowardsDirection(facing);
        RotateCharacterTowardsPoint(transform.position.x + direction.x, transform.position.y + direction.y);

        this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(distanceX, distanceY, 0);
    }

    public void StartMoveToPoint(Vector2 destination)
    {
        currentlyMovingTowardsPoint = true;
        currentDestinationPoint = destination;
    }
    void MovingTowardsPoint()
    {
        Point gridTargetPoint = grid.WorldToGrid(currentDestinationPoint);
        Point gridPCPoint = grid.WorldToGrid(new Vector2(transform.position.x, transform.position.y));

        //Debug.Log("target: "+ gridTargetPoint.X + " " + gridTargetPoint.Y + "; current: " + gridPCPoint.X + " " + gridPCPoint.Y);

        if ((gridTargetPoint.X == gridPCPoint.X)&& (gridTargetPoint.Y == gridPCPoint.Y))
        {
            //Debug.Log("MoveTowards PointZiel erreicht");
            currentlyMovingTowardsPoint = false;
        }

        CharacterMovement(new Vector2(currentDestinationPoint.x - transform.position.x, currentDestinationPoint.y - transform.position.y));


    }

    public void RotateCharacterTowardsPoint(float targetX, float targetY)
    {
      

        float charX = transform.position.x;
        float charY = transform.position.y;
        float ratio = (charX - targetX) / (charY - targetY);

        //Debug.Log(" ");
        if (charX < targetX)
        {
            // N, NE, E, SE, S
            if(charY < targetY)
            {
                // N, NE, E
                //Debug.Log("charX: " + charX + " targetX: " + targetX + " charX/targetX: " + charX / targetX);
                //Debug.Log("charY: " + charY + " targetY: " + targetY + " charY/targetY: " + charY / targetY);
                //Debug.Log("ratio X: " + charX / targetX + " ratio Y: " + charY / targetY);
                //Debug.Log("distance X: " + (charX - targetX) + " distance Y: " + (charY - targetY) + "ratio :" + ((charX - targetX) / (charY - targetY)));

                if(ratio <= 0.33)
                {
                    
                    RotateCharacterTowardsDirection(Direction.N);
                }
                if((ratio <= 3.3)&&(ratio >= 0.33))
                {
                    RotateCharacterTowardsDirection(Direction.NE);
                }
                if (ratio >= 3.3)
                {
                    RotateCharacterTowardsDirection(Direction.E);
                }
            }
            else
            {
                // S, SE, E
                //Debug.Log("distance X: " + (charX - targetX) + " distance Y: " + (charY - targetY) + "ratio :" + ((charX - targetX) / (charY - targetY)));
                if (ratio >= -0.33)
                {
                    RotateCharacterTowardsDirection(Direction.S);
                }
                if ((ratio >= -3.3) && (ratio <= -0.33))
                {
                    RotateCharacterTowardsDirection(Direction.SE);
                }
                if (ratio <= -3.3)
                {
                    RotateCharacterTowardsDirection(Direction.E);
                }
            }
        }
        else
        {
            // N, NW, W, SW, S
            if (charY < targetY)
            {
                // N, NW, W
                //Debug.Log("distance X: " + (charX - targetX) + " distance Y: " + (charY - targetY) + "ratio :" + ((charX - targetX) / (charY - targetY)));
                if (ratio >= -0.33)
                {
                    RotateCharacterTowardsDirection(Direction.N);
                }
                if ((ratio >= -3.3) && (ratio <= -0.33))
                {
                    RotateCharacterTowardsDirection(Direction.NW);
                }
                if (ratio <= -3.3)
                {
                    RotateCharacterTowardsDirection(Direction.W);
                }
            }
            else
            {
                // S, SW, W
                //Debug.Log("distance X: " + (charX - targetX) + " distance Y: " + (charY - targetY) + "ratio :" + ((charX - targetX) / (charY - targetY)));
                if (ratio <= 0.33)
                {
                    RotateCharacterTowardsDirection(Direction.S);
                }
                if ((ratio <= 3.3) && (ratio >= 0.33))
                {
                    RotateCharacterTowardsDirection(Direction.SW);
                }
                if (ratio >= 3.3)
                {
                    RotateCharacterTowardsDirection(Direction.W);
                }
            }

        }

        
    }

    void RotateCharacterTowardsDirection(Direction direction)
    {
        switch (direction)
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
    }

}
