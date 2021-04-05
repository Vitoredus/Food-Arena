using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeLike : EnemyClass
{
    public bool foiInstanciado = false;
    public GameObject clonePrefab;

    private void Start()
    {
        EnemyStart();
        onDeath += EventoDeMorte;
    }

    private void Update()
    {
        SeguirAlvo();
    }

    IEnumerator Attack(Collider other)
    {
        while (true)
        {
            other.gameObject.GetComponent<ILifeInterface>().TakeDamage(enemyObject.damage);
            yield return new WaitForSeconds(1f);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Attack(other));
        }
        base.OnTriggerEnter(other);
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
        }
        base.OnTriggerExit(other);
    }

    public void EventoDeMorte()
    {
        if (!foiInstanciado)
        {
            for (int i = 0; i < 2; i++)
            {
                var clone = Instantiate(clonePrefab, transform.position, transform.rotation);
                clone.transform.SetParent(GameObject.Find("EnemiesContainer").transform);
                clone.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                clone.GetComponent<SlimeLike>().foiInstanciado = true;
            }
        }
        
    }

}
