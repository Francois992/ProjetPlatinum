using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSystem : MonoBehaviour
{

    public Transform SpawnPoint;
    public GameObject FuelMaterial;

    private bool _craftOK = false;
    public QTESystem QTE;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FuelBase")
        {
            //Instantiate(FuelMaterial, SpawnPoint.transform.position, SpawnPoint.rotation);
            _craftOK = true;
            Destroy(other.gameObject);
        }

        
    }


    // Update is called once per frame
    void Update()
    {
        if (_craftOK == true)
        {
            QTE.LaunchQTEPhase();
            //Instantiate(FuelMaterial, SpawnPoint.transform.position, SpawnPoint.rotation);
            _craftOK = false;
        }
    }
}
