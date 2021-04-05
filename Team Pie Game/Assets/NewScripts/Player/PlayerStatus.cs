using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour, ILifeInterface, IKnockBack
{
    public GameObject characterAttacks;
    [SerializeField] private HealthBar healthBar;

    public PlayerClass player = new PlayerClass();

    private float currentHealth;

    private Rigidbody rb;

    private bool invencivel = false;
    private bool taMorto = false;

    void Awake()
    {
        HelpfulFunctions.LoadStatus(player);
    }

    void Start()
    {
        UIManager.instance.reviverPlayer += Reviver;

        rb = GetComponent<Rigidbody>();
        currentHealth = player.hp;
        healthBar.SetMaxHealth(currentHealth);
    }

    public void AddIngredient(int amount)
    {
        PlayerClass.ingredients += amount;
        UIManager.instance.UpDateIngredientText();
    }

    public void AddExperience(int amount)
    {
        player.experience += amount;
        while (player.experience >= player.experienceToNextLevel)
        {
            player.level++;
            player.experience -= player.experienceToNextLevel;
        }

    }

    public int GetPlayerExperience()
    {
        return player.experience;
    }

    public void TakeDamage(float damage)
    {
        if (!invencivel && currentHealth > 0)
        {
            currentHealth -= damage;
        }

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0 && !taMorto)
        {
            taMorto = true;
            GetComponent<Movimento>().enabled = false;
            characterAttacks.SetActive(false);
            if (!UIManager.instance.AnuncioJaUsado)
            {
                UIManager.instance.DeathScreenControl(true);
            }
            else
            {
                UIManager.instance.ActiveExpScreen();
            }
        }


    }

    public void AddHealth(int healthToAdd)
    {
        if(currentHealth < player.hp)
        {
            currentHealth += healthToAdd;
            if(currentHealth > player.hp)
            {
                currentHealth = player.hp;
            }
            healthBar.SetHealth(player.hp);
        }
    }

    private void Reviver()
    {
        currentHealth = player.hp;
        healthBar.SetHealth(currentHealth);
        taMorto = false;
        GetComponent<Movimento>().enabled = true;
        characterAttacks.SetActive(true);
        StartCoroutine(Invencibilidade());
    }

    public void Save()
    {
        HelpfulFunctions.SaveStatus(player);
    }

    IEnumerator Invencibilidade()
    {
        invencivel = true;
        yield return new WaitForSeconds(2f);
        invencivel = false;
    }

    public void KnockBack(Transform bala, float knockbackValue)
    {
        rb.isKinematic = false;
        Vector3 direction = transform.position - bala.position;
        direction.y = 0;
        rb.AddForce(direction.normalized * knockbackValue, ForceMode.Impulse);   
    }

    public PlayerClass GetPlayerClass()
    {
        return player;
    }
}
