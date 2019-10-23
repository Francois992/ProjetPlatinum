using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

//Made by Francois Dessarts
public class MechaController : MonoBehaviour
{
    private Player playerHorizontal;
    private Player playerVertical;

    public MechaManager mecha;

    // Start is called before the first frame update
    void Start()
    {
        playerHorizontal = ReInput.players.GetPlayer("PlayerMecha");
        playerVertical = ReInput.players.GetPlayer("PlayerMecha");
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = playerHorizontal.GetAxis("MoveHorizontal");
        float dirY = playerVertical.GetAxis("MoveVertical");

        mecha.HorizontalMovement(dirX);
        mecha.VerticalMovement(dirY);
    }
}
