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
    [SerializeField] private AudioClip doorOpen;
    [SerializeField] private Animator doorAnimator;
    private Vector2 _moveInput;
    private Rigidbody2D _rigidbody2D;
    private string currentMeal = "Any";
    private float _originalMoveSpeed;
    public bool canMove = true;
    private Animator _animator;
    private bool isLeaving = false;
    private float leaveTime = 2f;

    void Start()
    {
        _originalMoveSpeed = _moveSpeed;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (isLeaving)
        {
            leaveTime -= Time.deltaTime;
            if (leaveTime < 0)
            {
                SceneManager.LoadScene(4);
            }

            gameObject.transform.localScale -= new Vector3(0.004f, 0.004f, 0.004f);
        }
        else
        {
            Run();
        }
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
        if (isLeaving)
        {
            return;
        }
        
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Food")))
        {
            ConsumeFood(other);
        }
        
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Door")))
        {
            FindObjectOfType<GameSessionController>().SaveTotalScore();
            _animator.SetTrigger("isLeaving");
            doorAnimator.SetTrigger("open");
            AudioSource.PlayClipAtPoint(doorOpen, gameObject.transform.position);
            isLeaving = true;
            _rigidbody2D.velocity = new Vector2(0f, 0f);
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
        _animator.SetBool("isEating", true);
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
                gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0f);
            }
            _animator.SetTrigger("ateGood");
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
                gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0f);
            }
            _animator.SetTrigger("ateBad");
        }

        // Outro
        if (currentMeal.Equals("Any"))
        {
            FindObjectOfType<GameSessionController>().AteFood();
        }
        
        Destroy(other.gameObject);
    }
}
