using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class RoomManager : MonoBehaviour
{
    #region Singleton

    public static RoomManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    [SerializeField] private List<GameObject> easyRooms = new List<GameObject>();
    [SerializeField] private List<GameObject> mediumRooms = new List<GameObject>();
    [SerializeField] private List<GameObject> hardRooms = new List<GameObject>();

    [SerializeField] private Transform spawnRoomPoint;

    [SerializeField] private GameObject currentRoom;

    [SerializeField] private Animator fadeImage;
    [SerializeField] private NavMeshSurface navMesh;

    public Action onNewRoom;

    private int roomsPassed;

    void InstantiateRoom()//Instancia uma sala aleatoria e remove ela da lista
    {
        onNewRoom?.Invoke();

        if(roomsPassed < 10)//salas faceis
        {
            int random = UnityEngine.Random.Range(0, easyRooms.Count);
            currentRoom = Instantiate(easyRooms[random], spawnRoomPoint.position, Quaternion.identity);
            easyRooms.Remove(easyRooms[random]);
        }
        else if(roomsPassed >= 10 && roomsPassed < 20)//salas medias
        {
            int random = UnityEngine.Random.Range(0, mediumRooms.Count);
            currentRoom = Instantiate(mediumRooms[random], spawnRoomPoint.position, Quaternion.identity);
            mediumRooms.Remove(mediumRooms[random]);
        }
        else if (roomsPassed >= 20 && roomsPassed <= 30)//salas dificeis
        {
            if(hardRooms.Count > 0)
            {
                int random = UnityEngine.Random.Range(0, hardRooms.Count);
                currentRoom = Instantiate(hardRooms[random], spawnRoomPoint.position, Quaternion.identity);
                hardRooms.Remove(hardRooms[random]);
            }
            else
            {
                HelpfulFunctions.LoadScene("MenuInicial");
            }
            
        }
        
        roomsPassed++;
        navMesh.BuildNavMesh();
    }

    
    public IEnumerator NextRoom()
    {
        fadeImage.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        GetComponent<PlayerManager>().ResetPlayerPos();
        Destroy(currentRoom.gameObject);
        InstantiateRoom();
        fadeImage.SetTrigger("FadeOut");
    }
}
