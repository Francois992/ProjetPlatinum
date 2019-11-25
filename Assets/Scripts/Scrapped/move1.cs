using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move1 : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * h * speed * Time.deltaTime);

        float H = Input.GetAxis("Vertical");
        transform.Translate(Vector2.up * H * speed * Time.deltaTime);
    }
}
