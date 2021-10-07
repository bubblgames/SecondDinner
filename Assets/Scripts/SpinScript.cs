using UnityEngine;

public class SpinScript : MonoBehaviour
{
    private float spinAmount = 0;

    void Update()
    {
        gameObject.transform.Rotate(0, 0, spinAmount);
        spinAmount += 0.05f;
    }
}
