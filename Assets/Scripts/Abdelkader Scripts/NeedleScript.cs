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
                UIManager.instance.UIAnimator.SetTrigger("NoScraps");
                return;
            }
            GetComponent<SpriteRenderer>().enabled = true;
            Spinner.instance.LaunchWheel();
            if (other.gameObject.CompareTag("Fuel"))
            {
                if (playerController.GetButtonDown("Shoot"))
                {
                    //UIAnimatorManager.LaunchAnimFuel();
                    UIManager.instance.UIAnimator.SetTrigger("AddFuel");
                    UIManager.instance.ChangeInventory("Add", ref UIManager.instance.fuelJerrycanAmount, UIManager.instance._initialGain);
                    UIManager.instance.ChangeInventory("Remove", ref UIManager.instance.scrapsAmount, UIManager.instance._initialCost);
                    FindObjectOfType<SoundManager>().Play("AddFuel");
                }
            }

            if (other.gameObject.CompareTag("Oxygen"))
            {
                if (playerController.GetButtonDown("Shoot"))
                {
                    //UIAnimatorManager.LaunchAnimOxygen();
                    UIManager.instance.UIAnimator.SetTrigger("AddOxygen");
                    UIManager.instance.ChangeInventory("Add", ref UIManager.instance.scubaTankAmount, UIManager.instance._initialGain);
                    UIManager.instance.ChangeInventory("Remove", ref UIManager.instance.scrapsAmount, UIManager.instance._initialCost);
                    FindObjectOfType<SoundManager>().Play("AddOxygen");
                }
            }

            if (other.gameObject.CompareTag("Ammo"))
            {
                if (playerController.GetButtonDown("Shoot"))
                {
                    //UIAnimatorManager.LaunchAnimAmmo();
                    UIManager.instance.UIAnimator.SetTrigger("AddAmmo");
                    UIManager.instance.ChangeInventory("Add", ref UIManager.instance.ammoAmount, UIManager.instance._initialGain);
                    UIManager.instance.ChangeInventory("Remove", ref UIManager.instance.scrapsAmount, UIManager.instance._initialCost);
                    FindObjectOfType<SoundManager>().Play("AddAmmo");
                }
            }

            if (other.gameObject.CompareTag("RepairKit"))
            {
                if (playerController.GetButtonDown("Shoot"))
                {
                    //UIAnimatorManager.LaunchAnimRepairKit();
                    UIManager.instance.UIAnimator.SetTrigger("AddRepairKit");
                    UIManager.instance.ChangeInventory("Add", ref UIManager.instance.repairKitAmount, UIManager.instance._initialGain);
                    UIManager.instance.ChangeInventory("Remove", ref UIManager.instance.scrapsAmount, UIManager.instance._initialCost);
                    FindObjectOfType<SoundManager>().Play("AddRepairKit");
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
