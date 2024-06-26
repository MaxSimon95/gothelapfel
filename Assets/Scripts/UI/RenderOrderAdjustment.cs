﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderOrderAdjustment : MonoBehaviour
{
    // Start is called before the first frame update
    public bool lyingOnFloor;

    public static bool anyOverlayOpen = false;
    //public static bool BlockedByUI = false;
    public RoomHandler room;

    public void AdjustRenderOrder()
    {
        if (lyingOnFloor)
            GetComponent<SpriteRenderer>().sortingOrder = 1;
        else
            GetComponent<SpriteRenderer>().sortingOrder = -1 * (int)(100 * this.gameObject.GetComponent<Collider2D>().bounds.min.y) + 600;


        //Debug.Log("Boudn box");
        //Debug.Log(this.gameObject.GetComponent<Collider2D>().bounds.min.y);
    }

    void Start()
    {
        room = DetermineRoom(transform);
        AdjustRenderOrder();
    }

    private RoomHandler DetermineRoom(Transform pTransform)
    {
        if(pTransform.parent != null)
        {
            if(pTransform.parent.gameObject.tag == "Room")
            {
                return(pTransform.parent.gameObject.GetComponent<RoomHandler>());
            }
            else
            {
                return (DetermineRoom(pTransform.parent));
            }
        }
        else
        {
            Debug.LogError(name + " is not set in any Room. It has to be set in a room via the Unity Editor (Transform Hierarchy). ");
            return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
