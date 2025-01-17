using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private float _moveSpeed;
    private float _acceleration;
    private float _moveVectorDecreace;

    private Vector2 _inputVector;
    private Vector2 _moveVector;

    private Rigidbody2D _rigidbody;
    private IPlayerInputHandler _inputHandler;

    public void Init(PlayerConfig config, IPlayerInputHandler inputHandler) 
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputHandler = inputHandler;

        _moveSpeed = config.MoveSpeed;
        _acceleration = config.Acceleration;
        _moveVectorDecreace = config.MoveVectorDecreace;
    }

    public bool MovingInputed => _inputVector.x != 0 || _inputVector.y != 0;

    private void Update()
    {
        _inputVector = _inputHandler.GetMovement();

        if (MovingInputed)
        {
            Vector2 nVec = new Vector2(_inputVector.x, _inputVector.y).normalized;
            _moveVector = Vector2.ClampMagnitude(_moveVector + nVec * _acceleration, _moveSpeed);
        }
        else
        {
            _moveVector *= _moveVectorDecreace;
        }
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = _moveVector;
    }
}
