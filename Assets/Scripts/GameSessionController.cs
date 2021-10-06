using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSessionController : MonoBehaviour
{
    [SerializeField] private int numberOfRounds;
    [SerializeField] private int timeBeforeRound;
    [SerializeField] private float roundDuration;
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI roundTimeRemainingText;
    
    private bool roundTimerIsRunning;
    private float roundTimeRemaining;
    void Start()
    {
        StartRound();
        roundTimerIsRunning = true;
        roundTimeRemaining = roundDuration;
    }

    void Update()
    {
        if (roundTimerIsRunning)
        {
            if (roundTimeRemaining > 0)
            {
                roundTimeRemaining -= Time.deltaTime;
                DisplayTime();
            }
            else
            {
                roundTimerIsRunning = false;
                roundTimeRemainingText.text = "ROUND OVER!";
                FindObjectOfType<KitchenScript>().StopServing();
            }
        }
    }
    
    void DisplayTime()
    {
        roundTimeRemainingText.text = "Time Left: " + roundTimeRemaining.ToString("#.000");
    }

    void StartRound()
    {
        FindObjectOfType<KitchenScript>().StartServing();
        roundText.text = "Breakfast!";
        roundText.color = Color.blue;
    }
}
