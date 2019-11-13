using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeedleScript : MonoBehaviour
{

    public GameObject theWheel;

    public PlayerController pOne;
    public PlayerController pTwo;

    public PlayerRigidBodyEntity entityOne;
    public PlayerRigidBodyEntity entityTwo;


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
        if (entityOne.canInteractQTE)
        {
            entityOne.isInteracting = true;
            if (pOne._mainPlayer.GetButtonDown("Action"))
            {
                entityOne.isInteracting = false;
                if (other.gameObject.CompareTag("Fuel"))
                {
                    Debug.Log("Addfuel");
                    Spinner.instance.StopWheel();
                }

                if (other.gameObject.CompareTag("Oxygen"))
                {
                    Debug.Log("Addoxygen");
                    Spinner.instance.isRotating = true;
                    theWheel.SetActive(false);
                }

                if (other.gameObject.CompareTag("Ammo"))
                {
                    Debug.Log("Addammo");
                    Spinner.instance.isRotating = true;
                    theWheel.SetActive(false);
                }

                if (other.gameObject.CompareTag("RepairKit"))
                {
                    Debug.Log("Addrepairkit");
                    Spinner.instance.isRotating = true;
                    theWheel.SetActive(false);
                }
            }
        }
            Debug.Log("W is pushed");
            

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
