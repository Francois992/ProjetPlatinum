using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made by Francois Dessarts
public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject Canon;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject muzzle;
    [SerializeField] private float targetSpeed = 15f;

    [SerializeField] private float bulletSpread = 15f;
    [SerializeField] private float maxCanonRot = 45;
    [SerializeField] private float minCanonRot = -45f;

    [SerializeField] private float shotCoolDown = 3f;

    [SerializeField] private float minOffsetX = 4f;
    [SerializeField] private float maxOffsetX = 30f;
    [SerializeField] private float minOffsetY = 10f;
    [SerializeField] private float maxOffsetY = 10f;

    [SerializeField] private ParticleSystem flash;

    private bool hasShot = false;

    public bool isActivated = false;

    public float _dirX;
    public float _dirY;

    // Start is called before the first frame update
    void Start()
    {
        Canon.transform.LookAt(target.transform);
    }

    // Update is called once per frame
    void Update()
    {     
        if (target.transform.position.x >= Canon.transform.position.x)
        {
            float newPosX = target.transform.position.x;
            float newPosY = target.transform.position.y;

            if(_dirX != 0)
            {
                newPosX = target.transform.position.x + _dirX;
                newPosX = Mathf.Clamp(newPosX, Canon.transform.position.x + minOffsetX, Canon.transform.position.x + maxOffsetX);
            }
            else
            {
                newPosX = target.transform.position.x;
            }

            if (_dirY != 0)
            {
                newPosY = target.transform.position.y + _dirY;
                newPosY = Mathf.Clamp(newPosY, Canon.transform.position.y - minOffsetY, Canon.transform.position.y + maxOffsetY);
            }
            else
            {
                newPosY = target.transform.position.y;
            }


            Vector3 newPos = new Vector3(newPosX, newPosY, target.transform.position.z);
            target.transform.position = Vector3.MoveTowards(target.transform.position, newPos, targetSpeed * Time.deltaTime);

            Canon.transform.LookAt(target.transform);

            float rotX = Canon.transform.eulerAngles.x;

            if (rotX >= 180) rotX -= 360;

            rotX = Mathf.Clamp(rotX, minCanonRot, maxCanonRot);

            Canon.transform.localRotation = Quaternion.Euler(rotX, Canon.transform.eulerAngles.y, Canon.transform.eulerAngles.z);
        }

        if (hasShot)
        {
            shotCoolDown -= Time.deltaTime;
            
        }

        if (shotCoolDown <= 0)
        {
            hasShot = false;
            shotCoolDown = 3f;
            flash.Stop();
        }
                
    }

    public void Action()
    {
        if(!hasShot) Shoot();
    }

    private void Shoot()
    {
        hasShot = true;
        Instantiate(bullet, muzzle.transform.position, muzzle.transform.rotation);
        flash.Play();
    }

    public void targetMovement(float dirX, float dirY)
    {
        _dirX = dirX;
        _dirY = dirY;
    }

}
