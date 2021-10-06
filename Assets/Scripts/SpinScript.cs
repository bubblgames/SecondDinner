using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpinScript : MonoBehaviour
{
    private int spinAmount = 0;
    void Update()
    {
        gameObject.transform.Rotate(0, 0, spinAmount);
        spinAmount++;

        if (spinAmount > 130)
        {
            SceneManager.LoadScene(1);
        }
    }
}
