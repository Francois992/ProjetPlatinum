using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

//Made by Francois Dessarts
public class MechaController : MonoBehaviour
{
    public Player playerHorizontal;
    public Player playerVertical;

    public MechaManager mecha;

    public bool isActivatedHor = false;
    public bool isActivatedVer = false;

    // Start is called before the first frame update
    void Start()
    {
        playerHorizontal = ReInput.players.GetPlayer("PlayerMecha");
        playerVertical = ReInput.players.GetPlayer("PlayerMecha");
    }

    // Update is called once per frame
    void Update()
    {
        if(isActivatedHor)
        {
            float dirX = playerHorizontal.GetAxis("MoveHorizontal");

            mecha.HorizontalMovement(dirX);
            
        }

        if (isActivatedVer)
        {
            float dirY = playerVertical.GetAxis("MoveVertical");

            mecha.VerticalMovement(dirY);
        }
        
    }
}
