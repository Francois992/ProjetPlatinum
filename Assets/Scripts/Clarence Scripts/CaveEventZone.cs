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

    private void Start()
    {
        titleZone = "Cave of Mines";
        featureOne = "- The craft wheel turns faster.";
        featureTwo = "- Watch out for mines !";
        zoneColor = Color.red;
    }

    protected override void EnterZone()
    {
        base.EnterZone();
        Spinner.instance.speed *= multiplicator;
    }

    protected override void ExitZone()
    {
        //throw new System.NotImplementedException();
        Spinner.instance.speed = initialWheelSpeed;
    }
}
