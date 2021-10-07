using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinScoreScript : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI score;
    // Start is called before the first frame update
    void Start()
    {
        score.text = "Your Score: " + PlayerPrefs.GetInt("score", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
