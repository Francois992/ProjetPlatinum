using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

//Made by Francois Dessarts
public class MechaController : MonoBehaviour
{
    public Player playerHorizontal;
    public Player playerVertical;

    private MechaManager mecha = MechaManager.Instance;

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

            MechaManager.Instance.HorizontalMovement(dirX);
            
        }
        else
        {
            MechaManager.Instance.HorizontalMovement(0);

            
        }

        if (isActivatedVer)
        {
            float dirY = playerVertical.GetAxis("MoveVertical");

            MechaManager.Instance.VerticalMovement(dirY);

        }
        else
        {
            MechaManager.Instance.VerticalMovement(0);

            
        }
        
    }
}
