using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventZone : MonoBehaviour
{
    protected MechaManager _submarine;
    protected float initialWheelSpeed;
    protected UIManager _uiManager;
    protected float _playerInitialSpeed;


    // Start is called before the first frame update
    void Start()
    {
        _submarine = MechaManager.Instance;
        _uiManager = UIManager.instance;
        initialWheelSpeed = Spinner.instance.speed;
    }

    protected virtual void EnterZone()
    {
        
    }
    protected virtual void ExitZone()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        EnterZone();
        
    }

    private void OnTriggerExit(Collider other)
    {
        ExitZone();
    }
}
