using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    private GameObject enemiesContainer;
    public GameObject door;

    private bool canSpawn = true;

    void Start()
    {
        enemiesContainer = transform.Find("EnemiesContainer").gameObject;

        if (transform.name == "StartRoom")
        {
            canSpawn = true;
        }
    }
    
    void Update()
    {
        if (enemiesContainer.transform.childCount == 0 && canSpawn)
        {
            canSpawn = false;
            door.SetActive(true);
        }
    }

}
