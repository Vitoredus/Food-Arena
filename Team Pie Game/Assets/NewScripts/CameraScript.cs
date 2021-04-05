using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private Transform ponto1, ponto2;

    [SerializeField]
    public float offsetY, offsetZ;

    private Transform player;

    void Start()
    {
        player = PlayerManager.instance.player.transform;
    }


    void LateUpdate()
    {
        float positionX = Mathf.Clamp(player.position.x, -3.5f, 3.5f);
        float positionY = Mathf.Clamp(player.position.y + offsetY, ponto1.position.y, ponto2.position.y);
        float positionZ = Mathf.Clamp(player.position.z + offsetZ, ponto1.position.z, ponto2.position.z);
        transform.position = new Vector3(positionX, positionY, positionZ);
    }
}
