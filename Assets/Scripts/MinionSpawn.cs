using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawn : MonoBehaviour {

	GameObject spawn1;
	HexInfo Nucli;

	private int counter;

	public GameObject minionCyan;
	public GameObject minionMagenta;
	public GameObject minionYellow;

	public GameObject minionCyanS;
	public GameObject minionMagentaS;
	public GameObject minionYellowS;

	public GameObject minionCyanRandom;
	public GameObject minionMagentaRandom;
	public GameObject minionYellowRandom;

	void Start(){
	
		InvokeRepeating ("Spawn", 1f, 3f);


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

		spawn1 = GameObject.Find ("Hex_0_5");
		Nucli = GameObject.Find ("Hex_7_7").GetComponentInChildren<HexInfo>();

		char MinionColorIdenityfier = RandomColor ();

		if (counter == 100) 
		{
			if (MinionColorIdenityfier == 'C') {

				InstantiateForward (minionCyan, spawn1, MinionColorIdenityfier);
			} else if (MinionColorIdenityfier == 'M') {
			
				InstantiateForward (minionMagenta, spawn1, MinionColorIdenityfier);
			} else if (MinionColorIdenityfier == 'Y') {

				InstantiateForward (minionYellow, spawn1, MinionColorIdenityfier);
			}
			counter++;
		} 
		else if (counter >= 5 && counter <= 10) 
		{
			if (MinionColorIdenityfier == 'C') {

				InstantiateS (minionCyanS, spawn1, MinionColorIdenityfier);
			} else if (MinionColorIdenityfier == 'M') {

				InstantiateS (minionMagentaS, spawn1, MinionColorIdenityfier);
			} else if (MinionColorIdenityfier == 'Y') {

				InstantiateS (minionYellowS, spawn1, MinionColorIdenityfier);
			}
			counter++;
		}

		else if (counter < 5) 
		{

			Debug.Log ("vaina");

			if (MinionColorIdenityfier == 'C') {

				InstantiateRandom (minionCyanRandom, spawn1, MinionColorIdenityfier);
			} 
			else if (MinionColorIdenityfier == 'M') {

				InstantiateRandom (minionMagentaRandom, spawn1, MinionColorIdenityfier);
			} 
			else if (MinionColorIdenityfier == 'Y') {

				InstantiateRandom (minionYellowRandom, spawn1, MinionColorIdenityfier);
			}
			counter++;
		}
	}

	void InstantiateForward(GameObject minion, GameObject spawn1, char MinionColorIdentyfier){


		MinionMovement minionScript = minion.GetComponent<MinionMovement> ();
		HexInfo spawn1Hex = spawn1.GetComponentInChildren<HexInfo> ();

		minionScript.ActualHex = spawn1Hex;
		minionScript.ColorIdentifier = MinionColorIdentyfier;

		Instantiate (minion, spawn1.transform.position, minionCyan.transform.rotation);

	}

	void InstantiateS(GameObject minion, GameObject spawn1, char MinionColorIdentyfier){


		MinionMovementS minionScript = minion.GetComponent<MinionMovementS> ();
		HexInfo spawn1Hex = spawn1.GetComponentInChildren<HexInfo> ();

		minionScript.ActualHex = spawn1Hex;
		minionScript.ColorIdentifier = MinionColorIdentyfier;

		Instantiate (minion, spawn1.transform.position, minionCyan.transform.rotation);

	}

	void InstantiateRandom(GameObject minion, GameObject spawn1, char MinionColorIdentyfier){


		MinionMovementRandom minionScript = minion.GetComponent<MinionMovementRandom> ();
		HexInfo spawn1Hex = spawn1.GetComponentInChildren<HexInfo> ();

		minionScript.ActualHex = spawn1Hex;
		minionScript.ColorIdentifier = MinionColorIdentyfier;

		Instantiate (minion, spawn1.transform.position, minionCyan.transform.rotation);

	}


}
