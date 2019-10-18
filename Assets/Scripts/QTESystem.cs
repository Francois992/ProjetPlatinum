using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTESystem : MonoBehaviour
{

    public GameObject DisplayBox;     //Texte contenant la lettre a appuyé
    public GameObject PassBox;        //Text affichant la reussite ou l'echec du QTE
    public int QTEGen;                //Variable generant une touche du QTE
    public int WaitingForKey;         //Delais pour afficher la touche
    public int CorrectKey;            //Verifie si la touche appuyé est correcte
    public int CountingDown;          //Delais pour appuyer sur la touche

    void Update()
    {
        if (WaitingForKey == 0)
        {
            QTEGen = Random.Range(1, 4);
            CountingDown = 1;
            StartCoroutine(CountDown());
            if(QTEGen == 1)
            {
                WaitingForKey = 1;
                DisplayBox.GetComponent<Text>().text = "[E]";
            }
            if (QTEGen == 2)
            {
                WaitingForKey = 1;
                DisplayBox.GetComponent<Text>().text = "[R]";
            }
            if (QTEGen == 3)
            {
                WaitingForKey = 1;
                DisplayBox.GetComponent<Text>().text = "[T]";
            }
        }
        if (QTEGen == 1)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("EKey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if (QTEGen == 2)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("RKey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if (QTEGen == 3)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("TKey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
    }

    IEnumerator KeyPressing()
    {
        QTEGen = 4;
        if (CorrectKey == 1) 
        {
            CountingDown = 2;
            PassBox.GetComponent<Text>().text = "PASS!";
            yield return new WaitForSeconds(1.5f);
            CorrectKey = 0;
            PassBox.GetComponent<Text>().text = "";
            DisplayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.5f);
            WaitingForKey = 0;
            CountingDown = 1;
        }
        if (CorrectKey == 2)
        {
            CountingDown = 2;
            PassBox.GetComponent<Text>().text = "FAIL!";
            yield return new WaitForSeconds(1.5f);
            CorrectKey = 0;
            PassBox.GetComponent<Text>().text = "";
            DisplayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.5f);
            WaitingForKey = 0;
            CountingDown = 1;
        }
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(3.5f);
        if(CountingDown == 1)
        {
            QTEGen = 4;
            CountingDown = 2;
            PassBox.GetComponent<Text>().text = "FAIL!";
            yield return new WaitForSeconds(1.5f);
            CorrectKey = 0;
            PassBox.GetComponent<Text>().text = "";
            DisplayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.5f);
            WaitingForKey = 0;
            CountingDown = 1;
        }
    }

}
