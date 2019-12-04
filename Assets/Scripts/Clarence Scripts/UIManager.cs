using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scubaTankText;
    [SerializeField]
    private Text fuelJerrycanText;
    [SerializeField]
    private Text ammoText;
    [SerializeField]
    private Text repairKitText;
    [SerializeField]
    private Text scrapsText;

    public int scubaTankAmount;
    public int fuelJerrycanAmount;
    public int ammoAmount;
    public int repairKitAmount;
    public int scrapsAmount;

    private const int _MAXAMOUNT = 99;
    private const int _MINAMOUNT = 0;
    public int _initialCost = 1;
    public int _initialGain = 1;

    public static UIManager instance;

    public Animator UIAnimator;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIAnimator = GetComponent<Animator>();
        scubaTankAmount = 0;
        fuelJerrycanAmount = 0;
        ammoAmount = 5;
        repairKitAmount = 0;
        scrapsAmount = 5;

        UpdateHUD();
    }


    //Permet d'ajouter/ d'enlever un nombre défini d'objet dans l'inventaire
    public void ChangeInventory(string action, ref int itemStorage, int amount)
    {
        if(action == "Add")
        {
            if (itemStorage < _MAXAMOUNT)
            {
                itemStorage += amount;
                if (itemStorage > _MAXAMOUNT)
                {
                    itemStorage = _MAXAMOUNT;
                }
            }
            else return;
            UpdateHUD();
        }
        else if(action == "Remove")
        {
            if (itemStorage > _MINAMOUNT)
            {
                itemStorage -= amount;
                if(itemStorage < _MINAMOUNT)
                {
                    itemStorage = _MINAMOUNT;
                }
            }
            else
            {
                itemStorage = _MINAMOUNT;
            }
            UpdateHUD();
        }
        else
        {
            return;
        }
    }

    
    //Met à jour les éléments d'UI du HUD
    private void UpdateHUD()
    {
        scubaTankText.text = ": " + scubaTankAmount;
        fuelJerrycanText.text = ": " + fuelJerrycanAmount;
        ammoText.text = ": " + ammoAmount;
        repairKitText.text = ": " + repairKitAmount;
        scrapsText.text = ": " + scrapsAmount;
    }

    
}
