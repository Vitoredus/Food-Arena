using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BatedeiraScript : MonoBehaviour
{
    [SerializeField] private GameObject vortex;
    [SerializeField] private GameObject vortexUp1, vortexUp2;

    [HideInInspector] public Vector3 endLine;

    public float speed;
    float distanceTravelled;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (PlayerPrefs.HasKey("Especial+Area"))
        {
            int ctrl = PlayerPrefs.GetInt("Especial+Area");
            if(ctrl == 1)
            {
                vortex = vortexUp1;
            }
            else
            {
                vortex = vortexUp2;
            }
        }

        DOTweenModulePhysics.DOJump(rb ,endLine, 2, 1, 0.8f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            InstanciaVortex();
            this.gameObject.SetActive(false);
            Invoke("Destruir", 5f);
        }
        
    }

    void Destruir()
    {
        Destroy(this.gameObject);
    }

    void InstanciaVortex()
    {
        Instantiate(vortex, transform.position, Quaternion.Euler(-90, 0, 0));
    }
}
