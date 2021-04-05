using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BalaCapenga : MonoBehaviour
{
    public float velocidade;
    public int atkDamage;
    public float lifeTime;
    public string targetTag;

    [Header("KnockBack")]
    public bool temKnockBack;
    public float knockbackForce;

    public GameObject part;

    void Start()
    {
        Invoke("DestroyBullet", lifeTime);
    }

    void Update()
    {
        MoveBullet();
    }

    public void MoveBullet()
    {
        transform.Translate(Vector3.forward * velocidade * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag(targetTag))
        {
            col.gameObject.GetComponent<ILifeInterface>().TakeDamage(atkDamage);
            if (temKnockBack)
            {
                col.collider.GetComponent<IKnockBack>().KnockBack(this.transform, knockbackForce);
            }
            Destroy(gameObject);
        }
        else
        {
            
            Destroy(gameObject);
        }

    }


    void DestroyBullet()
    {
        part.transform.parent = null;
        Destroy(this.gameObject);
    }
}
