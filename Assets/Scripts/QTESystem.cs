using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTESystem : MonoBehaviour
{

    public GameObject DisplayBox;     //Texte contenant la lettre a appuyé
    public GameObject PassBox;        //Text affichant la reussite ou l'echec du QTE
    private int QTEGen;               //Variable generant une touche du QTE


    public float timer = 0f;
    public bool isPlay = false;
    private float timerMax = 3.5f;
    public Slider timerSld;

    public static QTESystem instance;

    Result _result = Result.None;

    public enum Result
    {
        None,
        Success,
        Fail,
    }

    private void LaunchSuccess()
    {
        _result = Result.Success;
        PassBox.GetComponent<Text>().text = "PASS!";
        isPlay = false;
    }

    private void LaunchFail()
    {
        _result = Result.Fail;
        PassBox.GetComponent<Text>().text = "FAIL!";
        isPlay = false;
    }

    public Result GetResult()
    {
        return _result;
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        LaunchQTEPhase();
        timerSld.maxValue = timerMax;
        
    }

    void Update()
    {
        timerSld.value = timer;
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
                        LaunchSuccess();
                    }
                    else if (Input.GetKeyDown(KeyCode.Space))
                    {

                    }
                    else
                    {
                        LaunchFail();
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
                        LaunchSuccess();
                    }
                    else if (Input.GetKeyDown(KeyCode.Space))
                    {

                    }
                    else
                    {
                        LaunchFail();
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
                        LaunchSuccess();
                    }
                    else if (Input.GetKeyDown(KeyCode.Space))
                    {

                    }
                    else
                    {
                        LaunchFail();
                    }
                }
            }
        }
        else
        {
            LaunchFail();
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
