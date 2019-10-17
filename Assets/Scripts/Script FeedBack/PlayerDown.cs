using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDown : MonoBehaviour
{
    public Animator J1, J2, J3, J4;

    
    void Start()
    {
        
    }

   
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            J1.SetBool("Down", true);
        }
        else
        {
            J1.SetBool("Down", false);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {

        }
        if (Input.GetKeyDown(KeyCode.E))
        {

        }
        if (Input.GetKeyDown(KeyCode.R))
        {

        }
    }

    
}
