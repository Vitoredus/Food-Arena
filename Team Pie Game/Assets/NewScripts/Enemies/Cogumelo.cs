using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cogumelo : EnemyClass
{
    [SerializeField] private bool minion;

    private void Start()
    {
        if (minion)
        {
            transform.SetParent(GameObject.Find("EnemiesContainer").transform);
        }
        EnemyStart();
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

}
