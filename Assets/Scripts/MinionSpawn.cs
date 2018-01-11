using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawn : MonoBehaviour {

	GameObject spawn1;
	HexInfo Nucli;

	public GameObject minionCyan;
	public GameObject minionMagenta;
	public GameObject minionYellow;

	void Start(){
	
		InvokeRepeating ("Spawn", 1f, 5f);


	}

	char RandomColor(){

		int Rand = Random.Range(0, 3);

		switch (Rand) {

		case 0:
			return 'C';
		case 1: 
			return 'M';
		case 2:
			return 'Y';
		default:

			Debug.Log ("random error spawn");
			return 'E';

		}
	}

	void Spawn(){
	
		//yield return new WaitForSeconds (1);

		spawn1 = GameObject.Find ("Hex_0_11");
		Nucli = GameObject.Find ("Hex_7_7").GetComponentInChildren<HexInfo>();

		char MinionColorIdenityfier = RandomColor ();

		if ( MinionColorIdenityfier == 'C') {

			Instantiate (minionCyan, spawn1,  MinionColorIdenityfier);
		} 
		else if ( MinionColorIdenityfier == 'M') {
		
			Instantiate (minionMagenta, spawn1,  MinionColorIdenityfier);
		} 
		else if ( MinionColorIdenityfier == 'Y'){

			Instantiate (minionYellow, spawn1,  MinionColorIdenityfier);
		}

	}

	void Instantiate(GameObject minion, GameObject spawn1, char MinionColorIdentyfier){


		MinionMovement minionScript = minion.GetComponent<MinionMovement> ();
		HexInfo spawn1Hex = spawn1.GetComponentInChildren<HexInfo> ();

		minionScript.ActualHex = spawn1Hex;
		minionScript.Nucli = Nucli;
		minionScript.ColorIdentifier = MinionColorIdentyfier;

		Instantiate (minion, spawn1.transform.position, minionCyan.transform.rotation);

	}


}
