using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfeiteiroSkill1 : SkillClass
{
    [SerializeField] private int numOfBullets;

    public AudioSource tirosom;

    public float atkDamage;
    public float lifeTime;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Skill1Bullets"))
        {
            numOfBullets += PlayerPrefs.GetInt("Skill1Bullets");
        }

        if (PlayerPrefs.HasKey("+DanoPorUpgrade"))
        {
            atkDamage += PlayerPrefs.GetFloat("+DanoPorUpgrade");
        }

        if (PlayerPrefs.HasKey("+Dano"))
        {
            atkDamage = atkDamage + (atkDamage * PlayerPrefs.GetFloat("+Dano"));
        }

        if (PlayerPrefs.HasKey("+ChanceDe+Dano"))
        {
            if (Random.Range(0, 100) <= PlayerPrefs.GetInt("+ChanceDe+Dano"))
            {
                atkDamage = atkDamage + (atkDamage * 0.5f);
            }
        }

        if (PlayerPrefs.HasKey("+Alcance"))
        {
            lifeTime = lifeTime + (lifeTime * PlayerPrefs.GetFloat("+Alcance"));
        }
    }

    void FixedUpdate()
    {
        RotacionarEAtacar(this.transform);
        if(atirando == true)
        {
            atirando = false;
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        GenericShoot();
        for (int i = 0; i < numOfBullets; i++)
        {
            var bullet = Instantiate(projetil, atkPoint2.transform.position, atkPoint2.transform.rotation);
            bullet.GetComponent<Bullet>().atkDamage = this.atkDamage;
            bullet.GetComponent<Bullet>().lifeTime = this.lifeTime;
            tirosom.Play();
            tirosom.pitch = (Random.Range(0.7f, 1f));
            yield return new WaitForSeconds(0.15f);
        }
    }
}
