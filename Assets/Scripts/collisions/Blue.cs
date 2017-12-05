using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue : MonoBehaviour {

	public GameObject minionred;
	public GameObject minionblue;
	public GameObject minionyellow;
	public GameObject minionmagenta;
	public GameObject miniongreen;
	public GameObject minioncyan;

	public GameObject blue;

	private Renderer rend;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rend = GetComponent<Renderer> ();
	}
	void Update () {
	

	}


	//si nom. "minion" + gameobject == nom.colider {



	void OnCollisionEnter (Collision col)
	{
		if(col.collider.name == "minionred")
		{
			
			rend.material.color = Color.white;
			Destroy(col.gameObject);
			//Instantiate minionmagenta
		}
		if(col.collider.name == "miniongreen")
		{
			rend.material.color = Color.white;
			Destroy (miniongreen);
			//Instantiate minioncyan
		}
		if(col.collider.name == "minionyellow")
		{
			rend.material.color = Color.grey;
			Destroy (minionyellow);
		}
	/*	if(col.collider.name == "minionmagenta")
		{
			rend.material.color = Color.white;
			//Destroy (minion);
			//Instantiate
		}
		if(col.collider.name == "minioncyan")
		{
			rend.material.color = Color.white;
			//Destroy (minion);
			//Instantiate
		}*/
		if(col.collider.name == "minionblue")
		{
			rend.material.color = Color.blue;
			Destroy (minionblue);
		}

	}
}