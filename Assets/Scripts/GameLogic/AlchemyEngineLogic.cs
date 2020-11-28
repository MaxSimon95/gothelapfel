using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyEngineLogic : MonoBehaviour
{
    public bool isActive;
    public float UpdateFrequencyInSeconds;
    public List<GameObject> alchemyReactions;
    //private GameObject alchemyReactionsParent;

    IEnumerator Start()
    {
        LoadAlchemyReactionsIntoList();



        while (isActive)
        {
            yield return new WaitForSeconds(UpdateFrequencyInSeconds);
            //Debug.Log("Alchemy Commencing");


            foreach (Transform child in transform.parent)
            {

                if (child.gameObject.name == "ButtonTransferOutOfContainer")
                {
                    child.gameObject.GetComponent<TransferOutOfContainerHandler>().updateButtonActive();
                }
            }

        }
    }

    private void LoadAlchemyReactionsIntoList()
    {
        GameObject alchemyReactionsParent = null;
        foreach (Transform child in transform)
        {

            if (child.gameObject.name == "AlchemyReactions")
            {
                alchemyReactionsParent = child.gameObject;
            }
        }

        foreach (Transform child in alchemyReactionsParent.transform)
        {

            alchemyReactions.Add(child.gameObject);

        }

        foreach (var x in alchemyReactions)
        {
            Debug.Log(x.ToString());
        }

    }

    public void CheckForFittingAlchemyReaction(List<IngredientType> ingredientTypes, List<int> ingredientTypeAmounts)
    {
        Debug.Log("Starting CheckForFittingAlchemyReaction");
        // Vorgehen: Eine temp liste von alchemyreactions anlegen, die erstmal alle enthält. und in jedem durchlauf werden dann auf möglichst performante weise 
        // reactions eliminiert, die es nicht sein können und rausgeschmissen, bis nur noch die gültigen bleiben. dann wird nach prio sortiert.

        List<GameObject> alchemyReactionCandidates = new List<GameObject>(alchemyReactions);
        Debug.Log(alchemyReactionCandidates.Count);

        /*foreach (var x in alchemyReactionCandidates)
        {
            Debug.Log(x.ToString());
        }*/

        // step 1: remove all alchemyreactions which have to few different input ingredienttypes to even be possible 
        Debug.Log("Step 1: Check counts and remove ill suited candidates");

        for (int index = alchemyReactionCandidates.Count - 1; index >= 0; index--)
        {
            //Debug.Log(index);
            if (alchemyReactionCandidates[index].GetComponent<AlchemyReaction>().inputIngredientTypes.Count > ingredientTypes.Count)
            {
                Debug.Log("removed");
                alchemyReactionCandidates.RemoveAt(index);
            }
            else
            {
                Debug.Log("not removed");
            }
        }

        Debug.Log(alchemyReactionCandidates.Count);
        /*foreach (var x in alchemyReactionCandidates)
        {
            Debug.Log(x.ToString());
        }*/

        // step 2: Iterate through each ingredienttype present and remove all alchemyreactioncandidates that have to little of that 

        // traverse all reaction candidates
        for (int index = alchemyReactionCandidates.Count - 1; index >= 0; index--)
        //for (int index = 0; index < ingredientTypes.Count; index++)
        {
            Debug.Log(CheckForEnoughIngredients(alchemyReactionCandidates[index],ingredientTypes, ingredientTypeAmounts));

        }



        }

    private bool CheckForEnoughIngredients (GameObject alchemyReactionCandidate, List<IngredientType> ingredientTypes, List<int> ingredientTypeAmounts)
        {

       
            // traverse all input ingredient types in reaction candidate
            for (int j = 0; j < alchemyReactionCandidate.GetComponent<AlchemyReaction>().inputIngredientTypes.Count; j++)
            {
                IngredientType requiredIngredientInput = alchemyReactionCandidate.GetComponent<AlchemyReaction>().inputIngredientTypes[j];
                int requiredAmountInput = alchemyReactionCandidate.GetComponent<AlchemyReaction>().inputIngredientTypeAmounts[j];
                bool enoughOfIngredient = false;

                // traverse all available ingredient types (in container, inventory item, wherever this is checked)
                for (int k = 0; k < ingredientTypes.Count; k++)
                {
                    IngredientType currentIngredientAvailable = ingredientTypes[k];
                    int currentAmountAvailable = ingredientTypeAmounts[k];

                    if (
                        (currentIngredientAvailable == requiredIngredientInput)
                        &&
                        (currentAmountAvailable >= requiredAmountInput)
                       )
                    {
                        enoughOfIngredient = true;
                    }
                }

                // if the input ingredient that has just been checked is not available in sufficent quantity, return false and stop this madness
                if(!enoughOfIngredient)
                {
                    return false;
                }

            }

            //if all input ingredients could be traversed without it throwing false once and cancelling the method, everything we need is available
            return true;

        

    }
}
