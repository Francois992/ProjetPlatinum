using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private static CameraShaker _instance = null;

    public static CameraShaker Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Transform positionSub;
    public float smoothValue = 0.125f;
    public Vector3 Offset;

    private float shakeDuration = 0f;
    [SerializeField] private float shakeLength = 1f;
    [SerializeField] private float shakeMagnitude = 0.7f;
    [SerializeField] private float dampingSpeed = 1.0f;
    Vector3 initialPosition;

    private void Start()
    {
        //initialPosition = transform.localPosition;
    }

    void Update()
    {
        Vector3 positionDesiree = positionSub.position + Offset;
        //Vector3 smoothPosition = Vector3.Lerp(transform.position, positionDesiree, smoothValue);

        if (shakeDuration > 0)
        {
            transform.localPosition = transform.localPosition + Random.insideUnitSphere * shakeMagnitude;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else if(shakeDuration < 0)
        {
            shakeDuration = 0f;
            //transform.localPosition = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z);
        }
    }

    public void startShake()
    {
        //initialPosition = transform.localPosition;
        shakeDuration = shakeLength;
    }

    public void stopShake()
    {
        shakeLength = 0;
    }
}
