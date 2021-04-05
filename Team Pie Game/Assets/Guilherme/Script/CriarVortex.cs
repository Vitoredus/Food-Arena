using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriarVortex : MonoBehaviour
{
    public float lifeTime;
    public GameObject batedeira;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }


    private void OnCollisionEnter(Collision col)
    {
      //  if (col.gameObject.tag == "Enemy")
     //   {
            CriarBatedeira();
            Destroy(gameObject);
    //    }
    //    else
     //   {
     //       Destroy(gameObject);
     //   }

    }

    void DestroyProjectile()
    {
        CriarBatedeira();
        Destroy(gameObject);
    }

    private void CriarBatedeira()
    {
        //instancia batedeira
        Instantiate(batedeira, transform.position, Quaternion.Euler(-90, 0, 0));
    }


}
