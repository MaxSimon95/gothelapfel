using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NotebookIngredients : MonoBehaviour
{
    private List<Transform> ingredientTypePanels = new List<Transform>();
    private List<IngredientType> ingredientTypeList = new List<IngredientType>();

    public Transform AlchemyEngineIngredientTypes;

    public NotebookIngredientDetails notebookIngredientDetails;
    

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

    private void LoadIngredients()
    {
        ingredientTypeList.Clear();

        foreach (Transform child in AlchemyEngineIngredientTypes)
        {
            if (child.gameObject.GetComponent<IngredientType>().knownToPlayer)
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

        Debug.Log("ingredienttypepanel count " + ingredientTypePanels.Count);
        Debug.Log("ingredienttypes count" + ingredientTypeList.Count);

        for (int i = 0; i < ingredientTypePanels.Count; i++)
        {
            ingredientTypePanels[i].localScale = new Vector3(0, 0, 0);

            //Debug.Log(i);

            //Debug.Log(JobsManagement.activeJobList.Count);
            if (i < ingredientTypeList.Count)
            {
                ingredientTypePanels[i].localScale = new Vector3(1, 1, 1);
                Debug.Log(i + "set to visible, local scale: " + ingredientTypePanels[i].localScale);
                //Debug.Log(i);
                // CAREFUL: THIS STUFF IS ORDERING SENSITIVE. YOU MESS WITH THE ORDERING, YOU MESS WITH THE CONTENTS, YO! 
                //Debug.Log(i);

                ingredientTypePanels[i].GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = ingredientTypeList[i + openPage * 100].inventorySprite;
                
                ingredientTypePanels[i].GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text =  ingredientTypeList[i + openPage * 100].ingredientName;

                Debug.Log(i + "info updated");

            }
        }
    }

    public void OpenIngredientDetails(int i)
    {
        //Debug.Log(i + openPage * 10);
        //Debug.Log(i + " " + JobsManagement.activeJobList[i + openPage * 10].title);
        //Debug.Log(jobList[i + openPage * 10]);
        //JobHandler.detailJob = JobsManagement.activeJobList[i + openPage * 10];
        notebookIngredientDetails.Open(ingredientTypeList[i + openPage * 100]);
       

    }
}
