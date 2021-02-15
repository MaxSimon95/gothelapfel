using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NotebookRecipes : MonoBehaviour
{
    private List<Transform> recipePanels = new List<Transform>();
    private List<AlchemyReaction> recipesList = new List<AlchemyReaction>();

    public Transform AlchemyEngineRecipes;

    public NotebookRecipeDetails notebookRecipeDetails;


    //public ingredientItemDetails ingredientTypeDetails;

    int openPage = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Open()
    {
        Debug.Log("opening");
        UpdateDetailPanels();
        GetComponent<NotebookBaseUI>().Open();

    }

    private void LoadRecipes()
    {
        recipesList.Clear();

        foreach (Transform child in AlchemyEngineRecipes)
        {
            Debug.Log(child);

            if (child.gameObject.GetComponent<AlchemyReaction>().knownToPlayer && !child.gameObject.GetComponent<AlchemyReaction>().AlwaysHideFromNotebookView)
            {
                Debug.Log(child.gameObject.name + "it's in there");
                recipesList.Add(child.gameObject.GetComponent<AlchemyReaction>());
            }
        }

        recipesList = recipesList.OrderBy(recipe => recipe.reactionName).ToList();

    }

    private void LoadRecipePanels()
    {
        recipePanels.Clear();

        foreach (Transform child in transform.GetChild(0))
        {
            if (child.gameObject.name == "PanelRecipe")
            {
                recipePanels.Add(child);
            }
        }
    }

    public void UpdateDetailPanels()
    {

        LoadRecipePanels();

        LoadRecipes();

        //Debug.Log("ingredienttypepanel count " + recipePanels.Count);
        //Debug.Log("ingredienttypes count" + recipesList.Count);

        for (int i = 0; i < recipePanels.Count; i++)
        {
            recipePanels[i].localScale = new Vector3(0, 0, 0);

            //Debug.Log(i);

            //Debug.Log(JobsManagement.activeJobList.Count);
            if (i < recipesList.Count)
            {
                recipePanels[i].localScale = new Vector3(1, 1, 1);
                //Debug.Log(i + "set to visible, local scale: " + recipePanels[i].localScale);
                //Debug.Log(i);
                // CAREFUL: THIS STUFF IS ORDERING SENSITIVE. YOU MESS WITH THE ORDERING, YOU MESS WITH THE CONTENTS, YO! 
                //Debug.Log(i);


                // setting sprite: 
                //recipePanels[i].GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = recipesList[i + openPage * 100].inventorySprite;

                recipePanels[i].GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = recipesList[i + openPage * 100].reactionName;

                //Debug.Log(i + "info updated");

            }
        }
    }

    public void OpenIngredientDetails(int i)
    {
        //Debug.Log(i + openPage * 10);
        //Debug.Log(i + " " + JobsManagement.activeJobList[i + openPage * 10].title);
        //Debug.Log(jobList[i + openPage * 10]);
        //JobHandler.detailJob = JobsManagement.activeJobList[i + openPage * 10];
        GetComponent<NotebookBaseUI>().Close();
        //notebookRecipeDetails.Open(recipesList[i + openPage * 100]);


    }
}
