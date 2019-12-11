using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{   
    

    [SerializeField]
    private bool isGameOver = false;

    private float Timer;
    public float maxTimer;


    // Start is called before the first frame update
    void Start()
    {
        Timer = maxTimer;
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
            if (UIManager.instance.)
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

    void UpdateTimer()
    {
        if(Timer<= 0)
        {
            isGameOver = true;
            return;
        }
        Timer -= Time.deltaTime;

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
