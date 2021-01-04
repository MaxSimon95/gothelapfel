using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangePreserver : MonoBehaviour
{
    private static List<string> gameObjectsToBePreserved; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        if (gameObjectsToBePreserved == null)
        {
            //Debug.Log("gameObjectsToBePreserved created" );
            gameObjectsToBePreserved = new List<string>();
        }
        DontDestroyOnLoad(this);

        //Debug.Log(gameObjectsToBePreserved.Contains(gameObject.name));
        if (gameObjectsToBePreserved.Contains(gameObject.name))
        {
            Destroy(gameObject);
        }
        else
        {
            gameObjectsToBePreserved.Add(gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
