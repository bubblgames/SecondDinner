using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    [SerializeField]private float timeToRestart = 3f;

    void Update()
    {
        timeToRestart -= Time.deltaTime;
        if (timeToRestart < 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
