using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poça : MonoBehaviour
{
    public float lifeTime;

    public float slowEffect;

    void Start()
    {
        RoomManager.instance.onNewRoom += DestroyItself;

        if (PlayerPrefs.HasKey("+SlowDown"))
        {
            slowEffect = slowEffect + (slowEffect * PlayerPrefs.GetFloat("+SlowDown"));
        }

        Invoke("Destroy", lifeTime);
    }

    private void Destroy()
    {
        StartCoroutine(DestroyProjectile());
    }

    IEnumerator DestroyProjectile()
    {
        transform.position = new Vector3(transform.position.x, -50, transform.position.z);
        yield return new WaitForSeconds(3f);
        DestroyItself();
    }

    void DestroyItself()
    {
        RoomManager.instance.onNewRoom -= DestroyItself;
        Destroy(gameObject);
    }
}
