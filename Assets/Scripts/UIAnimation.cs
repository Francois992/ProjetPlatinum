using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{

    private float animationTimer = 0.0f;
    private float animationSpeed = 5.0f;

    public Image scubaTank;
    public Image fuel;
    public Image ammo;
    public Image repairKit;
    public Image scrap;

    //Vector3 SizeOriginal;
    float maxScale_x = 1.2f;
    float maxScale_y = 1.2f;

    float initialScale_x = 1.0f;
    float initialScale_y = 1.0f;

    bool isAnimScraps = false;
    bool isAnimFuel = false;
    bool isAnimOxygen = false;
    bool isAnimAmmo = false;
    bool isAnimRepairKit = false;

    // Start is called before the first frame update
    void Start()
    {
        //scubaTank.transform.localScale = SizeOriginal;
    }

    // Update is called once per frame
    void Update()
    {
        AnimScraps();
        AnimFuel();
        AnimOxygen();
        AnimAmmo();
        AnimRepairKit();

        if (Input.GetKeyDown(KeyCode.Space)) LaunchAnimScraps();
        if (Input.GetKeyDown(KeyCode.Space)) LaunchAnimFuel();
        if (Input.GetKeyDown(KeyCode.Space)) LaunchAnimOxygen();
        if (Input.GetKeyDown(KeyCode.Space)) LaunchAnimAmmo();
        if (Input.GetKeyDown(KeyCode.Space)) LaunchAnimRepairKit();
        
    }

    public void AnimScraps()
    {
        if (isAnimScraps)
        {
            animationTimer += Time.deltaTime * animationSpeed;
            if(animationTimer <= 1)
            {
                scrap.transform.localScale += new Vector3(Time.deltaTime * animationSpeed, Time.deltaTime * animationSpeed);
            }
            else
            {
                scrap.transform.localScale += new Vector3(Time.deltaTime * -animationSpeed, Time.deltaTime * -animationSpeed);
            }
            if(scrap.transform.localScale.x <= initialScale_x && scrap.transform.localScale.y <= initialScale_y)
            {
                scrap.transform.localScale = new Vector3(initialScale_x, initialScale_y);
                isAnimScraps = false;
            }
        }
        
    }

    public void LaunchAnimScraps()
    {
        if (isAnimScraps) return;
        animationTimer = 0f;
        isAnimScraps = true;
    }

    public void AnimFuel()
    {
        if (isAnimFuel)
        {
            animationTimer += Time.deltaTime * animationSpeed;
            if (animationTimer <= 1)
            {
                fuel.transform.localScale += new Vector3(Time.deltaTime * animationSpeed, Time.deltaTime * animationSpeed);
            }
            else
            {
                fuel.transform.localScale += new Vector3(Time.deltaTime * -animationSpeed, Time.deltaTime * -animationSpeed);
            }
            if (fuel.transform.localScale.x <= initialScale_x && fuel.transform.localScale.y <= initialScale_y)
            {
                fuel.transform.localScale = new Vector3(initialScale_x, initialScale_y);
                isAnimFuel = false;
            }
        }

    }

    public void LaunchAnimFuel()
    {
        if (isAnimFuel) return;
        animationTimer = 0f;
        isAnimFuel = true;
    }

    public void AnimOxygen()
    {
        if (isAnimOxygen)
        {
            animationTimer += Time.deltaTime * animationSpeed;
            if (animationTimer <= 1)
            {
                scubaTank.transform.localScale += new Vector3(Time.deltaTime * animationSpeed, Time.deltaTime * animationSpeed);
            }
            else
            {
                scubaTank.transform.localScale += new Vector3(Time.deltaTime * -animationSpeed, Time.deltaTime * -animationSpeed);
            }
            if (scubaTank.transform.localScale.x <= initialScale_x && scubaTank.transform.localScale.y <= initialScale_y)
            {
                scubaTank.transform.localScale = new Vector3(initialScale_x, initialScale_y);
                isAnimOxygen = false;
            }
        }

    }

    public void LaunchAnimOxygen()
    {
        if (isAnimOxygen) return;
        animationTimer = 0f;
        isAnimOxygen = true;
    }

    public void AnimAmmo()
    {
        if (isAnimAmmo)
        {
            animationTimer += Time.deltaTime * animationSpeed;
            if (animationTimer <= 1)
            {
                ammo.transform.localScale += new Vector3(Time.deltaTime * animationSpeed, Time.deltaTime * animationSpeed);
            }
            else
            {
                ammo.transform.localScale += new Vector3(Time.deltaTime * -animationSpeed, Time.deltaTime * -animationSpeed);
            }
            if (ammo.transform.localScale.x <= initialScale_x && ammo.transform.localScale.y <= initialScale_y)
            {
                ammo.transform.localScale = new Vector3(initialScale_x, initialScale_y);
                isAnimAmmo = false;
            }
        }

    }

    public void LaunchAnimAmmo()
    {
        if (isAnimAmmo) return;
        animationTimer = 0f;
        isAnimAmmo = true;
    }

    public void AnimRepairKit()
    {
        if (isAnimRepairKit)
        {
            animationTimer += Time.deltaTime * animationSpeed;
            if (animationTimer <= 1)
            {
                repairKit.transform.localScale += new Vector3(Time.deltaTime * animationSpeed, Time.deltaTime * animationSpeed);
            }
            else
            {
                repairKit.transform.localScale += new Vector3(Time.deltaTime * -animationSpeed, Time.deltaTime * -animationSpeed);
            }
            if (repairKit.transform.localScale.x <= initialScale_x && repairKit.transform.localScale.y <= initialScale_y)
            {
                repairKit.transform.localScale = new Vector3(initialScale_x, initialScale_y);
                isAnimRepairKit = false;
            }
        }

    }

    public void LaunchAnimRepairKit()
    {
        if (isAnimRepairKit) return;
        animationTimer = 0f;
        isAnimRepairKit = true;
    }

}
