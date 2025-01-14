using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGMovement : MonoBehaviour
{
    //Movement speed (in units per second)
    public float moveSpeed;

    //Vector for storing inputs
    private Vector2 _inputVector;

    private int k_horizontalHash, k_verticalHash, k_speedHash;

    private Rigidbody2D _rb2d;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        k_horizontalHash = Animator.StringToHash("Horizontal");
        k_verticalHash = Animator.StringToHash("Vertical");
        k_speedHash = Animator.StringToHash("Speed");
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat(k_horizontalHash, Input.GetAxisRaw("Horizontal"));
        _animator.SetFloat(k_verticalHash, Input.GetAxisRaw("Vertical"));

        _inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        _animator.SetFloat(k_speedHash, _inputVector.magnitude * moveSpeed);
    }

    void FixedUpdate()
    {
        Move();
    }

    protected void Move()
    {
        _rb2d.MovePosition((Vector2)transform.position + _inputVector * moveSpeed * Time.deltaTime);
    }
}
