using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookBaseUI : MonoBehaviour
{
    //public static bool timeIsStopped = false;
    private AudioSource source;
    public AudioClip sound;
    public static Stack<GameObject> historyNotebooks = new Stack<GameObject>();
    public static Stack<GameObject> historyAdditionalParameter = new Stack<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
                Close();
        }
    }

    public void Open()
    {
        CanvasContainerHandler.SetSceneUIVisible(false);
        PlaySound();
        transform.GetChild(0).localScale = new Vector3(1, 1, 1);
        GameTime.timeIsStopped = true;
        RenderOrderAdjustment.anyOverlayOpen = true;
        

    }

    public void Close()
    {
        Debug.Log("CLOSE CALL: " + this.gameObject.name);

        CanvasContainerHandler.SetSceneUIVisible(true);
        //Debug.Log("CLOSE");
        transform.GetChild(0).localScale = new Vector3(0, 0, 0);

        //Debug.Log(transform.GetChild(0).localScale);
        GameTime.timeIsStopped = false;
        RenderOrderAdjustment.anyOverlayOpen = false;
        // 
        GameObject.Find("JobManagement").GetComponent<JobsManagement>().UpdateActiveJobs(false);
    }

    public static void AddToHistory(GameObject notebook)
    {
        AddToHistory(notebook, notebook);
    }

    public static void AddToHistory(GameObject notebook, GameObject additionalParameter)
    {
        historyNotebooks.Push(notebook);
        historyAdditionalParameter.Push(additionalParameter);
    }

    public void OpenFromHistory()
    {
        Close(); 

        historyNotebooks.Pop();
        historyAdditionalParameter.Pop();

        if (historyNotebooks.Count > 0)
        {
            GameObject notebook = historyNotebooks.Pop();
            GameObject additionalParameter = historyAdditionalParameter.Pop();

            if (notebook.GetComponent<NotebookIngredients>() != null)
                notebook.GetComponent<NotebookIngredients>().Open();

            if (notebook.GetComponent<NotebookIngredientDetails>() != null)
                notebook.GetComponent<NotebookIngredientDetails>().Open(additionalParameter.GetComponent<IngredientType>());

            if (notebook.GetComponent<NotebookRecipes>() != null)
                notebook.GetComponent<NotebookRecipes>().Open();

            if (notebook.GetComponent<NotebookRecipeDetails>() != null)
                notebook.GetComponent<NotebookRecipeDetails>().Open(additionalParameter.GetComponent<AlchemyReaction>());

            if (notebook.GetComponent<NotebookRegions>() != null)
                notebook.GetComponent<NotebookRegions>().Open();

            if (notebook.GetComponent<NotebookRegionDetails>() != null)
                notebook.GetComponent<NotebookRegionDetails>().Open(additionalParameter.GetComponent<RegionHandler>());

            if (notebook.GetComponent<NotebookJobs>() != null)
                notebook.GetComponent<NotebookJobs>().Open();

            if (notebook.GetComponent<JobDetails>() != null)
                notebook.GetComponent<JobDetails>().Open(additionalParameter.GetComponent<JobHandler>());
        }

    }

    

        

    public void PlaySound()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(sound, 1f);
    }

}
