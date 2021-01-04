using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class RoomChanger : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string targetScene;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.color(gameObject, new Color(1f, 0.94f, 0.43f, 1f), 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.color(gameObject, Color.white, 0.1f);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Single);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            //m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                //m_Text.text = "Press the space bar to continue";
                //Wait to you press the space key to activate the Scene
                //if (Input.GetKeyDown(KeyCode.Space))
                    //Activate the Scene
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
        
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(targetScene));

       /* foreach (var raycaster in Transform.FindObjectsOfType<BaseRaycaster>())
        {
            Debug.Log(raycaster.enabled);
            raycaster.enabled = raycaster.gameObject.scene.name != targetScene;
            Debug.Log(raycaster.enabled);
        } */

    }


}
