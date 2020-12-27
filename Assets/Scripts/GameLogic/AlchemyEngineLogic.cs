using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyEngineLogic : MonoBehaviour
{
    public bool isActive;
    public float UpdateFrequencyInSeconds;
    public List<GameObject> alchemyReactions;
    //private GameObject alchemyReactionsParent;
    public AudioClip reactionSound;
    private AudioSource source;

     
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
            //Debug.Log(x.ToString());
        }

    }

    public void CheckForFittingAlchemyReaction(List<IngredientType> ingredientTypes, List<int> ingredientTypeAmounts, float surroundingTemperature, string surroundingContainerName)
    {
        CheckForFittingAlchemyReaction(ingredientTypes, ingredientTypeAmounts, 0, surroundingTemperature, surroundingContainerName);
    }

    public void CheckForFittingAlchemyReaction(List<IngredientType> ingredientTypes, List<int> ingredientTypeAmounts, int reactionsHaveHappened, float surroundingTemperature, string surroundingContainerName)
    {
        
        //Debug.Log("Starting CheckForFittingAlchemyReaction");
        // Vorgehen: Eine temp liste von alchemyreactions anlegen, die erstmal alle enthält. und in jedem durchlauf werden dann auf möglichst performante weise 
        // reactions eliminiert, die es nicht sein können und rausgeschmissen, bis nur noch die gültigen bleiben. dann wird nach prio sortiert.

        List<GameObject> alchemyReactionCandidates = new List<GameObject>(alchemyReactions);
        //Debug.Log(alchemyReactionCandidates.Count);

        /*foreach (var x in alchemyReactionCandidates)
        {
            Debug.Log(x.ToString());
        }*/

        // step 1: remove all alchemyreactions which have to few different input ingredienttypes to even be possible (precheck)

        for (int index = alchemyReactionCandidates.Count - 1; index >= 0; index--)
        {
            //Debug.Log(index);
            if (alchemyReactionCandidates[index].GetComponent<AlchemyReaction>().inputIngredientTypes.Count > ingredientTypes.Count)
            {
                alchemyReactionCandidates.RemoveAt(index);
            }
        }

        //Debug.Log(alchemyReactionCandidates.Count);

        // step 1.5: remove all alchemyreactions which dont fit the temperature

        for (int index = alchemyReactionCandidates.Count - 1; index >= 0; index--)
        {
            // if min and max temperature are completely identical we assume that nothing has been specified at all --> the reaction has no temperature constraint
            if(alchemyReactionCandidates[index].GetComponent<AlchemyReaction>().minTemperature != alchemyReactionCandidates[index].GetComponent<AlchemyReaction>().maxTemperature)
            {
                // check if min and max temperature fit
                if ((surroundingTemperature < alchemyReactionCandidates[index].GetComponent<AlchemyReaction>().minTemperature) || (surroundingTemperature > alchemyReactionCandidates[index].GetComponent<AlchemyReaction>().maxTemperature))
                {
                    alchemyReactionCandidates.RemoveAt(index);
                }
            }
        }

        // Step 1.7 remove all alchemyreactions with a container constraint that doesnt fit

        for (int index = alchemyReactionCandidates.Count - 1; index >= 0; index--)
        {
            // check if there are any constraints on the container at all
            if (alchemyReactionCandidates[index].GetComponent<AlchemyReaction>().validAlchemyContainers.Count > 0)
            {
                Debug.Log(surroundingContainerName);
                // check if this container is one of the valid ones
                if (!alchemyReactionCandidates[index].GetComponent<AlchemyReaction>().validAlchemyContainers.Contains(surroundingContainerName))
                {
                    alchemyReactionCandidates.RemoveAt(index);
                }
            }
        }

        // step 2: Iterate through each ingredienttype present and remove all alchemyreactioncandidates that have to little of that 

        // traverse all reaction candidates
        for (int index = alchemyReactionCandidates.Count - 1; index >= 0; index--)
        //for (int index = 0; index < ingredientTypes.Count; index++)
        {
            //Debug.Log(CheckForEnoughIngredients(alchemyReactionCandidates[index],ingredientTypes, ingredientTypeAmounts));
            if(!CheckForEnoughIngredients(alchemyReactionCandidates[index], ingredientTypes, ingredientTypeAmounts))
            {
                alchemyReactionCandidates.RemoveAt(index);
            }
        }

        // step 3: find remaining alchemyreaction-candidate with highest priority and make that alchemy reaction happen!

        //Debug.Log("FINAL APPLICABLE SET OF CANDIDATES");
        
        if(alchemyReactionCandidates.Count > 0)
        {
            reactionsHaveHappened++;
            foreach (var x in alchemyReactionCandidates)
            {
                //Debug.Log(x.ToString());
            }

            int tempMaxPrio = 0;
            GameObject tempCandidate = null;
            foreach(GameObject reaction in alchemyReactionCandidates)
            {
                if (reaction.GetComponent<AlchemyReaction>().priority > tempMaxPrio)
                {
                    tempMaxPrio = reaction.GetComponent<AlchemyReaction>().priority;
                    tempCandidate = reaction;
                }
                    
            }

            //Debug.Log(tempCandidate);
            ExecuteAlchemyReaction(tempCandidate, ingredientTypes, ingredientTypeAmounts);
            
            CheckForFittingAlchemyReaction(ingredientTypes,ingredientTypeAmounts, reactionsHaveHappened, surroundingTemperature, surroundingContainerName);

        }
        else
        {
            //Debug.Log("No alchemy reaction candidates applicable");
        }

        if (reactionsHaveHappened == 1)
        {
            // here we can execute fancy stuff like SOUNDZ and sparkle effects and all that that we want to happen ONCE if a batch of n>=1 reactions occured
            source = GetComponent<AudioSource>();
            source.PlayOneShot(reactionSound, 1f);
            

        }

    }
    private void ExecuteAlchemyReaction(GameObject reaction, List<IngredientType> ingredientTypesAvailable, List<int> ingredientTypeAmountsAvailable)
    {

        // remove required input ingredients from origin

        for(int i=0; i< reaction.GetComponent<AlchemyReaction>().inputIngredientTypes.Count; i++)
        {

            for (int j = ingredientTypesAvailable.Count-1; j >= 0; j--)
            {

                if(reaction.GetComponent<AlchemyReaction>().inputIngredientTypes[i] == ingredientTypesAvailable[j])
                {
                    Debug.Log("HIT");
                    ingredientTypeAmountsAvailable[j] -= reaction.GetComponent<AlchemyReaction>().inputIngredientTypeAmounts[i];
                }
                else
                {
                    Debug.Log("MISS");
                }

            }


        }


        // add aquired output ingredients to origin

        for (int i = 0; i < reaction.GetComponent<AlchemyReaction>().outputIngredientTypes.Count; i++)
        {
            ingredientTypesAvailable.Add(reaction.GetComponent<AlchemyReaction>().outputIngredientTypes[i]);
            ingredientTypeAmountsAvailable.Add(reaction.GetComponent<AlchemyReaction>().outputIngredientTypeAmounts[i]);
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
