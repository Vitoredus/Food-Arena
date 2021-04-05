using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocidade;
    public float atkDamage;
    public float lifeTime;
    public string targetTag;

    [Header("KnockBack")]
    public bool temKnockBack;
    public float knockbackForce;

    public GameObject particles;

    void Start()
    {
        Invoke("DestroyBullet", lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * velocidade * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag(targetTag))
        {
            col.gameObject.GetComponent<ILifeInterface>().TakeDamage(atkDamage);
            if (temKnockBack)
            {
                col.GetComponent<IKnockBack>().KnockBack(this.transform, knockbackForce);
            }
            Destroy(gameObject);
        }
        if (col.gameObject.CompareTag("Parede"))
        {
            particles.transform.parent = null;
            Destroy(gameObject);
        }
    }


    void DestroyBullet()
    {
        particles.transform.parent = null;
        Destroy(this.gameObject);
    }
}
