using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class BossScript : MonoBehaviour
{
    private Rigidbody rb;

    public float shootDelay;

    public NavMeshAgent agent;

    private Transform target;
    public Transform shootPoint;

    public GameObject bulletPrefab;
    public GameObject dangerZone;

    public LayerMask groundLayer;
    private bool isGrouded;

    private bool canShoot;
    private bool acertou;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Shoot());
        TeleportAttack();
    }

    private void Update()
    {

        if (isGrouded)
        {
            agent.enabled = true;
            rb.isKinematic = true;
            rb.useGravity = false;
            dangerZone.SetActive(true);
            canShoot = true;
        }
        else
        {
            agent.enabled = false;
            dangerZone.SetActive(false);
            canShoot = false;
        }

        if (agent.enabled)
        {
            agent.SetDestination(target.position);
        }
            

        shootPoint.LookAt(target.position);

    }

    private void FixedUpdate()
    {
        isGrouded = Physics.Raycast(transform.position, Vector3.down, 0.2f, groundLayer);
    }

    void TeleportAttack()
    {
        StartCoroutine(Teleport());
    }

    IEnumerator Teleport()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10, 20));
            rb.isKinematic = false;
            agent.enabled = false;
            transform.position = new Vector3(0, 5, 0);

            yield return new WaitForSeconds(2);
            transform.position = new Vector3(target.position.x, 5, target.position.z);

            yield return new WaitForSeconds(0.2f);
            rb.useGravity = true;
        }
        
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootDelay);
            if (canShoot)
            {
                Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            }
        }
        
    }

    public void KnockBack(Transform bala, float knockbackValue)
    {
        if (!acertou)
        {
            rb.isKinematic = false;

            acertou = true;
            Vector3 direction = transform.position - bala.position;
            direction.y = 0;

            agent.speed = 0;
            StartCoroutine(AtivaAgent());
            rb.AddForce(direction.normalized * knockbackValue, ForceMode.Impulse);
        }
    }

    IEnumerator AtivaAgent()
    {
        yield return new WaitForSeconds(0.5f);
        rb.isKinematic = true;
        agent.speed = 2;
        acertou = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStatus>().TakeDamage(1);
        }
    }
}
