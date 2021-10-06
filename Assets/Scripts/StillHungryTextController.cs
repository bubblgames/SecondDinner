using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StillHungryTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI banner;

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
    }

    void ShowWord()
    {
        InvokeRepeating("ShowLetter", wordDuration, letterDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
