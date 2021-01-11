using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockSeasonDisplay : MonoBehaviour
{
    IEnumerator Start()
    {

        while (true)
        {
            UpdateSeasonText();
            yield return new WaitForSeconds(1);
        }

    }

    void UpdateSeasonText()
    {
        string seasonText = "Season: ";
        //Debug.Log(GameTime.currentSeason);
        switch(GameTime.currentSeason)
        {
            case GameTime.season.spring:
                seasonText += "Spring";
  
                break;
            case GameTime.season.summer:
                seasonText += "Summer";
                break;
            case GameTime.season.autumn:
                seasonText += "Autumn";
                break;
            case GameTime.season.winter:
                seasonText += "Winter";
                break;
            default:
                seasonText += "ERROR SEASON TEXT SWITCH CASE FAILED";
                Debug.LogError("ERROR SEASON TEXT SWITCH CASE FAILED");
  
                break;
        }    

        gameObject.GetComponent<UnityEngine.UI.Text>().text = seasonText;
        //Debug.Log(seasonText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
