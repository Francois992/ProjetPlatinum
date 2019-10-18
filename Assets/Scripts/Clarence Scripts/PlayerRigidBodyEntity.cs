using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerRigidBodyEntity : MonoBehaviour
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

    //ActionBool
    [Header("Actions")]
    public bool canGrab = false;
    public bool canInteractQTE = false;

    //Interact
    [Header("Interact")]
    public bool isInteracting = false;
    public GameObject interactItem;
    private InteractItem interactQTE;

    //Grab
    [Header("Grab")]
    public bool isGrabing = false;
    public GameObject ressourceItem;
    public GameObject holdItem;
    public GameObject playerHands;


    //Debug
    [Header("Debug")]
    public bool debugMode = false;

    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //rigidBody.gravityscale = 0f;
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

        Vector3 velocity = transform.position;
        velocity.x = _speed * _orientX;
        velocity.y = _verticalSpeed;
        rigidBody.velocity = velocity;

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
        }

        else if(_speed > 0f)
        {
            _speed -= _GetFriction() * Time.fixedDeltaTime;
            if(_speed < 0f)
            {
                _speed = 0f;
            }
        }
    }

    public void Move(float dirX)
    {
        _dirX = dirX;
        if(_orientX > 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        }
        else if(_orientX < 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
        }
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

    #region Action Commands
    public void Actions()
    {
        if (canGrab && !isGrabing)
        {
            ActionGrab();
            return;
        }
            

        if (isGrabing)
        {
            ActionUngrab();
            return;
        }

        if(canInteractQTE && !isInteracting)
        {
            ActionInteract();
            return;
        }

        if (isInteracting)
        {
            ActionStopInteract();
            return;
        }
            
    }

    private void ActionInteract()
    {
        if (interactItem == null) return;
        if (isInteracting) return;

        //interactItem.QTE(); -----> Lancer la fonction de QTE
        interactQTE.StartQTE();
        Debug.Log("Lancemennt du QTE");
        isInteracting = true;
    }

    private void ActionStopInteract()
    {
        if(!isInteracting) return;
        if (interactItem == null) return;
        if (interactQTE == null) return;

        Debug.Log("Fin du QTE, peut bouger à nouveau");
        interactQTE.EndQTE();
        isInteracting = false;
    }


    private void ActionGrab()
    {
        if (ressourceItem == null) return;
        if (isGrabing) return;

        Debug.Log("Grab");
        holdItem = ressourceItem;
        ressourceItem = null;
        holdItem.transform.position = playerHands.transform.position;
        holdItem.transform.parent = playerHands.transform;
        isGrabing = true;
        
    }

    private void ActionUngrab()
    {
        if (holdItem == null) return;
        if (!isGrabing) return;

        holdItem.transform.parent = null;
        holdItem = null;
        isGrabing = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "RessourceItem")
        {
            canGrab = true;
            if(!isGrabing)
                ressourceItem = collision.gameObject;
        }
        else if(collision.gameObject.tag == "InteractItem")
        {
            canInteractQTE = true;
            if (!isInteracting)
            {
                interactItem = collision.gameObject;
                interactQTE = collision.gameObject.GetComponent<InteractItem>();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "RessourceItem")
        {
            canGrab = false;
            ressourceItem = null;
        }
        else if (collision.gameObject.tag == "InteractItem")
        {
            canInteractQTE = false; ;
            interactQTE = null;
            interactItem = null;
        }
    }
    #endregion

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
