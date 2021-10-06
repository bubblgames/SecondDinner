using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSessionController : MonoBehaviour
{
    [SerializeField] private int restDuration = 2;
    private float restTimeRemaining = 0;
    
    [SerializeField] private float mealDuration;
    private bool mealIsOnGoing = false;
    private float mealTimeRemaining = 0;
    private bool servingIsOnGoing = false;
    
    private FudController _fudController;
    
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI roundTimeRemainingText;
    [SerializeField] private List<TextMeshProUGUI> scoreTexts;
    private List<RoundInfo> roundInfos = new List<RoundInfo>
    {
        new RoundInfo
        {
            name = "Breakfast",
            color = Color.blue,
            score = 0,
        },
        new RoundInfo
        {
            name = "Lunch",
            color = Color.green,
            score = 0
        },
        new RoundInfo
        {
            name = "Dinner",
            color = Color.red,
            score = 0
        }
    };
    private int currentRound = 0;
   
    void Start()
    {
        restTimeRemaining = restDuration;
        _fudController = FindObjectOfType<FudController>();
    }

    void Update()
    {
        if (mealIsOnGoing)
        {
            WaitForMealToEnd();
        }
        else
        {
            WaitToStartMeal();
        }
    }

    void WaitForMealToEnd()
    {
        if (mealTimeRemaining > 0)
        {
            mealTimeRemaining -= Time.deltaTime;
            DisplayTime();
        }
        else if (servingIsOnGoing)
        {
            StopServing();
        }
        else if (FindObjectsOfType<FoodScript>().Length == 0)
        {   
            EndRound();
        }
    }

    void StopServing()
    {
        roundTimeRemainingText.text = "ROUND OVER!";
        var kitchens = FindObjectsOfType<KitchenScript>();
        foreach (var kitchen in kitchens)
        {
            kitchen.StopServing();
        }

        servingIsOnGoing = false;
    }

    void WaitToStartMeal()
    {
        if (restTimeRemaining <= 0)
        {
            StartMeal();
        }
        else
        {
            restTimeRemaining -= Time.deltaTime;
        }
    }
    
    void DisplayTime()
    {
        roundTimeRemainingText.text = "Time Left: " + mealTimeRemaining.ToString("#.000");
    }

    void StartMeal()
    {
        if (currentRound == roundInfos.Count)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            mealIsOnGoing = true;
            mealTimeRemaining = mealDuration;
            roundText.text = roundInfos[currentRound].name;
            roundText.color = roundInfos[currentRound].color;
            _fudController.SetCurrentMeal(roundInfos[currentRound].name);
            var kitchens = FindObjectsOfType<KitchenScript>();
            foreach (var kitchen in kitchens)
            {
                kitchen.StartServing();
            }
            servingIsOnGoing = true;
        }
    }

    void EndRound()
    {
        mealIsOnGoing = false;
        currentRound++;
        restTimeRemaining = restDuration;
    }

    public void IncreaseScore(int scoreAmount)
    {
        roundInfos[currentRound].score += scoreAmount;
        scoreTexts[currentRound].text = roundInfos[currentRound].score.ToString();
    }
}