using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NotebookIngredients : MonoBehaviour
{
    private List<Transform> ingredientTypePanels = new List<Transform>();
    private List<IngredientType> ingredientTypeList = new List<IngredientType>();

    public GameObject paginationButtonBack;
    public GameObject paginationButtonForward;
    public GameObject paginationTextLeft;
    public GameObject paginationTextRight;

    public Transform AlchemyEngineIngredientTypes;

    public NotebookIngredientDetails notebookIngredientDetails;
    

    //public ingredientItemDetails ingredientTypeDetails;

    int openPage = 0;
    int itemsPerPage = 100;

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

    private void LoadIngredients()
    {
        ingredientTypeList.Clear();

        foreach (Transform child in AlchemyEngineIngredientTypes)
        {
            if (child.gameObject.GetComponent<IngredientType>().knownToPlayer && !child.gameObject.GetComponent<IngredientType>().AlwaysHideFromNotebookView)
            {
                ingredientTypeList.Add(child.gameObject.GetComponent<IngredientType>());
            }
        }
        
        ingredientTypeList = ingredientTypeList.OrderBy(ingredient => ingredient.ingredientName).ToList();
        
    }

    private void LoadIngredientTypePanels()
    {
        ingredientTypePanels.Clear();

        foreach (Transform child in transform.GetChild(0))
        {
            if (child.gameObject.name == "PanelIngredient")
            {
                ingredientTypePanels.Add(child);
            }
        }
    }

    public void UpdateDetailPanels()
    {

        LoadIngredientTypePanels();
        
        LoadIngredients();

        //Debug.Log("ingredienttypepanel count " + ingredientTypePanels.Count);
        //Debug.Log("ingredienttypes count" + ingredientTypeList.Count);

        for (int i = 0; i < ingredientTypePanels.Count; i++)
        {
            ingredientTypePanels[i].localScale = new Vector3(0, 0, 0);

            //Debug.Log(i);

            //Debug.Log(JobsManagement.activeJobList.Count);
            if (i < (ingredientTypeList.Count-openPage*itemsPerPage))
            {
                ingredientTypePanels[i].localScale = new Vector3(1, 1, 1);
                //Debug.Log(i + "set to visible, local scale: " + ingredientTypePanels[i].localScale);
                //Debug.Log(i);
                // CAREFUL: THIS STUFF IS ORDERING SENSITIVE. YOU MESS WITH THE ORDERING, YOU MESS WITH THE CONTENTS, YO! 
                //Debug.Log(i);

                ingredientTypePanels[i].GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = ingredientTypeList[i + openPage * itemsPerPage].inventorySprite;
                
                ingredientTypePanels[i].GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text =  ingredientTypeList[i + openPage * itemsPerPage].ingredientName;

                //Debug.Log(i + "info updated");

            }
            else
            {
                ingredientTypePanels[i].localScale = new Vector3(0, 0, 0);
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
        if (ingredientTypeList.Count > ((openPage+1) * itemsPerPage))
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
        paginationTextLeft.gameObject.GetComponent<UnityEngine.UI.Text>().text = (openPage *2+ 1).ToString();
        paginationTextRight.gameObject.GetComponent<UnityEngine.UI.Text>().text = (openPage *2 + 2).ToString();
}

    public void PaginateForward()
    {
        openPage++;
        UpdatePaginationButtons();
        UpdatePageNumbers();
        UpdateDetailPanels();
        //Debug.Log("Pageforward: " + openPage);
    }

    public void PaginateBackward()
    {
        openPage--;
        UpdatePaginationButtons();
        UpdatePageNumbers();
        UpdateDetailPanels();
    }

    public void OpenIngredientDetails(int i)
    {
        //Debug.Log(i + openPage * 10);
        //Debug.Log(i + " " + JobsManagement.activeJobList[i + openPage * 10].title);
        //Debug.Log(jobList[i + openPage * 10]);
        //JobHandler.detailJob = JobsManagement.activeJobList[i + openPage * 10];
        GetComponent<NotebookBaseUI>().Close();
        notebookIngredientDetails.Open(ingredientTypeList[i + openPage * itemsPerPage]);
       

    }
}
