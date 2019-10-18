using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractItem : MonoBehaviour
{

    public GameObject QTESprite;

    // Start is called before the first frame update
    void Start()
    {
        QTESprite.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void StartQTE()
    {
        QTESprite.SetActive(true);
    }

    public void EndQTE()
    {
        QTESprite.SetActive(false);
    }
}
