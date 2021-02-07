using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookIngredientDetails : MonoBehaviour
{

    public IngredientType ingredient;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(IngredientType pIngredientType)
    {
       
        ingredient = pIngredientType;
        UpdateIngredientDetails();
        GetComponent<NotebookBaseUI>().Open();
    }

    public void UpdateIngredientDetails()
    {

    }
}
