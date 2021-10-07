using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinScoreScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    void Start()
    {
        score.text = "Your Score: " + PlayerPrefs.GetInt("score", 0);
    }
}
