using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FudController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Vector2 _moveInput;
    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Run();
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2
        {
            x = _moveInput.x * _moveSpeed,
            y = _rigidbody2D.velocity.y
        };
        _rigidbody2D.velocity = playerVelocity;
    }

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Food")))
        {
            ConsumeFood(other);
        }
    }

    private void ConsumeFood(Collider2D other)
    {
        Destroy(other.gameObject);
        // determine food quality
        // add score
        // adjust speed
    }
}
