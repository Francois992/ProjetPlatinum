using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject Canon;
    [SerializeField] private GameObject target;
    [SerializeField] private float targetSpeed = 15f;

    [SerializeField] private float bulletSpread = 15f;
    [SerializeField] private float maxCanonRot = 45;
    [SerializeField] private float minCanonRot = -45f;

    [SerializeField] private float shotCoolDown = 3f;

    private bool hasShot = false;

    public bool isActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target.transform.position.x >= Canon.transform.position.x)
        {
            float newPosX = target.transform.position.x + Input.GetAxis("Horizontal");
            newPosX = Mathf.Clamp(newPosX, Canon.transform.position.x, Canon.transform.position.x + 30);

            float newPosY = target.transform.position.y + Input.GetAxis("Vertical");
            newPosY = Mathf.Clamp(newPosY, Canon.transform.position.y - 10, Canon.transform.position.y + 10);

            Vector3 newPos = new Vector3(newPosX, newPosY, target.transform.position.z);
            target.transform.position = Vector3.MoveTowards(target.transform.position, newPos, targetSpeed * Time.deltaTime);

            Canon.transform.LookAt(target.transform);

            float rotX = Canon.transform.eulerAngles.x;

            if (rotX >= 180) rotX -= 360;

            rotX = Mathf.Clamp(rotX, minCanonRot, maxCanonRot);

            Canon.transform.localRotation = Quaternion.Euler(rotX, Canon.transform.eulerAngles.y, Canon.transform.eulerAngles.z);
        }

        if (Input.GetKeyDown(KeyCode.F) && !hasShot)
        {
            Shoot();
        }

        if (hasShot)
        {
            shotCoolDown -= Time.deltaTime;
        }

        if(shotCoolDown <= 0)
        {
            hasShot = false;
            shotCoolDown = 3f;
        }
    }

    private void Shoot()
    {
        hasShot = true;
        Instantiate(bullet, Canon.transform.position, Canon.transform.rotation);
    }
}
