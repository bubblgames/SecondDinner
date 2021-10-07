using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StillHungryTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI banner;
    [SerializeField] private TextMeshProUGUI scoreTotal;
    private List<string> words = new List<string>
    {
        "What's that?",
        "Still Hungry?",
        "Come on over.",
        "Save room for..",
        "Second Dinner",
        "-------->"
    };
    
    private float letterDuration = 0.07f;
    private int currentLetter = 0;
    private int currentWord = 0;
    private float wordDuration = 1.5f;
    
    void Start()
    {
        ShowWord();
        FindObjectOfType<FudController>().SetMoveable(false);
        SetScoreTotal(PlayerPrefs.GetInt("score", 0));
        FindObjectOfType<GameSessionController>().SetTotalScore(PlayerPrefs.GetInt("score", 0));
    }

    void ShowWord()
    {
        InvokeRepeating("ShowLetter", wordDuration, letterDuration);
    }

    void ShowLetter()
    {
        if (words[currentWord].Length == currentLetter)
        {
            CancelInvoke("ShowLetter");
            currentWord++;
            currentLetter = 0;
            if (words.Count != currentWord)
            {
                ShowWord();
            }
            else
            {
                FindObjectOfType<FudController>().SetMoveable(true);
                var kitchens = FindObjectsOfType<KitchenScript>();
                foreach (var kitchen in kitchens)
                {
                    kitchen.StartServing(0.2f);
                }
                InvokeRepeating("BlinkWord", 0, letterDuration*5);
            }
        }
        else
        {
            if (currentLetter == 0)
            {
                banner.text = "" + words[currentWord][currentLetter];
            }
            else
            {
                banner.text = banner.text + words[currentWord][currentLetter];
            }
            
            currentLetter++;
        }
    }

    void BlinkWord()
    {
        if (banner.isActiveAndEnabled)
        {
            banner.gameObject.SetActive(false);
        }
        else
        {
            banner.gameObject.SetActive(true);
        }
    }

    public void StopBlink()
    {
        CancelInvoke("BlinkWord");
        banner.gameObject.SetActive(false);
    }

    public void SetScoreTotal(int score)
    {
        scoreTotal.text = score.ToString();
    }
}
