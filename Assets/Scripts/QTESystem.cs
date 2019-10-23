using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTESystem : MonoBehaviour
{

    public GameObject DisplayBox;     //Texte contenant la lettre a appuyé
    public GameObject PassBox;        //Text affichant la reussite ou l'echec du QTE
    private int QTEGen;               //Variable generant une touche du QTE


    private float timer = 0f;
    public bool isPlay = false;
    private float timerMax = 3.5f;

    public static QTESystem instance;

    private void Awake()
    {
        instance = this;
    }




    void Start()
    {
        LaunchQTEPhase();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isPlay)
        {
            LaunchQTEPhase();
        }


        if (isPlay && timer < timerMax)
        {
            timer += Time.deltaTime;
            PlayQTE();
        }
        
        
    }

    public void PlayQTE()
    {
        if (timer < timerMax && isPlay)
        {

            PassBox.GetComponent<Text>().text = "";

            if (QTEGen == 1)
            {
                DisplayBox.GetComponent<Text>().text = "[E]";
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        PassBox.GetComponent<Text>().text = "PASS!";
                        isPlay = false;
                    }
                    else if (Input.GetKeyDown(KeyCode.Space))
                    {

                    }
                    else
                    {
                        PassBox.GetComponent<Text>().text = "FAIL!";
                        isPlay = false;

                    }
                }
            }
            else if (QTEGen == 2)
            {
                DisplayBox.GetComponent<Text>().text = "[R]";
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        PassBox.GetComponent<Text>().text = "PASS!";
                        isPlay = false;
                    }
                    else if (Input.GetKeyDown(KeyCode.Space))
                    {

                    }
                    else
                    {
                        PassBox.GetComponent<Text>().text = "FAIL!";
                        isPlay = false;

                    }
                }
            }
            else if (QTEGen == 3)
            {
                DisplayBox.GetComponent<Text>().text = "[T]";
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.T))
                    {
                        PassBox.GetComponent<Text>().text = "PASS!";
                        isPlay = false;
                    }
                    else if (Input.GetKeyDown(KeyCode.Space))
                    {

                    }
                    else
                    {
                        PassBox.GetComponent<Text>().text = "FAIL!";
                        isPlay = false;

                    }
                }
            }
        }
        else
        {
            PassBox.GetComponent<Text>().text = "FAIL!";
            isPlay = false;
            return;
        }
    }


    public void LaunchQTEPhase()
    {
        timer = 0f;
        QTEGen = Random.Range(1, 4);
        PassBox.GetComponent<Text>().text = "";
        DisplayBox.GetComponent<Text>().text = "";
        isPlay = true;
    }


   
}
