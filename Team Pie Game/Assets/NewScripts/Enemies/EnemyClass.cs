using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyClass : MonoBehaviour, ILifeInterface, IKnockBack
{
    public Enemy enemyObject;
    public Slider healthBar;

    protected NavMeshAgent agent;
    protected Rigidbody rb;
    protected Transform target;

    private bool isImpaired = false;
    private bool isDead;
    private bool acertou;

    private float currentHp = 0;

    protected delegate void OnDeath();
    protected OnDeath onDeath;

    public AudioSource damageSound;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Poça"))
        {
            ChangeEnemySpeed(agent.speed - other.GetComponent<Poça>().slowEffect);
            isImpaired = true;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Poça"))
        {
            ChangeEnemySpeed(enemyObject.speed);
            isImpaired = false;
        }
    }

    protected void SeguirAlvo()
    {
        agent.SetDestination(target.transform.position);
    }

    public void EnemyStart()
    {
        rb = GetComponent<Rigidbody>();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyObject.speed;
        currentHp = enemyObject.hp;
        healthBar.maxValue = enemyObject.hp;
        healthBar.value = currentHp;
    }

    public void DropIngredient()
    {
        for (int i = 0; i < enemyObject.numOfIngredientsToDrop; i++)
        {
            Instantiate(enemyObject.ingridentPrefab, transform.position, transform.rotation);
        }
    }

    public virtual void Die()
    {
        if (!isDead)
        {
            onDeath?.Invoke();
            isDead = true;
            Instantiate(enemyObject.deathEffect, transform.position, transform.rotation);
            DropIngredient();
            PlayerManager.instance.AddExpRuntime(this.enemyObject.expValue);
            Destroy(this.gameObject);
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
        agent.speed = enemyObject.speed;
        acertou = false;
    }

    public void TakeDamage(float damage)
    {
        if (isImpaired)
        {
            float newDamage = damage * 1.5f;
            currentHp -= newDamage;
            healthBar.value = currentHp;
        }
        else
        {
            currentHp-= damage;
            healthBar.value = currentHp;
        }
        
        if (currentHp <= 0)
        {
            Die();
        }
        else
        {
            damageSound.Play();
            damageSound.pitch = (Random.Range(0.7f, 1.1f));
        }


    }

    private void ChangeEnemySpeed(float newSpeed)
    {
        agent.speed = newSpeed;
    }

}
