using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class totems : MonoBehaviour {

	bool totem;

	public static GameObject HexSpawn1;


	HexInfo SpawnHex;


	void Start () {
		SpawnHex = HexSpawn1.GetComponentInChildren<HexInfo> ();
	}
	

	void Update () {
		
	}
}
