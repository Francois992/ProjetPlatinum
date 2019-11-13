using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class MovementPanel : MonoBehaviour
{
    public PlayerRigidBodyEntity user;

    public bool horOrientation = false;

    public MechaController mechaController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUsed()
    {

        if (horOrientation)
        {
            mechaController.playerHorizontal = ReInput.players.GetPlayer(user.reController);
            mechaController.isActivatedHor = true;
        }
        else
        {
            mechaController.playerVertical = ReInput.players.GetPlayer(user.reController);
            mechaController.isActivatedVer = true;
        }
    }

    public void OnDropped()
    {
        if (horOrientation)
        {
            
            mechaController.isActivatedHor = false;
        }
        else
        {

            mechaController.isActivatedVer = false;
        }
    }
}
