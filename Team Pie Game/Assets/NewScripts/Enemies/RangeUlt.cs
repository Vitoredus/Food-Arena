using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeUlt : MonoBehaviour
{

    [SerializeField] private FixedJoystick ultJoystick;

    public Transform player;
    private Vector3 newLocation;

    public float velocidade;
    public float maxDistance = 5;
    public float positionY;


    private void OnEnable()
    {
        transform.position = new Vector3(player.position.x, positionY, player.position.z);
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector3(Mathf.Clamp(ultJoystick.Horizontal, -0.2f, 0.2f), (Mathf.Clamp(ultJoystick.Vertical, -0.2f, 0.2f) * -1), 0) * velocidade * Time.deltaTime);
        //transform.Translate(new Vector3(ultJoystick.Horizontal, (ultJoystick.Vertical *- 1), 0) * velocidade * Time.deltaTime);

        if(Vector3.Distance(player.position, transform.position) > maxDistance)
        {
            transform.position = (transform.position - new Vector3(player.position.x, positionY, player.position.z)).normalized * maxDistance 
                + new Vector3(player.position.x, positionY, player.position.z);
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -5, 5), positionY, Mathf.Clamp(transform.position.z, -5, 5));
    }
}
