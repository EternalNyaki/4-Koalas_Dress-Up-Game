using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGMovement : MonoBehaviour
{
    //Movement speed (in units per second)
    public float moveSpeed;

    //Vector for storing inputs
    protected Vector2 _inputVector;

    protected bool _isMoving;
    protected bool _wasMoving;

    private int _horizontalHash, _verticalHash, _speedHash;

    protected Rigidbody2D _rb2d;
    protected Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        //Get components
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        //Set animator hashes
        _horizontalHash = Animator.StringToHash("Horizontal");
        _verticalHash = Animator.StringToHash("Vertical");
        _speedHash = Animator.StringToHash("Speed");
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
    }

    protected virtual void GetInputs()
    {
        _inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        //Freeze direction if no input
        if (_inputVector.magnitude > 0f)
        {
            _isMoving = true;

            _animator.SetFloat(_horizontalHash, Input.GetAxisRaw("Horizontal"));
            _animator.SetFloat(_verticalHash, Input.GetAxisRaw("Vertical"));
        }
        else
        {
            _isMoving = false;
        }

        _animator.SetFloat(_speedHash, _inputVector.magnitude * moveSpeed);

        if (_isMoving && !_wasMoving)
        {
            OnStartMove();
        }
        if (!_isMoving && _wasMoving)
        {
            OnEndMove();
        }

        _wasMoving = _isMoving;
    }

    //TODO: Comment this shit
    protected virtual void OnStartMove() { }

    protected virtual void OnEndMove() { }

    void FixedUpdate()
    {
        if (_inputVector.magnitude > 0f)
        {
            Move();
        }
    }

    //Override this method for custom movement logic
    //Evaluated on physics update
    protected virtual void Move()
    {
        _rb2d.MovePosition((Vector2)transform.position + _inputVector * moveSpeed * Time.deltaTime);
    }
}
