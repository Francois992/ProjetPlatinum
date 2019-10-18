using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEShow : MonoBehaviour
{

    public GameObject QTEInput;
    public GameObject PassFailTxt;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent(QTESystem).enable = false;
        QTEInput.SetActive(false);
        PassFailTxt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            QTEInput.SetActive(true);
            PassFailTxt.SetActive(true);
        }
        if (Input.GetMouseButtonDown(1))
        {
            QTEInput.SetActive(false);
            PassFailTxt.SetActive(false);
        }
    }
}
