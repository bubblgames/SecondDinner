using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FudController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _speedIncrement;
    [SerializeField] private AudioClip yumClip;
    [SerializeField] private AudioClip yuckClip;
    [SerializeField] private AudioSource effectsPlayer;
    private Vector2 _moveInput;
    private Rigidbody2D _rigidbody2D;
    private string currentMeal = "Any";
    private float _originalMoveSpeed;
    public bool canMove = true;
    

    void Start()
    {
        _originalMoveSpeed = _moveSpeed;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Run();
    }

    void Run()
    {
        if (canMove)
        {
            Vector2 playerVelocity = new Vector2
            {
                x = _moveInput.x * _moveSpeed,
                y = _rigidbody2D.velocity.y
            };
            _rigidbody2D.velocity = playerVelocity;
        }
    }

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    public void SetMoveable(bool movability)
    {
        canMove = movability;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Food")))
        {
            ConsumeFood(other);
        }
        
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Door")))
        {
            FindObjectOfType<GameSessionController>().SaveTotalScore();
            SceneManager.LoadScene(4);
        }

        if (other.tag.Equals("KitchenTrigger"))
        {
            FindObjectOfType<StillHungryTextController>().StopBlink();
        }
    }

    public void SetCurrentMeal(string meal)
    {
        currentMeal = meal;
        _moveSpeed = _originalMoveSpeed;
    }

    private void ConsumeFood(Collider2D other)
    {
        // Main Game
        if (other.tag.Equals(currentMeal))
        {
            AudioSource.PlayClipAtPoint(yumClip, gameObject.transform.position);
            _moveSpeed += _speedIncrement;
            FindObjectOfType<GameSessionController>().AteFood();
            if (gameObject.transform.localScale.x > 1)
            {
                gameObject.transform.localScale -= new Vector3(0.1f, 0f, 0f);
            }
        }
        else
        {
            AudioSource.PlayClipAtPoint(yuckClip, gameObject.transform.position);
            if (_moveSpeed > _originalMoveSpeed)
            {
                _moveSpeed = _originalMoveSpeed;
            }
            else if (_moveSpeed > 2)
            {
                _moveSpeed -= _speedIncrement;
            }

            if (gameObject.transform.localScale.x < 2.5)
            {
                gameObject.transform.localScale += new Vector3(0.1f, 0f, 0f);
            }
        }

        // Outro
        if (currentMeal.Equals("Any"))
        {
            FindObjectOfType<GameSessionController>().AteFood();
        }
        
        Destroy(other.gameObject);
    }
}
