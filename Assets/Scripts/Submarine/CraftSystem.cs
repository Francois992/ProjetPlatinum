using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSystem : MonoBehaviour
{

    public Transform SpawnPoint;
    public GameObject FuelMaterial;
    public GameObject MedicMaterial;

    public bool _craftOK = false;
    public bool _craftMedic = false;
    public bool _craftFuel = false;
    public QTESystem QTE;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FuelBase" && !_craftOK )
        {
            //Instantiate(FuelMaterial, SpawnPoint.transform.position, SpawnPoint.rotation);
            _craftOK = true;
            _craftFuel = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "MedicBase" && !_craftOK)
        {
            //Instantiate(FuelMaterial, SpawnPoint.transform.position, SpawnPoint.rotation);
            _craftOK = true;
            _craftMedic = true;
            Destroy(other.gameObject);
        }


    }


    // Update is called once per frame
    void Update()
    {
        if (_craftOK)
        {
            QTE.LaunchQTEPhase();
            //Instantiate(FuelMaterial, SpawnPoint.transform.position, SpawnPoint.rotation);
            _craftOK = false;
        }
    }
}
