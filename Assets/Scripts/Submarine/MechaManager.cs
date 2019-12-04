using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made by Francois Dessarts
public class MechaManager : MonoBehaviour
{
    private static MechaManager _instance = null;

    public static MechaManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }

    public float _dirX;
    public float _dirY;

    public bool isActivated = true;

    private float _orientX = 0f;
    private float _orientY = 0f;

    public float accelerationX = 2f;
    public float accelerationY = 2f;
    [Range(0f, 30f)]
    public float speedMax = 10f;
    public float _speedX = 0f;
    public float _speedY = 0f;
    public FrictionSettings waterFriction;

    [System.Serializable]
    public class FrictionSettings
    {
        public float friction = 10f;
        public float turnAroundFriction = 10f;
    }

    [SerializeField] private MechFace top;
    [SerializeField] private MechFace bottom;
    [SerializeField] private MechFace front;
    [SerializeField] private MechFace back;

    public FuelSystem fuel;

    public GameObject CameraPoint;

    public float camPointSpeed = 5f;

    public float wantedCamPosX = 0f;
    public float wantedCamOffsetX = 20f;
    public float wantedCamPosY = 0f;
    public float wantedCamOffsetY = 20f;

    public float CamDezoom = -30;

    public bool isCamMoving;
    public bool isMoving = false;

    public multipleTargetCamera cameraMultiple;

    [SerializeField] private List<ParticleSystem> particles = new List<ParticleSystem>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_dirX > 0)
        {
            wantedCamPosX = wantedCamOffsetX;
            cameraMultiple.minZoom = 100;
            if (!isCamMoving) isCamMoving = true;
            isMoving = true;
        }
        else if(_dirX < 0)
        {
            wantedCamPosX = -wantedCamOffsetX;
            cameraMultiple.minZoom = 100;
            if (!isCamMoving) isCamMoving = true;
            isMoving = true;
        }
        else
        {
            wantedCamPosX = 0;
            if(_dirY == 0)isMoving = false;

        }

        if (_dirY > 0)
        {
            wantedCamPosY = wantedCamOffsetY;
            cameraMultiple.minZoom = 100;
            if (!isCamMoving) isCamMoving = true;
            isMoving = true;
        }
        else if (_dirY < 0)
        {
            wantedCamPosY = -wantedCamOffsetY;
            cameraMultiple.minZoom = 100;
            if (!isCamMoving) isCamMoving = true;
            isMoving = true;
        }
        else
        {
            wantedCamPosY = 0;
            if (_dirX == 0) isMoving = false;
        }
        
        if(wantedCamPosX == 0 && wantedCamPosY == 0 && isCamMoving)
        {
            cameraMultiple.minZoom = 75;
            isCamMoving = false;
        }

        

        if (_dirX != 0)
        { 

            if (_dirX * _orientX <= 0f)
            {
                _speedX -= waterFriction.turnAroundFriction * Time.fixedDeltaTime;
                if (_speedX <= 0f)
                {
                    _orientX = Mathf.Sign(_dirX);
                    _speedX = 0f;
                    
                }
            }
            else
            {
                _speedX += accelerationX * Time.fixedDeltaTime;
                if (_speedX >= speedMax)
                {
                    _speedX = speedMax;
                    
                }
                _orientX = Mathf.Sign(_dirX);
            }
        }

        else if (_speedX > 0f)
        {
            _speedX -= waterFriction.friction * Time.fixedDeltaTime;
            if (_speedX < 0f)
            {
                _speedX = 0f;
                
            }
        }

        if (_dirY != 0)
        {
            if (_dirY * _orientY <= 0f)
            {
                _speedY -= waterFriction.turnAroundFriction * Time.fixedDeltaTime;
                if (_speedY <= 0f)
                {
                    _orientY = Mathf.Sign(_dirY);
                    _speedY = 0f;
                    
                }
            }
            else
            {
                _speedY += accelerationY * Time.fixedDeltaTime;
                if (_speedY >= speedMax)
                {
                    _speedY = speedMax;
                }
                _orientY = Mathf.Sign(_dirY);
            }
        }

        else if (_speedY > 0f)
        {
            _speedY -= waterFriction.friction * Time.fixedDeltaTime;
            if (_speedY < 0f)
            {
                _speedY = 0f;
            }
        }

        for(int i = particles.Count - 1; i >= 0; i--)
        {
            if(particles[i].isStopped && isMoving)
            {
                particles[i].Play();
            }
            else if(particles[i].isPlaying && !isMoving)
            {
                particles[i].Stop();
            }
        }

        if (_speedX > 0)
        {
            front.checkCollisions = true;
            back.checkCollisions = true;
        }
        else if (_speedX < 0)
        {
            back.checkCollisions = false;
            front.checkCollisions = false;
        }

        if (_speedY > 0)
        {
            top.checkCollisions = true;
            bottom.checkCollisions = true;
        }
        else if (_speedY < 0)
        {
            bottom.checkCollisions = false;
            top.checkCollisions = false;
        }

        if (front.isColliding && _dirX > 0)
        {
            _speedX = 0;
        }
        else if (back.isColliding && _dirX < 0)
        {
            _speedX = 0;
        }

        if (top.isColliding && _dirY > 0)
        {
            _speedY = 0;
        }
        else if (bottom.isColliding && _dirY < 0)
        {
            _speedY = 0;
        }

        _UpdatePosition();

        updtateCamPos();

        if (_speedX > 0 || _speedY > 0) fuel.LoseFuel();
        
    }

    public void HorizontalMovement(float dirX)
    {
        _dirX = dirX;
    }

    public void VerticalMovement(float dirY)
    {
        _dirY = dirY;
    }

    private void updtateCamPos()
    {
        float ratio = Time.deltaTime * camPointSpeed;

        CameraPoint.transform.localPosition = Vector3.Lerp(CameraPoint.transform.localPosition, new Vector3(wantedCamPosX, wantedCamPosY,0), ratio);
    }

    private void _UpdatePosition()
    {
        Vector3 newPos = transform.position;
        newPos.x += _speedX * Time.fixedDeltaTime * _orientX;
        newPos.y += _speedY * Time.fixedDeltaTime * _orientY;
        transform.position = newPos;
    }

    public void TakeDamage()
    {
        CameraShaker.Instance.startShake();
        UIManager.instance.UpdateLifeBar();
    }
}
