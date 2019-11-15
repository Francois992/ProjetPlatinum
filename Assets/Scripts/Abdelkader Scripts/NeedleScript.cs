using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class NeedleScript : MonoBehaviour
{
    

    public Player playerController;
    

    public bool isDoingQTE = false;


    public UIAnimation UIAnimatorManager;


    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
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
            if (!isDoingQTE)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                return;
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }
            if (UIManager.instance.scrapsAmount <= 0)
            {
                return;
            }
            GetComponent<SpriteRenderer>().enabled = true;
            Spinner.instance.LaunchWheel();
            if (other.gameObject.CompareTag("Fuel"))
            {
                if (playerController.GetButtonDown("Shoot"))
                {
                    UIAnimatorManager.LaunchAnimFuel();
                    UIManager.instance.ChangeInventory("Add", ref UIManager.instance.fuelJerrycanAmount, 1);
                    UIManager.instance.ChangeInventory("Remove", ref UIManager.instance.scrapsAmount, 1);
                        
                }
            }

            if (other.gameObject.CompareTag("Oxygen"))
            {
                if (playerController.GetButtonDown("Shoot"))
                {
                    UIAnimatorManager.LaunchAnimOxygen();
                    UIManager.instance.ChangeInventory("Add", ref UIManager.instance.scubaTankAmount, 1);
                    UIManager.instance.ChangeInventory("Remove", ref UIManager.instance.scrapsAmount, 1);

                }
            }

            if (other.gameObject.CompareTag("Ammo"))
            {
                if (playerController.GetButtonDown("Shoot"))
                {
                    UIAnimatorManager.LaunchAnimAmmo();
                    UIManager.instance.ChangeInventory("Add", ref UIManager.instance.ammoAmount, 1);
                    UIManager.instance.ChangeInventory("Remove", ref UIManager.instance.scrapsAmount, 1);

                }
            }

            if (other.gameObject.CompareTag("RepairKit"))
            {
                if (playerController.GetButtonDown("Shoot"))
                {
                    UIAnimatorManager.LaunchAnimRepairKit();
                    UIManager.instance.ChangeInventory("Add", ref UIManager.instance.repairKitAmount, 1);
                    UIManager.instance.ChangeInventory("Remove", ref UIManager.instance.scrapsAmount, 1);

                }
            }

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
