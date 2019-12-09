using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterfallEventZone : EventZone
{
    [Header("Le sous-Marin ira __% plus vite")]
    [SerializeField]
    [Range(0, 500)]
    private float percentage;

    [Header("Le fuel se videra __% plus vite")]
    [SerializeField]
    [Range(0, 500)]
    private float fuelPercentage;

    private float PercentToFloat(float thisPercentage)
    {
        thisPercentage = thisPercentage / 100;
        return thisPercentage;
    }

    protected override void EnterZone()
    {
        MechaManager.Instance.speedMax += (initialSubmarineSpeed * PercentToFloat(percentage));
        MechaManager.Instance.accelerationX += (initialSubmarineAccelerationX * PercentToFloat(percentage));
        MechaManager.Instance.accelerationY += (initialSubmarineAccelerationY * PercentToFloat(percentage));
        FuelSystem.Instance.fuelConsumptionRate += (_fuelInitialSpeed * PercentToFloat(fuelPercentage));
    }

    protected override void ExitZone()
    {
        MechaManager.Instance.speedMax = initialSubmarineSpeed;
        MechaManager.Instance.accelerationX = initialSubmarineAccelerationX;
        MechaManager.Instance.accelerationY = initialSubmarineAccelerationY;
        FuelSystem.Instance.fuelConsumptionRate = _fuelInitialSpeed;
    }
}
