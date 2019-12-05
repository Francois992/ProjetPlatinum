using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CraftTable : MonoBehaviour
{
    public PlayerRigidBodyEntity user;

    public NeedleScript NeedleController;
    

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
        NeedleController.playerController = ReInput.players.GetPlayer(user.reController);
        NeedleController.isDoingQTE = true;
    }

    public void OnDropped()
    {
        user.isInteracting = false;
        user = null;
        NeedleController.playerController = null;
        NeedleController.isDoingQTE = false;
    }
}
