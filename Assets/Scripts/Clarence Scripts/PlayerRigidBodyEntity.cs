using System;
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
    [Range(10, 30)]
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
    [Range(10, 30)]
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
    [Range(1, 10)]
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
    private bool holdFBase = false;
    private bool holdFuel = false;
    private bool holdMediBase = false;
    private bool holdMedikit = false;

    //Resurrect
    [Header("Resurrect")]
    public bool canResurrect = false;
    private PlayerRigidBodyEntity downedPlayer;

    //UI Above player : Gives information about his state or what he holds
    [Header("Player UI")]
    [SerializeField]
    private GameObject fuelBaseSprite;
    [SerializeField]
    private GameObject fuelJerrycanSprite;
    [SerializeField]
    private GameObject mediBaseSprite;
    [SerializeField]
    private GameObject medikitSprite;
    [SerializeField]
    private GameObject KOSprite;

    [Header("PlayerOxygen")]
    [SerializeField]
    private Image oxygenBar;
    private float oxygenAmount = 0;
    private float maxOxygenAmount = 100;
    private bool isOutside = true;
    public bool isDown = false;

    [SerializeField]
    private GameObject fuelBasePrefab;
    [SerializeField]
    private GameObject fuelJerrycanPrefab;
    [SerializeField]
    private GameObject MediBasePrefab;
    [SerializeField]
    private GameObject MedikitPrefab;
    public GameObject playerHands;

    public static List<PlayerRigidBodyEntity> playerList = new List<PlayerRigidBodyEntity>();


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

    public string reController;

    public static event Action onRepairs;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        myMesh = myRenderer.GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //rigidBody.gravityscale = 0f;
        playerList.Add(this);
        fuelBaseSprite.SetActive(false);
        fuelJerrycanSprite.SetActive(false);
        medikitSprite.SetActive(false);
        KOSprite.SetActive(false);
        oxygenBar.enabled = false;

        oxygenAmount = maxOxygenAmount;
        halfBox = new Vector3(0.5f, 0.5f, 0.5f);

        Physics.IgnoreLayerCollision(9, 9);
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
                transform.position = new Vector3(myTeleport.arrival.transform.position.x, myTeleport.arrival.transform.position.y, myTeleport.arrival.transform.position.z -2);
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

    

    private void Update()
    {
        oxygenBar.fillAmount = oxygenAmount / maxOxygenAmount;
        if (isOutside && !isDown)
        {
            LoseOxygen();
            if (oxygenBar.enabled) return;
            oxygenBar.enabled = true;
        }
        else if (!isOutside)
        {
            if (!oxygenBar.enabled) return;
            oxygenBar.enabled = false;
        }
    }

    #region Oxygen

    public void FillOxygen()
    {
        if (UIManager.instance.scubaTankAmount == 0) return;
        oxygenAmount = maxOxygenAmount;
        UIManager.instance.scubaTankAmount -= UIManager.instance._initialCost;
    }

    public void LoseOxygen()
    {
        oxygenAmount -= Time.deltaTime;
        if (oxygenAmount <= 0)
        {
            isDown = true;
            //la condition de défaite
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

        if (!isTeleporting)
        {
            CheckTeleport();
        }

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

    private void CheckTeleport()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward, Color.blue);

        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 6f))
        {
            if (hit.transform.tag == "RepairHub")
            {
                onRepairs?.Invoke();

            }
        }
    }

    private void StartTeleport()
    {
        isTeleporting = true;
        //myRenderer.GetComponent<MeshRenderer>().material.color = invisibleColor;

    }

    public void ActionOxygen()
    {
        if (!isDown)
        {
            if(UIManager.instance.scubaTankAmount > 0)
            {
                FillOxygen();
            }
            else
            {
                UIManager.instance.UIAnimator.SetTrigger("NoOxygen");
            }
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

        if (interactItem.gameObject.tag == "MovePanel")
        {
            interactItem.GetComponent<MovementPanel>().user = this;
            interactItem.GetComponent<MovementPanel>().OnUsed();
            isInteracting = true;
        }
        else if (interactItem.gameObject.tag == "GunPanel")
        {
            if (UIManager.instance.ammoAmount <= 0)
            {
                UIManager.instance.UIAnimator.SetTrigger("NoAmmo");
                return;
            }
            interactItem.GetComponent<GunPanel>().user = this;
            interactItem.GetComponent<GunPanel>().OnUsed();
            isInteracting = true;
        }
        else if (interactItem.gameObject.tag == "Tank")
        {
            
            FuelSystem.Instance.ReplenishFuel();
            
        }
        else if (interactItem.gameObject.tag == "CraftTable")
        {
            //interactItem.QTE(); -----> Lancer la fonction de QTE
            //interactQTE.StartQTE();
            if (UIManager.instance.scrapsAmount <= 0)
            {
                UIManager.instance.UIAnimator.SetTrigger("NoScraps");
                return;
            }

            Debug.Log("Lancemennt du QTE");
            interactItem.GetComponent<CraftTable>().user = this;
            interactItem.GetComponent<CraftTable>().OnUsed();
            Spinner.instance.LaunchWheel();
            isInteracting = true;
        }

        
    }

    private void ActionStopInteract()
    {
        if(!isInteracting) return;
        if (interactItem == null) return;
        

        if(interactItem.gameObject.tag == "MovePanel")
        {
            isInteracting = false;
            interactItem.GetComponent<MovementPanel>().OnDropped();
        }
        else if (interactItem.gameObject.tag == "GunPanel")
        {
            isInteracting = false;
            interactItem.GetComponent<GunPanel>().OnDropped();
        }
        else if (interactItem.gameObject.tag == "CraftTable")
        {
            //interactItem.QTE(); -----> Lancer la fonction de QTE
            //interactQTE.StartQTE();
            
            Debug.Log("Lancemennt du QTE");
            interactItem.GetComponent<CraftTable>().OnDropped();
            Spinner.instance.StopWheel();
            isInteracting = false;
        }
        
        else
        {
            if (interactQTE == null) return;

            Debug.Log("Fin du QTE, peut bouger à nouveau");
            isInteracting = false;
        }

        
    }


    private void ActionGrab()
    {
        if (ressourceItem == null) return;
        if (isGrabing) return;

        Debug.Log("Grab");
        //holdItem = ressourceItem;
        //ressourceItem = null;
        if(ressourceItem.gameObject.tag == "FuelBase")
        {
            holdFBase = true;
            holdFuel = false;
            holdMedikit = false;
            holdMediBase = false;

            fuelBaseSprite.SetActive(true);
            fuelJerrycanSprite.SetActive(false);
            medikitSprite.SetActive(false);
            mediBaseSprite.SetActive(false);
        }
        else if (ressourceItem.gameObject.tag == "GasCan")
        {
            holdFuel = true;
            holdFBase = false;
            holdMedikit = false;
            holdMediBase = false;

            fuelBaseSprite.SetActive(false);
            fuelJerrycanSprite.SetActive(true);
            medikitSprite.SetActive(false);
            mediBaseSprite.SetActive(false);
        }
        else if (ressourceItem.gameObject.tag == "MedicBase")
        {
            holdFuel = false;
            holdFBase = false;
            holdMedikit = false;
            holdMediBase = true;

            fuelBaseSprite.SetActive(false);
            fuelJerrycanSprite.SetActive(false);
            medikitSprite.SetActive(false);
            mediBaseSprite.SetActive(true);
        }
        else if (ressourceItem.gameObject.tag == "MedicKit")
        {
            holdMedikit = true;
            holdFBase = false;
            holdFuel = false;
            holdMediBase = false;

            fuelBaseSprite.SetActive(false);
            fuelJerrycanSprite.SetActive(false);
            medikitSprite.SetActive(true);
            mediBaseSprite.SetActive(false);
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
        if (holdFBase)
        {
            Debug.Log("Reposer");
            fuelBaseSprite.SetActive(false);
            GameObject newItem;
            newItem = Instantiate(fuelBasePrefab);
            newItem.transform.position = playerHands.transform.position;
            holdFBase = false;
        }
        else if (holdFuel)
        {
            fuelJerrycanSprite.SetActive(false);
            GameObject newItem;
            newItem = Instantiate(fuelJerrycanPrefab);
            newItem.transform.position = playerHands.transform.position;
            holdFuel = false;
        }
        else if (holdMediBase)
        {
            mediBaseSprite.SetActive(false);
            GameObject newItem;
            newItem = Instantiate(MediBasePrefab);
            newItem.transform.position = playerHands.transform.position;
            holdMediBase = false;
        }
        else if (holdMedikit)
        {
            medikitSprite.SetActive(false);
            if (!canResurrect)
            {
                GameObject newItem;
                newItem = Instantiate(MedikitPrefab);
                newItem.transform.position = playerHands.transform.position;
                holdMedikit = false;
            }
            else
            {
                ActionResurrect();
                holdMedikit = false;
            }
        }

        isGrabing = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        
        if(collision.gameObject.tag == "FuelBase" || collision.gameObject.tag == "GasCan" || collision.gameObject.tag == "MedicBase" || collision.gameObject.tag == "MedicKit")
        {
            canGrab = true;
            if(!isGrabing)
                ressourceItem = collision.gameObject;
        }
        else if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerRigidBodyEntity>().isDown && holdMedikit)
            {
                canResurrect = true;
                downedPlayer = collision.gameObject.GetComponent<PlayerRigidBodyEntity>();
            }
        }
        else if(collision.gameObject.tag == "MovePanel")
        {
            canInteractQTE = true;
            if (!isInteracting)
            {
                interactItem = collision.gameObject;
            }
        }
        else if(collision.gameObject.tag == "GunPanel")
        {
            canInteractQTE = true;
            if (!isInteracting)
            {
                interactItem = collision.gameObject;
            }
        }
        else if (collision.gameObject.tag == "CraftTable")
        {
            canInteractQTE = true;
            if (!isInteracting)
            {
                interactItem = collision.gameObject;
            }
        }
        else if (collision.gameObject.tag == "Tank")
        {
            canInteractQTE = true;
            if (!isInteracting)
            {
                interactItem = collision.gameObject;
            }
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "FuelBase" || collision.gameObject.tag == "GasCan" || collision.gameObject.tag == "MedicBase" || collision.gameObject.tag == "MedicKit")
        {
            canGrab = false;
            ressourceItem = null;
        }
        else if (collision.gameObject.tag == "Player" && holdMedikit)
        {
            if (collision.gameObject.GetComponent<PlayerRigidBodyEntity>().isDown)
            {
                canResurrect = false;
                downedPlayer = null;
            }
        }
        else if (collision.gameObject.tag == "MovePanel")
        {
            canInteractQTE = false;
            interactItem = null;
        }
        else if (collision.gameObject.tag == "GunPanel")
        {
            canInteractQTE = false;
            interactItem = null;
        }
        else if (collision.gameObject.tag == "CraftTable")
        {
            canInteractQTE = false;
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
        FindObjectOfType<SoundManager>().Play("Jumping");
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
