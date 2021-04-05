using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    public SphereCollider colisor;

    private void OnEnable()
    {
        StartCoroutine(ActivatePortal());        
    }

    IEnumerator ActivatePortal()
    {
        yield return new WaitForSeconds(2f);
        colisor.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            colisor.enabled = false;
            StartCoroutine(RoomManager.instance.NextRoom());
        }
    }
}
