using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogHandler : MonoBehaviour
{

    public NPC npc;
    [SerializeField] private UnityEvent eventQueue;

    // Start is called before the first frame update
    void Start()
    {
        eventQueue.AddListener(Test);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Test()
    {
        Debug.Log("shafumasss");
    }

}
