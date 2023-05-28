using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScreenHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public Texture2D timeCyclePaletteImage;
    public Color[] timeCyclePaletteArray;

    void Start()
    {
        timeCyclePaletteArray = timeCyclePaletteImage.GetPixels();
        UpdateColorScreen(false);
        //Debug.Log(timeCyclePaletteArray[0]);

        //LeanTween.color(GetComponent<RectTransform>(), new Color (255,0,0), 3f);
        //LeanTween.color(GetComponent<RectTransform>(), new Color(0, 255, 0), 10f);

    }

    public void UpdateColorScreen(bool animationOverTime)
    {
        
        int paletteOffset;
        switch(GameTime.currentSeason)
        {
            case GameTime.season.SPRING:
                paletteOffset = 0;
                break;
            case GameTime.season.SUMMER:
                paletteOffset = 24;
                break;
            case GameTime.season.AUTUMN:
                paletteOffset = 48;
                break;
            default:
                paletteOffset = 72;
                break;
        }
        Debug.Log((int)GameTime.hourOfTheDay);
        //GetComponent<UnityEngine.UI.Image>().color = timeCyclePaletteArray[1];
        Color targetColor = timeCyclePaletteArray[(int)GameTime.hourOfTheDay + paletteOffset];
        //GetComponent<UnityEngine.UI.Image>()


        
        if(animationOverTime)
            LeanTween.color(GetComponent<RectTransform>(), targetColor, 1f);
        else
            LeanTween.color(GetComponent<RectTransform>(), targetColor, 0f);
        
        


        //LeanTween.moveY(GameObject.Find("PanelInventory").GetComponent<RectTransform>(), 333, 0.8f).setEaseInOutCubic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
