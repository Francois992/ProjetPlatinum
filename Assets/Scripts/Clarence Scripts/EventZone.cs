using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventZone : MonoBehaviour
{
    //Clarence Berard C# Script
    
    protected float initialSubmarineSpeed = 5.5f;
    protected float initialSubmarineAccelerationX = 2f;
    protected float initialSubmarineAccelerationY = 2f;
    protected float initialWheelSpeed = 100;
    protected UIManager _uiManager;
    protected float _playerInitialSpeed;
    protected float _fuelInitialSpeed = 0.2f;

    private bool isInside = false;

    protected string titleZone;
    protected string featureOne;
    protected string featureTwo;
    protected Color zoneColor = Color.white;

    private bool isEnterZone = false;


    // Start is called before the first frame update
    void Start()
    {
        _uiManager = UIManager.instance;
    }

    protected virtual void EnterZone()
    {
        if (!isEnterZone)
        {
            UIManager.instance.SetZoneTitle(titleZone);
            UIManager.instance.SetFeatureOne(featureOne);
            UIManager.instance.SetFeatureTwo(featureTwo);
            UIManager.instance.SetZoneColor(zoneColor);
            UIManager.instance.EnterZone();
            isEnterZone = true;
        }
        
    }
    protected virtual void ExitZone()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isInside)
        {
            EnterZone();
            isInside = true;
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (isInside)
        {
            ExitZone();
            isInside = false;
        }
    }
}
