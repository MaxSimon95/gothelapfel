using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepHandler : MonoBehaviour
{
    public MoneyHandler moneyHandler;
    public AlchemyEngineLogic alchemyEngine;
    public GameTime gameTime;

    public GameObject panel;
    public GameObject textDay1;
    public GameObject textDay2;
    public GameObject textMoney;
    public GameObject imageMoney;
    public GameObject textMoneyChangeReason;
    public GameObject textMoneyChangeAmount;
    public GameObject imageMoneyChange;
    public GameObject textRecipesToday;
    public GameObject imageRecipeSingle;
    public GameObject textRecipeSingle;

    public GameObject textProgressBar;
    public GameObject imageProgressBarCurrent;
    public GameObject imageProgressBarTotal;

    public GameObject panelMoneyChange;
    //public GameObject panelDay;
    public GameObject panelMoney;
    public GameObject panelRecipesToday;
    public GameObject panelRecipeSingle;

    public float wakeUpTime;
    int tempMoneyTotal;
    int tempRecipesTodayAmount;
    List<AlchemyReaction> tempReactions = new List<AlchemyReaction>();

    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        //LeanTween.alpha(text.GetComponent<UnityEngine.UI.Text>(), 0f, 0);
        panel.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.alpha(panel.GetComponent<RectTransform>(), 0f, 0);
        textDay1.transform.localScale = new Vector3(0, 0, 0);
        textDay2.transform.localScale = new Vector3(0, 0, 0);
        textMoney.transform.localScale = new Vector3(0, 0, 0);
        imageMoney.transform.localScale = new Vector3(0, 0, 0);
        textMoneyChangeReason.transform.localScale = new Vector3(0, 0, 0);
        textMoneyChangeAmount.transform.localScale = new Vector3(0, 0, 0);
        imageMoneyChange.transform.localScale = new Vector3(0, 0, 0);
        textRecipesToday.transform.localScale = new Vector3(0, 0, 0);
        imageRecipeSingle.transform.localScale = new Vector3(0, 0, 0);
        textRecipeSingle.transform.localScale = new Vector3(0, 0, 0);
        imageProgressBarCurrent.transform.localScale = new Vector3(0, 0, 0);
        imageProgressBarTotal.transform.localScale = new Vector3(0, 0, 0);
        textProgressBar.transform.localScale = new Vector3(0, 0, 0);
        panelMoneyChange.transform.localScale = new Vector3(0, 0, 0);
        panelRecipesToday.transform.localScale = new Vector3(0, 0, 0);
        panelRecipeSingle.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sleep(bool pTriggeredFromBed)
    {
        StartCoroutine(SleepStart(pTriggeredFromBed));
        
    }

    public void Sleep()
    {
        StartCoroutine(SleepStart(true));
    }

    IEnumerator SleepStart(bool pTriggeredFromBed)
    {
        GameTime.timeIsStopped = true;


        panel.transform.localScale = new Vector3(1, 1, 1);

        LeanTween.alpha(panel.GetComponent<RectTransform>(), 1f, 3);

        yield return new WaitForSeconds(3f);
        moneyHandler.AddMoneyChange("Rent", (moneyHandler.rent * -1));

        // appear day 1 label
        if (pTriggeredFromBed)
        {
            if (GameTime.hourOfTheDay < wakeUpTime)
            {
                textDay1.GetComponent<UnityEngine.UI.Text>().text = "Day " + (GameTime.daysSinceStart);
            }
            else
            {
                textDay1.GetComponent<UnityEngine.UI.Text>().text = "Day " + (GameTime.daysSinceStart + 1);
            }
        }
        else
        {
            textDay1.GetComponent<UnityEngine.UI.Text>().text = "Day " + (GameTime.daysSinceStart);
        }



        LeanTween.alpha(textDay1.GetComponent<RectTransform>(), 1f, 0);
        textDay1.transform.localScale = new Vector3(1, 1, 1);

        // appear money label
        tempMoneyTotal = moneyHandler.moneyAtDayStart;
        textMoney.GetComponent<UnityEngine.UI.Text>().text = tempMoneyTotal.ToString();
        LeanTween.alpha(textMoney.GetComponent<RectTransform>(), 1f, 0);
        imageMoney.transform.localScale = new Vector3(1, 1, 1);
        textMoney.transform.localScale = new Vector3(1, 1, 1);

        // iterate through money changes
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < moneyHandler.moneyChangeLabelsToday.Count; i++)
        {
            source = GetComponent<AudioSource>();
            source.PlayOneShot(moneyHandler.moneySound, 1f);

            textMoneyChangeReason.GetComponent<UnityEngine.UI.Text>().text = moneyHandler.moneyChangeLabelsToday[i];
            if (moneyHandler.moneyChangeAmountsToday[i] >= 0)
            {
                textMoneyChangeAmount.GetComponent<UnityEngine.UI.Text>().text = "+" + moneyHandler.moneyChangeAmountsToday[i].ToString();
            }
            else
            {
                textMoneyChangeAmount.GetComponent<UnityEngine.UI.Text>().text = moneyHandler.moneyChangeAmountsToday[i].ToString();
            }

            textMoneyChangeReason.transform.localScale = new Vector3(1, 1, 1);
            textMoneyChangeAmount.transform.localScale = new Vector3(1, 1, 1);
            imageMoneyChange.transform.localScale = new Vector3(1, 1, 1);

            LayoutRebuilder.ForceRebuildLayoutImmediate(panelMoneyChange.GetComponent<RectTransform>());

            panelMoneyChange.transform.localScale = new Vector3(1, 1, 1);
            LeanTween.alpha(panelMoneyChange.GetComponent<RectTransform>(), 1f, 0);


            yield return new WaitForSeconds(2f);

            tempMoneyTotal += moneyHandler.moneyChangeAmountsToday[i];
            textMoney.GetComponent<UnityEngine.UI.Text>().text = tempMoneyTotal.ToString();

            LeanTween.moveY(panelMoneyChange.GetComponent<RectTransform>(), panelMoneyChange.GetComponent<RectTransform>().localPosition.y - 200, 0.7f);
            LeanTween.alpha(panelMoneyChange.GetComponent<RectTransform>(), 0f, 0.7f);

            yield return new WaitForSeconds(2.5f);
            LeanTween.moveY(panelMoneyChange.GetComponent<RectTransform>(), panelMoneyChange.GetComponent<RectTransform>().localPosition.y + 200, 0);

        }

        //hide Money stuff
        textMoney.transform.localScale = new Vector3(0, 0, 0);
        imageMoney.transform.localScale = new Vector3(0, 0, 0);
        panelMoneyChange.transform.localScale = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(0.5f);

        //show alchemy title
        tempReactions = AlchemyReaction.reactionsDiscoveredToday;
        tempRecipesTodayAmount = 0;
        if (tempReactions.Count > 0)
        { 
        textRecipesToday.GetComponent<UnityEngine.UI.Text>().text = "New Recipes discovered: " + tempRecipesTodayAmount;

        LeanTween.alpha(panelRecipesToday.GetComponent<RectTransform>(), 1f, 0);
        textRecipesToday.transform.localScale = new Vector3(1, 1, 1);
        panelRecipesToday.transform.localScale = new Vector3(1, 1, 1);
        }

        //iterate through new recipes
        yield return new WaitForSeconds(1f);
        

        for (int i = 0; i < tempReactions.Count; i++)
        {
            source = GetComponent<AudioSource>();
            source.PlayOneShot(moneyHandler.moneySound, 1f);

            textRecipeSingle.GetComponent<UnityEngine.UI.Text>().text = tempReactions[i].outputIngredientTypes[0].ingredientName;
            imageRecipeSingle.GetComponent<UnityEngine.UI.Image>().sprite = tempReactions[i].outputIngredientTypes[0].inventorySprite;

            textRecipeSingle.transform.localScale = new Vector3(1, 1, 1);
            imageRecipeSingle.transform.localScale = new Vector3(1, 1, 1);
            

            LayoutRebuilder.ForceRebuildLayoutImmediate(panelRecipeSingle.GetComponent<RectTransform>());

            panelRecipeSingle.transform.localScale = new Vector3(1, 1, 1);
            LeanTween.alpha(panelRecipeSingle.GetComponent<RectTransform>(), 1f, 0);


            yield return new WaitForSeconds(2f);

            tempRecipesTodayAmount += 1;
            textRecipesToday.GetComponent<UnityEngine.UI.Text>().text = "New Recipes discovered: " + tempRecipesTodayAmount; 

            LeanTween.moveY(panelRecipeSingle.GetComponent<RectTransform>(), panelRecipeSingle.GetComponent<RectTransform>().localPosition.y - 200, 0.7f);
            LeanTween.alpha(panelRecipeSingle.GetComponent<RectTransform>(), 0f, 0.7f);

            yield return new WaitForSeconds(2.5f);
            LeanTween.moveY(panelRecipeSingle.GetComponent<RectTransform>(), panelRecipeSingle.GetComponent<RectTransform>().localPosition.y + 200, 0);

        }

        //hide Alchemy stuff
        textRecipesToday.transform.localScale = new Vector3(0, 0, 0);
        panelRecipeSingle.transform.localScale = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(1f);

        // timejump
        if (pTriggeredFromBed)
        gameTime.JumpToHourOfTheDay(wakeUpTime);

        //show new day
        textDay1.GetComponent<UnityEngine.UI.Text>().text = "Day " + (GameTime.daysSinceStart + 1);
        textDay1.transform.localScale = new Vector3(1, 1, 1);
        

        yield return new WaitForSeconds(2);

        StartCoroutine(SleepEnd());

    }

    IEnumerator SleepEnd()
    {
        moneyHandler.UpdateMoneyAtDayStart();
        AlchemyReaction.ClearTodaysReactions();

        textDay1.transform.localScale = new Vector3(0, 0, 0);

        LeanTween.alpha(panel.GetComponent<RectTransform>(), 0f, 1);

        yield return new WaitForSeconds(1);

        panel.transform.localScale = new Vector3(0, 0, 0);

        GameTime.timeIsStopped = false;

        
    }
}
