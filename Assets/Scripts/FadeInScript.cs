using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("LD Beta Bis");
    }

    public void PlayGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    public void PlayGameWin()
    {
        SceneManager.LoadScene("WinScreen");
    }
}
