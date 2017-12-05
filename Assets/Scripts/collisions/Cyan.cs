using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyan : MonoBehaviour {

	public GameObject minionred;
	public GameObject minionblue;
	public GameObject minionyellow;
	public GameObject minionmagenta;
	public GameObject miniongreen;
	public GameObject minioncyan;

	public GameObject cyan;

	private Renderer rend;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rend = GetComponent<Renderer> ();
	}
	void Update () {


	}

	void OnCollisionEnter (Collision col)
	{
		if(col.collider.name == "minionred")
		{
			rend.material.color = Color.grey;
			Destroy (minionred);
		}
		if(col.collider.name == "minionyellow")
		{
			rend.material.color = Color.white;
			Destroy (minionyellow);
			//Instantiate miniongreen
		}
		if(col.collider.name == "minionmagenta")
		{
			rend.material.color = Color.white;
			Destroy (minionmagenta);
			//Instantiate minionblue
		}
		if(col.collider.name == "minioncyan")
		{
			rend.material.color = Color.cyan;
			Destroy (minioncyan);
		}
	/*	if(col.collider.name == "minionblue")
		{
			rend.material.color = Color.blue;
			Destroy (minionblue);
		}
		if(col.collider.name == "miniongreen")
		{
			rend.material.color = Color.white;
			Destroy (miniongreen);
	*/

	}
}