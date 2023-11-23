using System;
using Shared.Movement;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private MovementHelper _movementHelper = new MovementHelper();
    public Vector3 movement;
    public float moveSpeed = 1f;
    public string direction;

    void Start()
    {
        this._rigidbody2D = GetComponent<Rigidbody2D>();
        this._animator = GetComponent<Animator>();
        this.movement = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void Update()
    {
        var isClick = Input.GetMouseButton(0);

        if (isClick) 
        {
            movement = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            movement.z = transform.position.z;
        }

        var result = this._movementHelper.GoTo(this.transform.position, this.movement, this.moveSpeed);

        this.transform.position = result.instantPosition;
        this.direction = result.direction;

        this._animator.SetBool("isRunning", result.isRunning);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        this._rigidbody2D.MovePosition(new Vector2(this.transform.position.x, this.transform.position.y));
        this.movement = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        this._rigidbody2D.MovePosition(new Vector2(this.transform.position.x, this.transform.position.y));
        this.movement = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }
}
