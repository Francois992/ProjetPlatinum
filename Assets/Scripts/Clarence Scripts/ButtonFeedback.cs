using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFeedback : MonoBehaviour
{
    [SerializeField]
    private GameObject xButton;
    [SerializeField]
    private GameObject redCross;
    [SerializeField]
    private GameObject InteractButton;

    private PlayerRigidBodyEntity thisPlayer;

    // Start is called before the first frame update
    void Start()
    {
        xButton.SetActive(false);
        redCross.SetActive(false);
        InteractButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RepairHubCheck()
    {
        if (UIManager.instance.repairKitAmount > 0)
        {
            redCross.SetActive(false);
        }
        else
        {
            redCross.SetActive(true);
        }
        xButton.SetActive(true);
        InteractButton.SetActive(false);
    }

    public void StopRepairCheck()
    {
        redCross.SetActive(false);
        xButton.SetActive(false);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            thisPlayer = collision.gameObject.GetComponent<PlayerRigidBodyEntity>();
            if (thisPlayer.isInteracting)
            {
                xButton.SetActive(false);
                redCross.SetActive(false);
                if(gameObject.tag == "CraftTable" || gameObject.tag == "MovePanel") InteractButton.SetActive(true);
            }
            else
            {
                if(gameObject.tag == "CraftTable")
                {
                    if(UIManager.instance.scrapsAmount > 0)
                    {
                        redCross.SetActive(false);
                    }
                    else
                    {
                        redCross.SetActive(true);
                    }
                }
                else if (gameObject.tag == "GunPanel")
                {
                    if (UIManager.instance.ammoAmount > 0)
                    {
                        redCross.SetActive(false);
                    }
                    else
                    {
                        redCross.SetActive(true);
                    }
                }
                else if (gameObject.tag == "Tank")
                {
                    if(UIManager.instance.fuelJerrycanAmount > 0)
                    {
                        redCross.SetActive(false);
                    }
                    else
                    {
                        redCross.SetActive(true);
                    }
                }
                
                xButton.SetActive(true);
                InteractButton.SetActive(false);
                
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            thisPlayer = null;
            xButton.SetActive(false);
            redCross.SetActive(false);
            InteractButton.SetActive(false);
        }
    }
}
