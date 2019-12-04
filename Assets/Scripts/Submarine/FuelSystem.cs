using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelSystem : MonoBehaviour
{

    public float startFuel = 0f; //starting fuel
    public float maxFuel = 100f;
    public float fuelConsumptionRate = 0.2f;
    public Image fuelIndicatorImg;
    //public Text fuelIndicatorTxt;

    private float addedFuelAmount = 20f;

    // Start is called before the first frame update
    void Start()
    {

        //cap the fuel
        if(startFuel > maxFuel)
            startFuel = maxFuel;

        //fuelIndicatorImg.fillAmount = maxFuel;
        UpdateUI();
    }

    public void Update()
    {

        //reduce the fuel level and update the UI
        
        UpdateUI();

    }

    void UpdateUI()
    {

        fuelIndicatorImg.fillAmount = startFuel/100;
        
        //fuelIndicatorTxt.text = "Fuel left : " + startFuel.ToString("0") + " %";

        if (startFuel <= 0)
        {
            startFuel = 0;
            //fuelIndicatorTxt.text = "Out of fuel !!!";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GasCan"))
        {
            ResplenishFuel();
            Destroy(other.gameObject);
        }
    }

    private void ResplenishFuel()
    {

        if(UIManager.instance.fuelJerrycanAmount > 0)
        {
            UIManager.instance.ChangeInventory("Remove", ref UIManager.instance.fuelJerrycanAmount, UIManager.instance._initialCost);
            startFuel += addedFuelAmount;
            if (startFuel > maxFuel)
                startFuel = maxFuel;
        }
        else
        {
            //UIManager.instance.UIAnimator.SetTrigger("NoFuel");
        }

        UpdateUI();
        
    }

    public void LoseFuel()
    {
        startFuel -= Time.deltaTime * fuelConsumptionRate;
    }

}
