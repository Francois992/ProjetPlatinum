using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{

    //Clarence Berard

    public PlayerRigidBodyEntity _playerRigidBody;

    private Player _mainPlayer;


    // Start is called before the first frame update
    void Start()
    {
        _mainPlayer = ReInput.players.GetPlayer("Player0");
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = _mainPlayer.GetAxis("MoveHorizontal");

        if (!_playerRigidBody.isInteracting)
        {
            _playerRigidBody.Move(dirX);
        }
        
        if(_mainPlayer.GetButtonDown("jump") && _playerRigidBody.IsOnGround() && !_playerRigidBody.isInteracting)
        {
            _playerRigidBody.Jump();
        }
        else if(_mainPlayer.GetButtonUp("jump"))
        {
            _playerRigidBody.StopJump();
        }

        if (_mainPlayer.GetButtonDown("Action"))
        {
            _playerRigidBody.Actions();
        }
    }
}
