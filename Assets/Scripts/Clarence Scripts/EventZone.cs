using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventZone : MonoBehaviour
{
    private MechaManager _submarine;
    private float initialWheelSpeed;
    private UIManager _uiManager;


    // Start is called before the first frame update
    void Start()
    {
        _submarine = MechaManager.Instance;
        _uiManager = UIManager.instance;
        initialWheelSpeed = Spinner.instance.speed;
    }

    protected abstract void EnterZone();
    protected abstract void QuitZone();

    private void OnTriggerEnter(Collider other)
    {
        EnterZone();
        
    }
}
