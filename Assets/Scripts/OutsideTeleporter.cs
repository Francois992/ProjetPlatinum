using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideTeleporter : Teleporter
{

    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
        ExploratonTeleporter.teleporters.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
