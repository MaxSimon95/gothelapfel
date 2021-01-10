using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockSeasonDisplay : MonoBehaviour
{
    void Start()
    {

        UpdateSeasonText();

    }

    void UpdateSeasonText()
    {
        string seasonText = "Season: ";
        Debug.Log(GameTime.currentSeason);
        switch(GameTime.currentSeason)
        {
            case GameTime.season.spring:
                seasonText += "Spring";
                Debug.Log(1);
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
                seasonText += "no pls no";
                Debug.Log(2);
                break;
        }    

        gameObject.GetComponent<UnityEngine.UI.Text>().text = seasonText;
        Debug.Log(seasonText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
