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

	private int firstSpawnPoint = 1;
	private int lastSpawnPoint = 7;

	//RESPAWN TIME
	public float invoke = 3f;

	//PROGRESIVE SPAWN TIME (-= 0.1 seconds EACH MINION SPAWNED)
	private float resetInvoke = 3f;

	void Update(){

		if (invoke * Time.deltaTime <= 0) {
			invoke = resetInvoke;
			Spawn ();
		}
		invoke -= Time.deltaTime;
	}

	char RandomColor()
	{
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

	int RandomInt(int from, int to)
	{
		return Random.Range (from, to);
	}
		
	void Spawn(){

		spawn1 = GameObject.Find ("Hex_0_" + RandomInt(firstSpawnPoint, lastSpawnPoint));
		char MinionColorIdenityfier = RandomColor ();

		int rand = RandomInt (0, 10);

		if(rand < 5)
			SpawnRecte (MinionColorIdenityfier);
		else if(rand >= 5 && rand <= 8)
			SpawnS (MinionColorIdenityfier);
		else
			SpawnRandom (MinionColorIdenityfier);

		if(resetInvoke > 1.5)
			resetInvoke -= 0.1f;
	}
	void SpawnRecte(char MinionColorIdenityfier)
	{
			if (MinionColorIdenityfier == 'C') {

				InstantiateForward (minionCyan, spawn1, MinionColorIdenityfier);
			} else if (MinionColorIdenityfier == 'M') {

				InstantiateForward (minionMagenta, spawn1, MinionColorIdenityfier);
			} else if (MinionColorIdenityfier == 'Y') {

				InstantiateForward (minionYellow, spawn1, MinionColorIdenityfier);
			}
	}
	void SpawnS(char MinionColorIdenityfier)
	{
			if (MinionColorIdenityfier == 'C') {

				InstantiateS (minionCyanS, spawn1, MinionColorIdenityfier);
			} else if (MinionColorIdenityfier == 'M') {

				InstantiateS (minionMagentaS, spawn1, MinionColorIdenityfier);
			} else if (MinionColorIdenityfier == 'Y') {

				InstantiateS (minionYellowS, spawn1, MinionColorIdenityfier);
			}
	}
	void SpawnRandom(char MinionColorIdenityfier)
	{
			if (MinionColorIdenityfier == 'C') {

				InstantiateRandom (minionCyanRandom, spawn1, MinionColorIdenityfier);
			} 
			else if (MinionColorIdenityfier == 'M') {

				InstantiateRandom (minionMagentaRandom, spawn1, MinionColorIdenityfier);
			} 
			else if (MinionColorIdenityfier == 'Y') {

				InstantiateRandom (minionYellowRandom, spawn1, MinionColorIdenityfier);
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
