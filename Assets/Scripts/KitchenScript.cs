using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenScript : MonoBehaviour
{
    [SerializeField] private GameObject breakfastFood;
    [SerializeField] private GameObject lunchFood;
    [SerializeField] private GameObject dinnerFood;
    [SerializeField] private float launchXVelocity;
    [SerializeField] private float launchYVelocity;
    void Start()
    {
        
    }

    void Update()
    {
        ServeFood();
    }

    void ServeFood()
    {
        GameObject nextFood = SelectNextFood();
        var createdFood = Instantiate(nextFood, transform.position, Quaternion.identity);

        createdFood.GetComponent<Rigidbody2D>().velocity = GenerateInitialVelocity();
    }

    private Vector2 GenerateInitialVelocity()
    {
        var launchY = Random.Range(5f, 10f);
        var launchX = Random.Range(-7f, -0.5f);

        return new Vector2
        {
            x = launchX,
            y = launchY
        };
    }

    private GameObject SelectNextFood()
    {
        var foodType = Random.Range(0, 5);
        switch (foodType)
        {
            case 0:
            case 1:
            case 2:
                return breakfastFood;
            case 3:
                return lunchFood;
            case 4:
                return dinnerFood;
            default:
                return breakfastFood;

        }
    }
}
