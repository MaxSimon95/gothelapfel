using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderOrderAdjustment : MonoBehaviour
{
    // Start is called before the first frame update
    public bool lyingOnFloor;

    public static bool anyOverlayOpen = false;
    //public static bool BlockedByUI = false;

    public void AdjustRenderOrder()
    {
        if (lyingOnFloor)
            GetComponent<SpriteRenderer>().sortingOrder = -30000;
        else
            GetComponent<SpriteRenderer>().sortingOrder = -1 * (int)(100 * this.gameObject.GetComponent<Collider2D>().bounds.min.y);


        //Debug.Log("Boudn box");
        //Debug.Log(this.gameObject.GetComponent<Collider2D>().bounds.min.y);
    }

    void Start()
    {
        AdjustRenderOrder();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
