using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTESystem : MonoBehaviour
{

    public GameObject QTEWindow;      //Ensemble du systeme de QTE
    public GameObject DisplayBox;     //Texte contenant la lettre a appuyé
    public GameObject PassBox;        //Text affichant la reussite ou l'echec du QTE
    private int QTEGen;               //Variable generant une touche du QTE
    
    public float timer = 0f;
    public bool isPlay = false;
    private float timerMax = 3.5f;
    public Slider timerSld;

    public CraftSystem Craft;

    //public static QTESystem instance;

    Result _result = Result.None;
    //Result _result0 = Result.None;

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
        QTEWindow.SetActive(false);
        Instantiate(Craft.FuelMaterial, Craft.SpawnPoint.transform.position, Craft.SpawnPoint.rotation);
    }

    private void LaunchFail()
    {
        _result = Result.Fail;
        PassBox.GetComponent<Text>().text = "FAIL!";
        //isPlay = false;
        LaunchQTEPhase();
    }

    public Result GetResult()
    {
        return _result;
    }

    private void Awake()
    {
        //instance = this;
    }

    void Start()
    {

        QTEWindow.SetActive(false);
        isPlay = false;
        //LaunchQTEPhase();
        timerSld.maxValue = timerMax;
        
    }

    /*void UpdateQTEOutcome(Result _QTEResult, int nbOfQTE = 0)
    {
        if(_QTEResult == Result.Success)
        {
            switch (nbOfQTE)
            {
                case 1:
                    {

                    }
                break;
                case 2:
                    {

                    }
                    break;
                case 3:
                    {

                    }
                    break;
                default:
                    {
                        Debug.Log("chtoc");
                    }
                    break;
            }
            //Code si Victoire QTE
            _QTEResult = Result.None;
        }
        else if (_QTEResult == Result.Fail)
        {
            switch (nbOfQTE)
            {
                case 1:
                    {

                    }
                    break;
                case 2:
                    {

                    }
                    break;
                case 3:
                    {

                    }
                    break;
                default:
                    {
                        Debug.Log("chtoc");
                    }
                    break;
            }
                    _QTEResult = Result.None;
        }
    }*/

    void Update()
    {
        timerSld.value = timer;
        /*if (Input.GetKeyDown(KeyCode.Space) && !isPlay)
        {
            LaunchQTEPhase();
        }*/


        if (isPlay && timer < timerMax)
        {
            timer += Time.deltaTime;
            PlayQTE();
        }

        //UpdateQTEOutcome(_result, 1);
        //UpdateQTEOutcome(_result0, 2); 
        
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
        QTEWindow.SetActive(true);
        timer = 0f;
        QTEGen = Random.Range(1, 4);
        PassBox.GetComponent<Text>().text = "";
        DisplayBox.GetComponent<Text>().text = "";
        isPlay = true;
       
    }


   
}
