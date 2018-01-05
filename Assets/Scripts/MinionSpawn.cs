using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawn : MonoBehaviour {

	GameObject spawn1;
	HexInfo Nucli;

	public GameObject minionPref;

	void Start(){
	
		StartCoroutine (Spawn (0,9));
	
	}

	IEnumerator Spawn(int x, int y){
	
		yield return new WaitForSeconds (1);

		spawn1 = GameObject.Find ("Hex_" + x + "_" + y);
		Nucli = GameObject.Find ("Hex_7_7").GetComponentInChildren<HexInfo>();

		HexInfo spawn1Hex = spawn1.GetComponentInChildren<HexInfo> ();
		MinionMovement minionScript = minionPref.GetComponent<MinionMovement> ();

		minionScript.ActualHex = spawn1Hex;
		minionScript.Nucli = Nucli;

		Instantiate (minionPref, spawn1.transform.position, minionPref.transform.rotation);






	

	}
}
