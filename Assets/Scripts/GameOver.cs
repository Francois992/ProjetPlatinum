﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{   
    

    [SerializeField]
    private bool isGameOver = false;

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            GameOverDie();
        }
        else
        {
            if (UIManager.instance.GetOxygen() <= 0)
            {
                isGameOver = true;
            }
            if (UIManager.instance.scrapsAmount == 0 && UIManager.instance.fuelJerrycanAmount== 0 && FuelSystem.Instance.startFuel <= 0)
            {
                isGameOver = true;
            }
            if(MechaManager.Instance.currentLife <= 0)
            {
                isGameOver = true;
            }
        }


    }

    void GameOverDie()
    {
        SceneManager.LoadScene("GameOver");
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mecha"))
        {
            IsWin();
        }
    }

    void IsWin()
    {
        SceneManager.LoadScene(0);
    }
}
