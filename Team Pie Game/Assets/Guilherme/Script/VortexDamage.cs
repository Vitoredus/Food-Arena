using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexDamage : MonoBehaviour
{
    public float atkDamage;
    public float atkRange;
    public LayerMask enemyLayer;
    public int timesCalled = 0;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Especial+Dano"))
        {
            atkDamage = atkDamage + (atkDamage * PlayerPrefs.GetFloat("Especial+Dano"));
        }
       
    }

    void Update()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, atkRange, enemyLayer);

        if (timesCalled < 1 && enemies != null)
        {
            foreach (Collider enemy in enemies)
            {
               enemy.GetComponent<ILifeInterface>().TakeDamage(atkDamage);
            }
            StartCoroutine(Esperar());
        }
        
    }

    IEnumerator Esperar()
    {
        timesCalled = 1;   
        yield return new WaitForSeconds(0.5f);
        timesCalled = 0;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, atkRange);
    }
}
