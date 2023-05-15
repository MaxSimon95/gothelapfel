using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockTimeDisplay : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(TimeDisplayLoop());
    }

    IEnumerator TimeDisplayLoop()
    {

        while (true)
        {

            gameObject.GetComponent<UnityEngine.UI.Text>().text = "Time: " + (int)GameTime.hourOfTheDay;
            yield return new WaitForSeconds(1);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
