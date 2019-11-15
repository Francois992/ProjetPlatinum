using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class GunPanel : MonoBehaviour
{
    public PlayerRigidBodyEntity user;

    public GunController gunController;

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
        gunController.player = ReInput.players.GetPlayer(user.reController);
        gunController.isActivated = true;
    }

    public void OnDropped()
    {
        gunController.isActivated = false;
    }
}
