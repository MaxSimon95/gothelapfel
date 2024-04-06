using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NotebookRecipes : MonoBehaviour
{
    private List<Transform> recipePanels = new List<Transform>();
    private List<AlchemyReaction> recipesList = new List<AlchemyReaction>();

    public Transform AlchemyEngineRecipes;

    public GameObject paginationButtonBack;
    public GameObject paginationButtonForward;
    public GameObject paginationTextLeft;
    public GameObject paginationTextRight;

    public NotebookRecipeDetails notebookRecipeDetails;


    //public ingredientItemDetails ingredientTypeDetails;

    int openPage = 0;
    int itemsPerPage = 40;

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
        //Debug.Log("opening");

        NotebookBaseUI.AddToHistory(this.gameObject);

        UpdateDetailPanels();

        UpdatePaginationButtons();
        UpdatePageNumbers();

        GetComponent<NotebookBaseUI>().Open();

    }

    private void LoadRecipes()
    {
        recipesList.Clear();

        foreach (Transform child in AlchemyEngineRecipes)
        {
            //Debug.Log(child);

            if (child.gameObject.GetComponent<AlchemyReaction>().knownToPlayer && !child.gameObject.GetComponent<AlchemyReaction>().AlwaysHideFromNotebookView)
            {
                //Debug.Log(child.gameObject.name + "it's in there");
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


        for (int i = 0; i < recipePanels.Count; i++)
        {
            
            recipePanels[i].gameObject.SetActive(false);

            if (i < recipesList.Count)
            {
                
                recipePanels[i].gameObject.SetActive(true);

                // CAREFUL: THIS STUFF IS ORDERING SENSITIVE. YOU MESS WITH THE ORDERING, YOU MESS WITH THE CONTENTS, YO! 

                // setting sprite: 
                //recipePanels[i].GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = recipesList[i + openPage * 100].inventorySprite;

                recipePanels[i].GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = recipesList[i + openPage * itemsPerPage].reactionName;


            }
        }
    }


    public void UpdatePaginationButtons()
    {
        //open backbutton
        if (openPage > 0)
        {
            paginationButtonBack.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            paginationButtonBack.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        //open forwardbutton
        if (recipesList.Count > ((openPage + 1) * itemsPerPage))
        {
            paginationButtonForward.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            paginationButtonForward.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
    }

    public void UpdatePageNumbers()
    {
        paginationTextLeft.gameObject.GetComponent<UnityEngine.UI.Text>().text = (openPage * 2 + 1).ToString();
        paginationTextRight.gameObject.GetComponent<UnityEngine.UI.Text>().text = (openPage * 2 + 2).ToString();
    }

    public void PaginateForward()
    {
        openPage++;
        UpdatePaginationButtons();
        UpdatePageNumbers();
        UpdateDetailPanels();
        GetComponent<NotebookBaseUI>().PlaySound();
        //Debug.Log("Pageforward: " + openPage);
    }

    public void PaginateBackward()
    {
        openPage--;
        UpdatePaginationButtons();
        UpdatePageNumbers();
        UpdateDetailPanels();
        GetComponent<NotebookBaseUI>().PlaySound();
    }

    public void OpenIngredientDetails(int i)
    {

        GetComponent<NotebookBaseUI>().Close();
        notebookRecipeDetails.Open(recipesList[i + openPage * itemsPerPage]);


    }
}
