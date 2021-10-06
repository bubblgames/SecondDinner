using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FudController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _speedIncrement;
    private Vector2 _moveInput;
    private Rigidbody2D _rigidbody2D;
    private GameSessionController _gameSessionController;
    private string currentMeal;
    private float _originalMoveSpeed;

    void Start()
    {
        _originalMoveSpeed = _moveSpeed;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _gameSessionController = FindObjectOfType<GameSessionController>();
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

    public void SetCurrentMeal(string meal)
    {
        currentMeal = meal;
        _moveSpeed = _originalMoveSpeed;
    }

    private void ConsumeFood(Collider2D other)
    {
        if (other.tag.Equals(currentMeal))
        {
            _moveSpeed += _speedIncrement;
            _gameSessionController.IncreaseScore(1);
        }
        else
        {
            _moveSpeed -= _speedIncrement;
        }
        Destroy(other.gameObject);
        
        // adjust speed
    }
}
