using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{

    public GameObject explosion;
    [SerializeField] private float damage = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag == "Mecha")
        {
            MechaManager.Instance.TakeDamage(damage);
            Explode();
        }
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Explode();
        }
    }

    public void Explode()
    {
        CameraShaker.Instance.startShake();
        Ripple.instance.RippleEffect();
        GameObject thisExplosion = Instantiate(explosion, transform.position, transform.rotation);
        FindObjectOfType<SoundManager>().Play("Explosion");
        Destroy(thisExplosion, 0.5f);
        Destroy(gameObject);
    }
}
