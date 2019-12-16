using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject fadeScreen;
    //public Slider slider;
    private void Start()
    {
        fadeScreen.SetActive(false);
    }
    public void LoadLevel()
    {
        //StartCoroutine(LoadAsynchronously(sceneIndex));
        fadeScreen.SetActive(true);
        
    }
    
    /*
    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;

            yield return null;
        }
    }*/

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
    public void BackGame()
    {
        SceneManager.LoadScene("LD Beta Bis");
    }
    public void OnApplicationQuit()
    {
        Application.Quit();        
    }
}
