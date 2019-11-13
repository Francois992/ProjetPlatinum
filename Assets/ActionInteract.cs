using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionInteract : MonoBehaviour
{
    public GameObject Interact;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Interact.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Interact.SetActive(false);
        }
    }
}
