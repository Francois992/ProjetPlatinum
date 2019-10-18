using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class MechaController : MonoBehaviour
{
    private Player _mainPlayer;

    public MechaManager mecha;

    // Start is called before the first frame update
    void Start()
    {
        _mainPlayer = ReInput.players.GetPlayer("PlayerMecha");
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = _mainPlayer.GetAxis("MoveHorizontal");
        float dirY = _mainPlayer.GetAxis("MoveVertical");

        mecha.HorizontalMovement(dirX);
        mecha.VerticalMovement(dirY);
    }
}
