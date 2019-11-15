using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.UI;

public class GunPanel : MonoBehaviour
{
    public PlayerRigidBodyEntity user;

    public GunController gunController;

    public Turret myTurret;

    public Canvas myUi;

    private bool reload = false;

    [SerializeField] private Text triggerText;
    [SerializeField] private Image loadFill;

    private float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        myUi.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (reload)
        {
            elapsedTime += Time.deltaTime;

            float ratio = elapsedTime / 3f;

            loadFill.fillAmount = Mathf.Lerp(0, 1, ratio);
        }

        if(elapsedTime >= 3)
        {
            onReload();
        }
    }

    public void OnUsed()
    {
        gunController.player = ReInput.players.GetPlayer(user.reController);
        gunController.isActivated = true;
        myUi.gameObject.SetActive(true);
    }

    public void OnDropped()
    {
        gunController.isActivated = false;
        myUi.gameObject.SetActive(false);
    }

    public void OnShoot()
    {
        reload = true;
        triggerText.gameObject.SetActive(false);
        loadFill.fillAmount = 0;
    }

    public void onReload()
    {
        reload = false;
        triggerText.gameObject.SetActive(true);
        elapsedTime = 0f;
    }
}
