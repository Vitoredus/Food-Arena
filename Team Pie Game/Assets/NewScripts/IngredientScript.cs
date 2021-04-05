using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientScript : MonoBehaviour
{
    public float upForce;
    public float sideForce;

    private GameObject player;
    public int ingredientValue;

    private bool goToPlayer = false;
    Rigidbody rb;

    void Start()
    {
        RoomManager.instance.onNewRoom += GiveIngredientsAndDestroy;
        player = PlayerManager.instance.player;

        float forceX = Random.Range(-sideForce, sideForce);
        float forceZ = Random.Range(-sideForce, sideForce);

        Vector3 force = new Vector3(forceX, upForce, forceZ);

        rb = GetComponent<Rigidbody>();
        rb.velocity = force;
        StartCoroutine(GoToPlayer());    
    }

    
    void LateUpdate()
    {
        if (goToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 5 * Time.deltaTime);
        }
    }

    IEnumerator GoToPlayer()
    {
        yield return new WaitForSeconds(1f);
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        goToPlayer = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStatus>().AddIngredient(ingredientValue);
            AudioMaster.Instance.CoinSound();
            RoomManager.instance.onNewRoom -= GiveIngredientsAndDestroy;
            Destroy(this.gameObject);
        }
    }

    void GiveIngredientsAndDestroy()
    {
        player.GetComponent<PlayerStatus>().AddIngredient(ingredientValue);
        RoomManager.instance.onNewRoom -= GiveIngredientsAndDestroy;
        Destroy(this.gameObject);
    }
}
