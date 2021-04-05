using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadeEspecial : MonoBehaviour
{
    public GameObject PullOBJ;
    public float ForceSpeed;

    public float lifeTime;

    void Start()
    {
        RoomManager.instance.onNewRoom += DestroyProjectile;
        if (PlayerPrefs.HasKey("Especial+Duraçao"))
        {
            lifeTime += PlayerPrefs.GetFloat("Especial+Duraçao");
        }
        Invoke("DestroyProjectile", lifeTime);
    }


    public void OnTriggerStay(Collider coll)
    {

        if (coll.gameObject.tag == ("Enemy"))
        {
            
            PullOBJ = coll.gameObject;
            PullOBJ.transform.position = Vector3.MoveTowards(PullOBJ.transform.position, transform.position, ForceSpeed * Time.deltaTime);
        }   
    }

    void DestroyProjectile()
    {
        RoomManager.instance.onNewRoom -= DestroyProjectile;
        Destroy(transform.parent.gameObject);
    }

}
