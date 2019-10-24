using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploratonTeleporter : Teleporter
{
    public static List<OutsideTeleporter> teleporters = new List<OutsideTeleporter>();

    public float detectionDistance = 50f;
    public float maxRange = 60f;
    private float detectedDistance = 0f;

    public bool hasArrival = false;

    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasArrival)
        {
            CheckForArrival();
        }
    }

    private void CheckForArrival()
    {
        for (int i = teleporters.Count - 1; i >= 0; i--)
        {
            if(detectedDistance != 0 && detectedDistance > Vector3.Distance(transform.position, teleporters[i].transform.position))
            {
                detectedDistance = Vector3.Distance(transform.position, teleporters[i].transform.position);
            }
            else if(detectedDistance == 0)
            {
                detectedDistance = Vector3.Distance(transform.position, teleporters[i].transform.position);
            }
            else
            {

            }

            if (detectedDistance <= detectionDistance)
            {
                arrival = teleporters[i];
                teleporters[i].isActivated = true;
                arrival.arrival = this;
                hasArrival = true;
                arrival.isActivated = true;
                isActivated = true;
            }
        }
    }

    private void checkRange()
    {
        if(Vector3.Distance(transform.position, arrival.transform.position) > maxRange)
        {
            arrival.isActivated = false;
            isActivated = false;
            arrival.arrival = null;
            arrival = null;
            hasArrival = false;
            detectedDistance = 0f;
        }
    }

}
