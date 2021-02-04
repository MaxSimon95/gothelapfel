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
      
        fullname = forename + " " + lastname;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
