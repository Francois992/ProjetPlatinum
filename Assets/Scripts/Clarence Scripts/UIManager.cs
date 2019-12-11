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

    [SerializeField] private Image Lifebar;

    [SerializeField] private Image oxygenBar = null;
    [SerializeField] private float maxOxygen = 100;
    private float _oxygen;
    [SerializeField] private int oxygenGain = 0; 

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

    [SerializeField] private float repairTime = 3;
    [SerializeField] private float repairValue = 20;
    private float startRepairValue;
    private float endRepairValue;

    private bool isRepairing = false;

    private float elapsedTime = 0f;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _oxygen = maxOxygen;
        UIAnimator = GetComponent<Animator>();
        scubaTankAmount = 5;
        fuelJerrycanAmount = 1;
        ammoAmount = 5;
        repairKitAmount = 1;
        scrapsAmount = 5;

        PlayerRigidBodyEntity.onRepairs += StartRepairs;
        MechaManager.CancelRepairs += StopRepairs;

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

    public void UpdateLifeBar(float fillValue)
    {
        Lifebar.fillAmount = fillValue;
    }

    private void Update()
    {
        if (isRepairing)
        {
            Repairing();

            if(Lifebar.fillAmount >= endRepairValue || Lifebar.fillAmount == 1)
            {
                isRepairing = false;
                MechaManager.Instance.currentLife += Mathf.RoundToInt((repairValue / 100) * MechaManager.Instance.fullLife);
                if (MechaManager.Instance.currentLife > MechaManager.Instance.fullLife) MechaManager.Instance.currentLife = MechaManager.Instance.fullLife;
                ChangeInventory("Remove", ref repairKitAmount, 1);
                elapsedTime = 0;
            }
        }

        UpdateOxygen();
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

    private void StartRepairs()
    {
        if (repairKitAmount <= 0 || MechaManager.Instance.currentLife == MechaManager.Instance.fullLife) return; 

        if (isRepairing)
        {
            return;
        }
        else
        {
            isRepairing = true;
            startRepairValue = Lifebar.fillAmount;
            endRepairValue = startRepairValue + (repairValue / 100);
        }
    }

    private void StopRepairs()
    {
        if (!isRepairing) return;

        else
        {
            isRepairing = false;
            Lifebar.fillAmount = startRepairValue;
            elapsedTime = 0;
        }
    }

    private void Repairing()
    {
        elapsedTime += Time.deltaTime;

        float ratio = elapsedTime / repairTime;

        Lifebar.fillAmount = Mathf.Lerp(startRepairValue, endRepairValue, ratio);
        FindObjectOfType<SoundManager>().Play("UpdatingGauges");
    }

    private void UpdateOxygen()
    {
        oxygenBar.fillAmount = _oxygen / maxOxygen;
        _oxygen -= Time.deltaTime;
        if (_oxygen <= 0)
        {
            _oxygen = 0;
        }
        else
        {
            oxygenBar.fillAmount = _oxygen / maxOxygen;
            _oxygen -= Time.deltaTime;
        }
    }

    public void FillOxygen()
    {
        _oxygen += oxygenGain;
        ChangeInventory("Remove", ref UIManager.instance.scubaTankAmount, UIManager.instance._initialCost);
        FindObjectOfType<SoundManager>().Play("UpdatingGauges");
    }

    public float GetOxygen()
    {
        return _oxygen;
    }
}
