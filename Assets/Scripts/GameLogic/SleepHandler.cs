using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepHandler : MonoBehaviour
{
    public GameObject panel;
    public GameObject text;
    public GameTime gameTime;

    public float wakeUpTime;

    // Start is called before the first frame update
    void Start()
    {
        //LeanTween.alpha(text.GetComponent<UnityEngine.UI.Text>(), 0f, 0);
        panel.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.alpha(panel.GetComponent<RectTransform>(), 0f, 0);
        text.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sleep()
    {
        StartCoroutine(SleepStart());
        
    }

    IEnumerator SleepStart()
    {
        GameTime.timeIsStopped = true;

        panel.transform.localScale = new Vector3(1, 1, 1);

        LeanTween.alpha(panel.GetComponent<RectTransform>(), 1f, 3);

        yield return new WaitForSeconds(3.5f);

        gameTime.JumpToHourOfTheDay(wakeUpTime);

        text.GetComponent<UnityEngine.UI.Text>().text = "Day " + (GameTime.daysSinceStart + 1);

        text.transform.localScale = new Vector3(1, 1, 1);
        

        yield return new WaitForSeconds(2);

        StartCoroutine(SleepEnd());

    }

    IEnumerator SleepEnd()
    {

        text.transform.localScale = new Vector3(0, 0, 0);

        LeanTween.alpha(panel.GetComponent<RectTransform>(), 0f, 1);

        yield return new WaitForSeconds(1);

        panel.transform.localScale = new Vector3(0, 0, 0);

        GameTime.timeIsStopped = false;

        
    }
}
