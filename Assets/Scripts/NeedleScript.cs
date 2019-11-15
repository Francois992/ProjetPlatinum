using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class NeedleScript : MonoBehaviour
{

    public GameObject theWheel;

    public Player playerController;
    

    public bool isDoingQTE = false;
    

    private void Start()
    {

    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.W))
        {
            Spinner.instance.isRotating = true;
        }*/
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerController != null)
        {
                Spinner.instance.LaunchWheel();
                if (other.gameObject.CompareTag("Fuel"))
                {
                    if (playerController.GetButtonDown("Shoot"))
                    {
                        UIManager.instance.ChangeInventory("Add", ref UIManager.instance.fuelJerrycanAmount, 1);
                        UIManager.instance.ChangeInventory("Remove", ref UIManager.instance.scrapsAmount, 1);
                        
                    }
                }
                    
                /*
                if (other.gameObject.CompareTag("Oxygen"))
                {
                    Debug.Log("Addoxygen");
                    Spinner.instance.StopWheel();
                }

                if (other.gameObject.CompareTag("Ammo"))
                {
                    Debug.Log("Addammo");
                    Spinner.instance.StopWheel();
                }

                if (other.gameObject.CompareTag("RepairKit"))
                {
                    Debug.Log("Addrepairkit");
                    Spinner.instance.StopWheel();
                }*/
          
        }
            

        if (!Spinner.instance.isRotating)
        {
            
            
        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("M is pressed");
        }
    }*/

}
