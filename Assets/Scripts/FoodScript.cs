using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodScript : MonoBehaviour
{
    private float rotationAmount;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rotationAmount = Random.Range(-20, 20);
    }

    void Update()
    {
        if (gameObject.transform.position.y < -10)
        {
            Destroy(gameObject);
        }
        
        gameObject.transform.Rotate(0, 0, rotationAmount);
    }
}
