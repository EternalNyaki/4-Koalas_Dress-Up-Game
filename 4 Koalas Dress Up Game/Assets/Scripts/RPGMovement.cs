using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGMovement : MonoBehaviour
{
    //Movement speed (in units per second)
    public float moveSpeed;

    //Vector for storing inputs
    protected Vector2 _inputVector;

    private int _horizontalHash, _verticalHash, _speedHash;

    protected Rigidbody2D _rb2d;
    protected Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

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
            _animator.SetFloat(_horizontalHash, Input.GetAxisRaw("Horizontal"));
            _animator.SetFloat(_verticalHash, Input.GetAxisRaw("Vertical"));
        }

        _animator.SetFloat(_speedHash, _inputVector.magnitude * moveSpeed);
    }

    void FixedUpdate()
    {
        Move();
    }

    //Override this method for custom movement logic
    //Evaluated on physics update
    protected virtual void Move()
    {
        _rb2d.MovePosition((Vector2)transform.position + _inputVector * moveSpeed * Time.deltaTime);
    }
}
