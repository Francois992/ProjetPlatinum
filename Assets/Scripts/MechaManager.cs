using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made by Francois Dessarts
public class MechaManager : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
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
                _UpdatePosition();
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
                _UpdatePosition();
            }

            else if (_speedY > 0f)
            {
                _speedY -= waterFriction.friction * Time.fixedDeltaTime;
                if (_speedY < 0f)
                {
                    _speedY = 0f;
                }
            }

            _UpdatePosition();
        }
        
    }

    public void HorizontalMovement(float dirX)
    {
        _dirX = dirX;
    }

    public void VerticalMovement(float dirY)
    {
        _dirY = dirY;
    }

    private void _UpdatePosition()
    {
        Vector3 newPos = transform.position;
        newPos.x += _speedX * Time.fixedDeltaTime * _orientX;
        newPos.y += _speedY * Time.fixedDeltaTime * _orientY;
        transform.position = newPos;
    }
}
