using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    [SerializeField]private float timeToRestart = 7f;
    [SerializeField] private int sceneToLoad = 0;
    void Update()
    {
        timeToRestart -= Time.deltaTime;
        if (timeToRestart < 0)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
