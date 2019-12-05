using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scraps : MonoBehaviour
{
    [SerializeField] private int scrapValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Mecha")
        {
            Explode();
        }
        if (collision.gameObject.tag == "Bullet")
        {
            Explode();
        }
    }

    private void Explode()
    {
        Destroy(gameObject);
        UIManager.instance.ChangeInventory("Add", ref UIManager.instance.scrapsAmount, scrapValue);
    }
}
