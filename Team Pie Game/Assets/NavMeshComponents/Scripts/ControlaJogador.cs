using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlaJogador : MonoBehaviour
{

	public Camera cam;	
	public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
    		Ray raio = cam.ScreenPointToRay(Input.mousePosition);
    		RaycastHit hit;
    		if(Physics.Raycast(raio, out hit)) {
    		    agent.SetDestination(hit.point);
    		}
	 	} 

    }
}
