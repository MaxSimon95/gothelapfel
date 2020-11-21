using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyEngineLogic : MonoBehaviour
{
    public bool isActive;
    public float UpdateFrequencyInSeconds;

    IEnumerator Start ()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(UpdateFrequencyInSeconds);
            //Debug.Log("Alchemy Commencing");
        }
    }
}
