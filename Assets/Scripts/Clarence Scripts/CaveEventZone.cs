using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveEventZone : EventZone
{
    //Clarence Berard C# Script

    [Header("Facteur de vitesse de la roue")]
    [SerializeField]
    [Range(1, 5)]
    private float multiplicator;

    protected override void EnterZone()
    {
        //throw new System.NotImplementedException();
        Spinner.instance.speed *= multiplicator;
    }

    protected override void ExitZone()
    {
        //throw new System.NotImplementedException();
        Spinner.instance.speed = initialWheelSpeed;
    }
}
