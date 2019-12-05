using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanyonEventZone : EventZone
{
    [SerializeField]
    [Range(0, 5)]private float speedMultiplicator;
    [SerializeField]
    private PlayerRigidBodyEntity playerOne;
    [SerializeField]
    private PlayerRigidBodyEntity playerTwo;

    private float _playerInitialAcceleration;

    private void Start()
    {
        _playerInitialSpeed = playerOne.speedMax;
        _playerInitialAcceleration = playerOne.acceleration;
        //Debug.Log(PlayerRigidBodyEntity.playerList[0]);
    }
    protected override void EnterZone()
    {

        //throw new System.NotImplementedException();
        /*
        for(int i = 0; i < PlayerRigidBodyEntity.playerList.Count; i++)
        {
            PlayerRigidBodyEntity.playerList[i]._speed = _playerInitialSpeed / speedMultiplicator;
        }*/
        playerOne.speedMax = _playerInitialSpeed * speedMultiplicator;
        playerTwo.speedMax = _playerInitialSpeed * speedMultiplicator;
        playerOne.acceleration = _playerInitialSpeed * _playerInitialAcceleration;
        playerTwo.acceleration = _playerInitialSpeed * _playerInitialAcceleration;
        UIManager.instance.ChangeInventory("Remove", ref UIManager.instance.fuelJerrycanAmount, UIManager.instance.fuelJerrycanAmount / 2);
    }

    
    protected override void ExitZone()
    {
        //throw new System.NotImplementedException();
        /*
        for (int i = 0; i < PlayerRigidBodyEntity.playerList.Count; i++)
        {
            PlayerRigidBodyEntity.playerList[i]._speed = _playerInitialSpeed;
        }*/
        playerOne.speedMax = _playerInitialSpeed;
        playerTwo.speedMax = _playerInitialSpeed;
        playerOne.acceleration = _playerInitialAcceleration;
        playerTwo.acceleration = _playerInitialAcceleration;
    }
}
