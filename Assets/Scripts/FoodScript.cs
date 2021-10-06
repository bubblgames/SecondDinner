using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
        {
            Destroy(gameObject);
        }
    }
}
