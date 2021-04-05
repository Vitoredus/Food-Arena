using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfeiteiroSkill2 : SkillClass
{
    public GameObject[] pontos;
    public GameObject poça;
    public GameObject poçaUp1, poçaUp2;
    public GameObject pontoPoça;

    public AudioSource skillSound;

    private void Start()
    {
        if (PlayerPrefs.HasKey("+Rapido"))
        {
            tempoDeRecarga -= PlayerPrefs.GetFloat("+Rapido");
        }

        if (PlayerPrefs.HasKey("+AreaDePoça"))
        {
            if(PlayerPrefs.GetInt("+AreaDePoça") == 1)
            {
                poça = poçaUp1;
            }
            else
            {
                poça = poçaUp2;
            }
        }
    }

    void FixedUpdate()
    {
        RotacionarEAtacar(this.transform, true);
        if (atirando == true)
        {
            atirando = false;
            this.GenericShoot();
        }
    }

    public override void GenericShoot()
    {
        base.GenericShoot();
        Instantiate(poça, pontoPoça.transform.position, pontoPoça.transform.rotation);
        for (int i = 0; i < pontos.Length; i++)
        {
            Instantiate(projetil, pontos[i].transform.position, pontos[i].transform.rotation);
        }
        ZerarRecarga();
        skillSound.Play();
        
    }
}
