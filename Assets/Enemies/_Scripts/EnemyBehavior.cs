using UnityEngine;
using Shared.Movement;
using System;

public class EnemyWalking : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private MovementHelper _movementHelper = new MovementHelper();
    private bool _isMouseOnMe = false;
    private bool _clikedOnMe = false;
    public Vector3 movement;
    public float moveSpeed = 1f;
    public float waitingTime = 3.0f;
    public bool isTimerFlagged = true;
    public bool isMoveEnable = true;
    public GameObject selector;
    public string direction;

    void Start() 
    {
        this._rigidbody2D = GetComponent<Rigidbody2D>();
        this._animator = GetComponent<Animator>();
        this.movement = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    private void OnMouseOver() 
    {
        _isMouseOnMe = true;
    }

    private void OnMouseExit()
    {
        _isMouseOnMe = false;
    }

    private void OnMouseDown()
    {
        _clikedOnMe = true;    
    }

    void Update()
    {
        if (isTimerFlagged && waitingTime <= 0f)
        {
            float[] directions = {
                this.transform.position.x + UnityEngine.Random .Range(-10f, 10f), 
                this.transform.position.y + UnityEngine.Random.Range(-10f, 10f),
                transform.position.z
            };
            this.movement = new Vector3(directions[0], directions[1], directions[2]);
            this.waitingTime = 3.0f;
        }
        else if (isTimerFlagged && waitingTime > 0f) 
        {
            waitingTime -= Time.deltaTime * 1.0f;
        }

        if (!_isMouseOnMe && Input.GetMouseButtonDown(0))
        {
            _clikedOnMe = false;
        }

        var result = this._movementHelper.GoTo(this.transform.position, this.movement, this.moveSpeed);

        this.transform.position = result.instantPosition;
        this.direction = result.direction;
        float direction;
        if (
            this.direction == "W" ||
            this.direction == "SW" ||
            this.direction == "NW"
        ) {
            direction = 2;
        }
        else
        {
            direction = 1;
        }

        if (this.direction == "STOP" || !isMoveEnable)
        {
            direction = -1;
        }

        this._animator?.SetFloat("isRunning", direction);
        this.selector?.SetActive(_isMouseOnMe || _clikedOnMe);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        this._rigidbody2D.MovePosition(new Vector2(this.transform.position.x, this.transform.position.y));
        this.movement = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        this._rigidbody2D.MovePosition(new Vector2(this.transform.position.x, this.transform.position.y));
        this.movement = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }

    private void Player_OnPlayerMoveEvent(object sender, System.EventArgs eventArgs)
    {
        this.selector.SetActive(false);
    }
}
