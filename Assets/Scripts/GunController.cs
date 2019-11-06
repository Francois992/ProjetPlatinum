using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class GunController : MonoBehaviour
{
    public Player player;

    public Turret turret;

    public bool isActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        player = ReInput.players.GetPlayer("PlayerMecha");
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            float dirX = player.GetAxis("MoveHorizontal");
            float dirY = player.GetAxis("MoveVertical");

            if (player.GetButtonUp("Action"))
            {
                turret.Action();
            }

            turret.targetMovement(dirX, dirY);
        }
        
    }
}
