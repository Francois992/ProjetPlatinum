using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{

    //Clarence Berard

    public PlayerRigidBodyEntity _playerRigidBody;

    public Player _mainPlayer;


    public string playerID;



    // Start is called before the first frame update
    void Start()
    {
        _playerRigidBody.reController = playerID;
        _mainPlayer = ReInput.players.GetPlayer(playerID);
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = _mainPlayer.GetAxis("MoveHorizontal");

        if (!_playerRigidBody.isInteracting && !_playerRigidBody.isDown)
        {
            _playerRigidBody.Move(dirX);
        }
        else
        {
            _playerRigidBody._speed = 0;
        }
        
        if(_mainPlayer.GetButtonDown("jump") && _playerRigidBody.IsOnGround() && !_playerRigidBody.isInteracting && !_playerRigidBody.isDown)
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
