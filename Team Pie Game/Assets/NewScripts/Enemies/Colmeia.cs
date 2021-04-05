using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colmeia : EnemyClass
{
    public float maxRange, minRange;
    public float tempoDeSpawn;
    public GameObject minionPrefab;
    
    void Start()
    {
        EnemyStart();
        StartCoroutine(SpawnMinion());
    }

    
    void Update()
    {
        SeguirAlvo();
    }

    IEnumerator SpawnMinion()
    {
        while (true)
        {
            yield return new WaitForSeconds(tempoDeSpawn);

            float x = Random.Range(minRange, maxRange);
            float z = Random.Range(minRange, maxRange);

            x = Random.value <= .5 ? x*-1 : x;
            z = Random.value <= .5 ? z*-1 : z;

            Vector3 position = new Vector3(transform.position.x + (x/2),
                transform.position.y, transform.position.z + (z/2));

            Instantiate(minionPrefab, position, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, minRange);
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }
}
