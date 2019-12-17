using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made by Francois Dessarts
public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject Canon;
    [SerializeField] public GameObject target;
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

    public GunPanel panel;

    public float _dirX;
    public float _dirY;

    public Vector3 targetPos;

    public GameObject CameraPoint;

    public float camPointSpeed = 5f;

    public float CamDezoom = -30;

    public multipleTargetCamera cameraMultiple;

    public float wantedCamPosX = 0f;
    public float wantedCamOffsetX = 20f;

    private float camInitMinZoom;
    private float camWantedMinZoom = 100;

    public bool onActivate = false;

    // Start is called before the first frame update
    void Start()
    {
        Canon.transform.LookAt(target.transform);
        targetPos = target.transform.localPosition;

        target.SetActive(false);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (target.activeSelf)
        {
            if (target.transform.position.x >= Canon.transform.position.x)
            {
                UpdateRot();
            }
        }

        if (onActivate)
        {
            wantedCamPosX = wantedCamOffsetX;
            if (cameraMultiple.minZoom != camWantedMinZoom) camInitMinZoom = cameraMultiple.minZoom;
            cameraMultiple.minZoom = camWantedMinZoom;
            
        }
        else
        {
            wantedCamPosX = 0;
        }

        if(wantedCamPosX == 0) cameraMultiple.minZoom = camInitMinZoom;

        updtateCamPos();

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

    private void updtateCamPos()
    {
        float ratio = Time.deltaTime * camPointSpeed;

        CameraPoint.transform.localPosition = Vector3.Lerp(CameraPoint.transform.localPosition, new Vector3(wantedCamPosX, 0, 0), ratio);
    }

    private void UpdateRot()
    {
        float newPosX = target.transform.position.x;
        float newPosY = target.transform.position.y;

        if (_dirX != 0)
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

    public void Action()
    {
        if (!hasShot && UIManager.instance.ammoAmount > 0) Shoot();
        else if (!hasShot) UIManager.instance.UIAnimator.SetTrigger("NoAmmo");
    }

    private void Shoot()
    {
        hasShot = true;
        UIManager.instance.ChangeInventory("Remove", ref UIManager.instance.ammoAmount, 1);
        Instantiate(bullet, muzzle.transform.position, Canon.transform.rotation);
        panel.OnShoot();
        flash.Play();
    }

    public void targetMovement(float dirX, float dirY)
    {
        _dirX = dirX;
        _dirY = dirY;
    }

}
