using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    #endregion

    public GameObject player;
    public Vector3 initPosition;//Posição inicial do jogador

    private int experienceRuntime;


    public void ResetPlayerPos() //Reseta posição do jogador
    {
        player.transform.position = initPosition;
    }

    //public void AddExperienceToPlayer()
    //{
    //    player.GetComponent<PlayerStatus>().AddExperience(experienceRuntime);
    //}

    public void AddExpRuntime(int amount)
    {
        experienceRuntime += amount;
    }

    public int GetExperienceRuntime()
    {
        return experienceRuntime;
    }
}
