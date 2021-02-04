using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string forename;
    public string lastname;
    public string fullname;
    public string occupation;
    public Sprite image;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<4; i++)
        {
            Debug.Log("Why is this not working");
        }
        fullname = forename + " " + lastname;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
