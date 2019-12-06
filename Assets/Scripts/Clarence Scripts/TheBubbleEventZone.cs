using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBubbleEventZone : EventZone
{
    //Clarence Berard C# Script

    [Header("Facteur de vitesse de la roue")]
    [SerializeField]
    [Range(1, 5)]
    private float dividor;

    private float initialSpinnerSpeed;

    private void Start()
    {
        initialSpinnerSpeed = Spinner.instance.speed;
    }
    protected override void EnterZone()
    {
        //throw new System.NotImplementedException();
        Spinner.instance.speed /= dividor;
    }

    protected override void ExitZone()
    {
        //throw new System.NotImplementedException();
        Spinner.instance.speed = initialSpinnerSpeed;
    }
}
