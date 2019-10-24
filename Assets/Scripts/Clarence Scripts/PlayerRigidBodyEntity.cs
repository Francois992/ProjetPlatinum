using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerRigidBodyEntity : MonoBehaviour
{
    //Clarence Berard

    [System.Serializable]
    public class FrictionSettings
    {
        public float friction = 30f;
        public float turnAroundFriction = 30f;
    }

    [SerializeField]
    private Transform visualCube;

    //Movement
    [Header("Movement")]
    public float acceleration = 20f;
    [Range(0f, 30f)]
    public float speedMax = 10f;
    public float _speed = 0f;
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
    private Vector3 halfBox;


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
    private bool holdItem1 = false;
    private bool holdItem2 = false;
    private bool holdItem3 = false;

    //Resurrect
    [Header("Resurrect")]
    public bool canResurrect = false;
    private PlayerRigidBodyEntity downedPlayer;

    //UI Above player : Gives information about his state or what he holds
    [Header("Player UI")]
    [SerializeField]
    private GameObject spriteItem1;
    [SerializeField]
    private GameObject spriteItem2;
    [SerializeField]
    private GameObject spriteItem3;
    [SerializeField]
    private GameObject KOSprite;

    [Header("PlayerOxygen")]
    [SerializeField]
    private Image oxygenBar;
    private float oxygenAmount = 0;
    private float maxOxygenAmount = 100;
    public bool isOutside = false;
    public bool isDown = false;

    [SerializeField]
    private GameObject prefabItem1;
    [SerializeField]
    private GameObject prefabItem2;
    [SerializeField]
    private GameObject prefabItem3;
    public GameObject playerHands;


    //Debug
    [Header("Debug")]
    public bool debugMode = false;

    private Rigidbody rigidBody;

    //Teleport
    private Teleporter myTeleport;

    public GameObject myRenderer;
    private MeshRenderer myMesh;
    private bool isTeleporting = false;
    private int teleportCountDown = 60;

    public Color invisibleColor;
    private Color fullColor;

    private float elapsedTime;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        myMesh = myRenderer.GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //rigidBody.gravityscale = 0f;
        spriteItem1.SetActive(false);
        spriteItem2.SetActive(false);
        spriteItem3.SetActive(false);
        KOSprite.SetActive(false);
        oxygenBar.enabled = false;

        oxygenAmount = maxOxygenAmount;
        halfBox = new Vector3(0.5f, 0.5f, 0.5f);

       
        fullColor = myMesh.material.color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isTeleporting)
        {
            elapsedTime += Time.deltaTime;

            teleportCountDown--;

            myMesh.material.color = Color.Lerp(myMesh.material.color, invisibleColor, elapsedTime/0.5f );

            if(elapsedTime >= 0.5)
            {
                myMesh.material.color = Color.Lerp(invisibleColor, fullColor, elapsedTime / 1f);
                transform.position = new Vector3(myTeleport.arrival.transform.position.x, myTeleport.arrival.transform.position.y, transform.position.z);
            }

            if (teleportCountDown <= 0)
            {
                isTeleporting = false;
                teleportCountDown = 60;
                elapsedTime = 0f;
                //transform.position = new Vector3(myTeleport.arrival.transform.position.x, myTeleport.arrival.transform.position.y, transform.position.z);

                myMesh.material.color = fullColor;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                RaycastHit hit;

                Debug.DrawRay(transform.position, transform.forward, Color.blue);

                if (Physics.Raycast(transform.position, Vector3.forward, out hit, 5f))
                {
                    if (hit.transform.tag == "Teleporter")
                    {
                        myTeleport = hit.transform.GetComponent<Teleporter>();
                        StartTeleport();
                        //transform.position = hit.transform.GetComponent<Teleporter>().arrival.transform.position;
                    }
                }
            }

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

        

    }

    private void StartTeleport()
    {
        isTeleporting = true;
        //myRenderer.GetComponent<MeshRenderer>().material.color = invisibleColor;
        
    }

    private void Update()
    {
        oxygenBar.fillAmount = oxygenAmount / maxOxygenAmount;
        if (isOutside && !isDown)
        {
            LoseOxygen();
            if (oxygenBar.enabled) return;
            oxygenBar.enabled = true;
        }
    }

    #region Oxygen

    public void LoseOxygen()
    {
        oxygenAmount -= Time.deltaTime * 5;
        if (oxygenAmount <= 0)
        {
            isDown = true;
            oxygenAmount = 0;
            KOSprite.SetActive(true);
        }
        else
        {
            isDown = false;
            KOSprite.SetActive(false);
        }
    }

    #endregion

    #region Move

    private void _UpdateMove()
    {
        if (isDown) return;
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
        if(isDown || isInteracting)
        {
           // _speed = 0;
        }
        if(_orientX > 0)
        {
            visualCube.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        }
        else if(_orientX < 0)
        {
            visualCube.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
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

    public void ActionResurrect()
    {
        if (downedPlayer == null) return;
        if (canResurrect)
        {
            downedPlayer.IsResurrected();
            downedPlayer = null;
            canResurrect = false;
        }
    }

    public void IsResurrected()
    {
        oxygenAmount = maxOxygenAmount / 2;
        isDown = false;
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
        //holdItem = ressourceItem;
        //ressourceItem = null;
        if(ressourceItem.gameObject.tag == "RessourceItem")
        {
            holdItem1 = true;
            holdItem2 = false;
            holdItem3 = false;

            spriteItem1.SetActive(true);
            spriteItem2.SetActive(false);
            spriteItem3.SetActive(false);
        }
        else if (ressourceItem.gameObject.tag == "RessourceItem2")
        {
            holdItem2 = true;
            holdItem1 = false;
            holdItem3 = false;

            spriteItem1.SetActive(false);
            spriteItem2.SetActive(true);
            spriteItem3.SetActive(false);
        }
        else if (ressourceItem.gameObject.tag == "RessourceItem3")
        {
            holdItem3 = true;
            holdItem1 = false;
            holdItem2 = false;

            spriteItem1.SetActive(false);
            spriteItem2.SetActive(false);
            spriteItem3.SetActive(true);
        }
        Destroy(ressourceItem.gameObject);
        //holdItem.transform.position = playerHands.transform.position;
        //holdItem.transform.parent = playerHands.transform;
        canGrab = false;
        isGrabing = true;
        
    }

    private void ActionUngrab()
    {
        //if (holdItem == null) return;
        if (!isGrabing) return;

        /*holdItem.transform.parent = null;
        holdItem = null;*/
        if (holdItem1)
        {
            Debug.Log("Reposer");
            spriteItem1.SetActive(false);
            GameObject newItem;
            newItem = Instantiate(prefabItem1);
            newItem.transform.position = playerHands.transform.position;
            holdItem1 = false;
        }
        else if (holdItem2)
        {
            spriteItem2.SetActive(false);
            GameObject newItem;
            newItem = Instantiate(prefabItem2);
            newItem.transform.position = playerHands.transform.position;
            holdItem2 = false;
        }
        else if (holdItem3)
        {
            spriteItem3.SetActive(false);
            if (!canResurrect)
            {
                GameObject newItem;
                newItem = Instantiate(prefabItem3);
                newItem.transform.position = playerHands.transform.position;
                holdItem3 = false;
            }
            else
            {
                ActionResurrect();
                holdItem3 = false;
            }
        }

        isGrabing = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "RessourceItem" || collision.gameObject.tag == "RessourceItem2" || collision.gameObject.tag == "RessourceItem3")
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
        else if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerRigidBodyEntity>().isDown && holdItem3)
            {
                canResurrect = true;
                downedPlayer = collision.gameObject.GetComponent<PlayerRigidBodyEntity>();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "RessourceItem" || collision.gameObject.tag == "RessourceItem2" || collision.gameObject.tag == "RessourceItem3")
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
        else if (collision.gameObject.tag == "Player" && holdItem3)
        {
            if (collision.gameObject.GetComponent<PlayerRigidBodyEntity>().isDown)
            {
                canResurrect = false;
                downedPlayer = null;
            }
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
        RaycastHit raycastHit;
        Vector3 raycastOrigin = transform.position;
        raycastOrigin.y += 0.005f;
        if(Physics.BoxCast(raycastOrigin, halfBox, Vector3.down,out raycastHit, transform.rotation, 0.01f))
        {
            if (raycastHit.collider != null)
            {
                _isGrounded = true;
                _verticalSpeed = 0;
                Vector3 newPos = transform.position;
                //newPos.y = hit.transform.position.y;
                transform.position = newPos;
            }
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
