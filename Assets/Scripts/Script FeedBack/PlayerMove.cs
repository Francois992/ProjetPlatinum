using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float speed;
    public float minSpeed = 0f;
    public float maxSpeed = 0.2f;
    public float rotSpeed;

    FuelSystem fuelSystem;

    // Start is called before the first frame update
    void Start()
    {
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);

        fuelSystem = GetComponent<FuelSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fuelSystem.startFuel <= 0)
            return;

        float forward = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotSpeed;

        if (Input.GetAxis("Vertical") > 0)
        {
            MoveForward(forward, rotation);
        }
        else
            speed = 0;

        if (Input.GetAxis("Vertical") < 0)
        {
            MoveBackward(forward, rotation);
        }
    }

    void MoveForward(float forw, float rot)
    {
        speed += Time.deltaTime / 20f;
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);

        forw += Time.deltaTime;
        rot += Time.deltaTime;
        transform.Translate(0, 0, forw);
        transform.Rotate(0, rot, 0);

        fuelSystem.fuelConsumptionRate = speed * 20f;
        fuelSystem.LoseFuel();
    }

    void MoveBackward(float forw, float rot)
    {
        forw -= Time.deltaTime;
        rot += Time.deltaTime;
        transform.Translate(0, 0, forw);
        transform.Rotate(0, -rot, 0);

        fuelSystem.fuelConsumptionRate = 0.2f;
        fuelSystem.LoseFuel();
    }

}
