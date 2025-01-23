using UnityEngine;

public class PlayerMovement : IPlayerContextUpdate, IPlayerContextFixedUpdate
{
    private float _moveSpeed;
    private float _acceleration;
    private float _moveVectorDecreace;

    private Vector2 _inputVector;
    private Vector2 _moveVector;

    private Rigidbody2D _rigidbody;
    private IPlayerInputHandler _inputHandler;
    private Animator _animator;

    public void Init(PlayerConfig config, IPlayerInputHandler inputHandler, Rigidbody2D rigidbody, Animator animator) 
    {
        _rigidbody = rigidbody;
        _inputHandler = inputHandler;
        _animator = animator;

        _moveSpeed = config.MoveSpeed;
        _acceleration = config.Acceleration;
        _moveVectorDecreace = config.MoveVectorDecreace;
    }

    public bool MovingInputed => _inputVector.x != 0 || _inputVector.y != 0;

    public void OnUpdate() 
    {
        _inputVector = _inputHandler.GetMovement();

        if (MovingInputed)
        {
            Vector2 nVec = new Vector2(_inputVector.x, _inputVector.y).normalized;
            _moveVector = Vector2.ClampMagnitude(_moveVector + nVec * _acceleration, _moveSpeed);
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _moveVector *= _moveVectorDecreace;
            _animator.SetBool("IsWalking", false);
        }
    }

    public void OnFixedUpdate()
    {
        _rigidbody.velocity = _moveVector;
    }
}
