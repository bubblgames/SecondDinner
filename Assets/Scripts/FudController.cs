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
}
