using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{

    [System.Serializable]
    public class FrictionSettings
    {
        public float friction = 30f;
        public float turnAroundFriction = 30f;
    }

    //Movement
    [Header("Movement")]
    public float acceleration = 20f;
    [Range(0f, 30f)]
    public float speedMax = 10f;
    private float _speed = 0f;
    public FrictionSettings groundFriction;
    public FrictionSettings airFriction;

    //Direction & Orientation
    private float _orientX = 0f;
    private float _dirX = 0f;

    //Gravity
    [Header("Gravity")]
    public float gravity = 20f;
    public float fallSpeedMax = 10f;
    private float _verticalSpeed = 0f;

    //Ground
    [Header("Ground")]
    public float groundY = 0f;
    public bool _isGrounded = false;


    //Jump
    [Header("Jump")]
    public float jumpSpeed = 5f;
    public float jumpDuration = 0.3f;
    private float _jumpCountdown = 0.1f;
    public bool _isJumping = false;


    //Debug
    [Header("Debug")]
    public bool debugMode = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isJumping)
        {
            _UpdateJump();
        }
        else
        {
            _UpdateGroundCheck();
            _UpdateGravity();
        }
        _UpdateMove();
        _UpdatePosition();
        
    }

    #region Move

    private void _UpdateMove()
    {
        if (_dirX != 0)
        {
            if(_dirX * _orientX <= 0f)
            {
                _speed -= _GetTurnAroundFriction() * Time.fixedDeltaTime;
                if(_speed <= 0f)
                {
                    _orientX = Mathf.Sign(_dirX);
                    _speed = 0f;
                }  
            }
            else
            {
                _speed += acceleration * Time.fixedDeltaTime;
                if (_speed >= speedMax)
                {
                    _speed = speedMax;
                }
                _orientX = Mathf.Sign(_dirX);
            }
            _UpdatePosition();
        }

        else if(_speed > 0f)
        {
            _speed -= _GetFriction() * Time.fixedDeltaTime;
            if(_speed < 0f)
            {
                _speed = 0f;
            }
        }
        _UpdatePosition();
    }

    public void Move(float dirX)
    {
        _dirX = dirX;
    }
    #endregion

    #region Friction Parameters
    private float _GetFriction()
    {
        return _isGrounded ? groundFriction.friction : airFriction.friction;
    }

    private float _GetTurnAroundFriction()
    {
        return _isGrounded ? groundFriction.turnAroundFriction : airFriction.turnAroundFriction;
    }
    #endregion

    private void _UpdatePosition()
    {
        Vector3 newPos = transform.position;
        newPos.x += _speed * Time.fixedDeltaTime * _orientX;
        newPos.y += _verticalSpeed * Time.fixedDeltaTime;
        transform.position = newPos;
    }

    #region Gravity
    private void _UpdateGravity()
    {
        if (_isGrounded) return;

        _verticalSpeed -= gravity * Time.fixedDeltaTime;
        if(_verticalSpeed < -fallSpeedMax)
        {
            _verticalSpeed = -fallSpeedMax;
        }
    }
    #endregion

    #region Ground
    public bool IsOnGround()
    {
        return _isGrounded;
    }
    private void _UpdateGroundCheck()
    {
        if(transform.position.y <= groundY)
        {
            _isGrounded = true;
            _verticalSpeed = 0;
            Vector3 newPos = transform.position;
            newPos.y = groundY;
            transform.position = newPos;
        }
        else
        {
            _isGrounded = false;
        }
    }
    #endregion

    #region Jump
    public void Jump()
    {
        if (_isJumping) return;

        _isJumping = true;
        _jumpCountdown = jumpDuration;
        _isGrounded = false;
    }

    public void StopJump()
    {
        _isJumping = false;
    }

    private void _UpdateJump()
    {
        if (!_isJumping) return;

        _jumpCountdown -= Time.fixedDeltaTime;
        if(_jumpCountdown <= 0f)
        {
            _isJumping = false;
        }
        else
        {
            _verticalSpeed = jumpSpeed;
        }
    }


    #endregion

    #region Debug
    private void OnGUI()
    {
        if (!debugMode) return;

        GUILayout.BeginVertical();
        GUILayout.Label("Speed = " + _speed);
        GUILayout.Label("Vertical Speed = " + _verticalSpeed);
        GUILayout.Label(_isGrounded ? "OnGround" : "InAir");
        GUILayout.EndVertical();

    }
    #endregion

}
