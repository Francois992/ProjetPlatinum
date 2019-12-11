using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventZone : MonoBehaviour
{
    //Clarence Berard C# Script
    
    protected float initialSubmarineSpeed;
    protected float initialSubmarineAccelerationX;
    protected float initialSubmarineAccelerationY;
    protected float initialWheelSpeed;
    protected UIManager _uiManager;
    protected float _playerInitialSpeed;
    protected float _fuelInitialSpeed;

    private bool isInside = false;

    protected string titleZone;
    protected string featureOne;
    protected string featureTwo;
    protected Color zoneColor = Color.white;

    private bool isEnterZone = false;


    // Start is called before the first frame update
    void Start()
    {
        initialSubmarineSpeed = MechaManager.Instance.speedMax;
        initialSubmarineAccelerationX = MechaManager.Instance.accelerationX;
        initialSubmarineAccelerationY = MechaManager.Instance.accelerationY;
        _uiManager = UIManager.instance;
        initialWheelSpeed = Spinner.instance.speed;
        _fuelInitialSpeed = FuelSystem.Instance.fuelConsumptionRate;
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
