using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogHandler : MonoBehaviour
{

    public NPC npc;
    [SerializeField] public UnityEvent eventQueue;

    // Start is called before the first frame update
    void Start()
    {
        eventQueue.AddListener(delegate { Test("frei"); });
        eventQueue.AddListener(Test2);
        //eventQueue.AddListener(delegate { Debug.Log("Externally Added eventcode : '" + "SSSS" + "' gets executed."); });
        // eventQueue.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Test(string x)
    {
        Debug.Log("Meiner linker linker Platz ist " + x);
    }

    void Test2()
    {
        Debug.Log("Ich wünsche mir Eistee herbei");
    }

}
