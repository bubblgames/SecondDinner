using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenScript : MonoBehaviour
{
    [SerializeField] private GameObject favoriteFood;
    [SerializeField] private GameObject badFoodOne;
    [SerializeField] private GameObject badFoodTwo;
    [SerializeField] private float minXVelocity;
    [SerializeField] private float maxXVelocity;
    [SerializeField] private float minYVelocity;
    [SerializeField] private float maxYVelocity;
    
    private int currentRound;
    void Start()
    {
    }

    void Update()
    {
    }

    public void StartServing(float speed)
    {
        InvokeRepeating("ServeFood", 0f, speed);
    }

    public void StopServing()
    {
        CancelInvoke("ServeFood");
    }

    void ServeFood()
    {
        GameObject nextFood = SelectNextFood();
        var createdFood = Instantiate(nextFood, transform.position, Quaternion.identity);

        createdFood.GetComponent<Rigidbody2D>().velocity = GenerateInitialVelocity();
    }

    private Vector2 GenerateInitialVelocity()
    {
        var launchY = Random.Range(minYVelocity, maxYVelocity);
        var launchX = Random.Range(minXVelocity, maxXVelocity);
        return new Vector2
        {
            x = launchX,
            y = launchY,
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
                return favoriteFood;
            case 3:
                return badFoodOne;
            case 4:
                return badFoodTwo;
            default:
                return favoriteFood;

        }
    }
}
