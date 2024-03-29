﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechFace : MonoBehaviour
{
    public bool checkCollisions = false;

    public bool isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (checkCollisions)
        {
            if (collision.gameObject.tag == "Walls")
            {
                if (!isColliding)
                {

                    MechaManager.Instance.TakeDamage(1);
                    CameraShaker.Instance.startShake();
                    Ripple.instance.RippleEffect();
                    isColliding = true;
                }
                
            }
        }
 
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Walls")
        {

            isColliding = false;
        }
    }
}
