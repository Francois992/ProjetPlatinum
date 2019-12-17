using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple : MonoBehaviour
{
    public static Ripple instance;

    public Material RippleMaterial;
    public float MaxAmount = 50f;

    [Range(0, 1)]
    public float Friction = .9f;

    private float Amount = 0f;

    void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Update()
    {

        RippleMaterial.SetFloat("_Amount", Amount);
        Amount *= Friction;
    }

    public void RippleEffect()
    {
        Amount = MaxAmount;
        Vector3 pos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        RippleMaterial.SetFloat("_CenterX", pos.x);
        RippleMaterial.SetFloat("_CenterY", pos.y);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, RippleMaterial);
    }
}
