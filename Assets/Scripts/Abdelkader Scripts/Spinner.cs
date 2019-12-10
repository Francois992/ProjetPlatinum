using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public static Spinner instance;

    public bool isRotating = true;
    private bool isOnPlaying = false;
    public float speed;
    public GameObject Needle;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StopWheel();
    }

    private void Update()
    {
        
        if (isRotating == true)
        {
            transform.Rotate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
            if (!isOnPlaying)
            {
                FindObjectOfType<SoundManager>().Play("RotatingWheel");
                isOnPlaying = true;
            }
        }

    }

    public void LaunchWheel()
    {
        isRotating = true;
        this.gameObject.SetActive(true);
        Needle.SetActive(true);
    }

    public void StopWheel()
    {
        isRotating = false;
        isOnPlaying = false;
        this.gameObject.SetActive(false);
        Needle.SetActive(false);
    }
    
}
