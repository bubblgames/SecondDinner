using System;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private bool isPlayingMainGame;
    private int totalScore = 0;
    
    private FudController _fudController;
    
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI roundTimeRemainingText;
    [SerializeField] private List<TextMeshProUGUI> scoreTexts;
    [SerializeField] private List<KitchenScript> kitchens;
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
        if (isPlayingMainGame)
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
        for (var i = 0; i <= currentRound; i++)
        {
            kitchens[i].StopServing();
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
            totalScore = roundInfos.Sum(roundInfo => roundInfo.score);
            isPlayingMainGame = false;
            SaveTotalScore();
            SceneManager.LoadScene(2);
            _fudController.SetCurrentMeal("Any");
        }
        else
        {
            mealIsOnGoing = true;
            mealTimeRemaining = mealDuration;
            roundText.text = roundInfos[currentRound].name;
            roundText.color = roundInfos[currentRound].color;
            _fudController.SetCurrentMeal(roundInfos[currentRound].name);
            for (var i = 0; i <= currentRound; i++)
            {
                kitchens[i].StartServing((currentRound - i + 1) * 0.2f);
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

    public void AteFood()
    {
        if (isPlayingMainGame)
        {
            roundInfos[currentRound].score += 1;
            scoreTexts[currentRound].text = roundInfos[currentRound].score.ToString();
        }
        else
        {
            if (totalScore > 1)
            {
                totalScore--;
                FindObjectOfType<StillHungryTextController>().SetScoreTotal(totalScore);
            }
            else
            {
                SceneManager.LoadScene(3);
            }
        }
    }

    public void SetTotalScore(int score)
    {
        totalScore = score;
    }

    public void SaveTotalScore()
    {
        PlayerPrefs.SetInt("score", totalScore);
    }
}
