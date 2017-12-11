using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {

	public static GameObject HexSpawn1;

	public GameObject[] minionprefab;
	public GameObject[] minionspawn;
	public Transform nucli;

	HexInfo SpawnHex;

	public float speed = 1;
	private float startTime;
	private float dist;

	void Start()
	{
		//yield return new WaitForSeconds (1f);
		startTime = Time.time;
		spawnminion ();

		SpawnHex = HexSpawn1.GetComponentInChildren<HexInfo> ();


	}


	void spawnminion (){

		minionspawn [0] = Instantiate (minionprefab [0], HexSpawn1.transform.position, Quaternion.Euler (0, 0, 0)) as GameObject;
		//minionspawn [1] = Instantiate (minionprefab [1], spawnLocation [1].transform.position, Quaternion.Euler (0, 0, 0)) as GameObject;
		//minionspawn [2] = Instantiate (minionprefab [2], spawnLocation [2].transform.position, Quaternion.Euler (0, 0, 0)) as GameObject;
	}

	void Update(){

		IAMoveForward (SpawnHex);
		//float step = speed * Time.deltaTime;
		//transform.position = Vector3.MoveTowards (minionspawn[0].transform.position, nucli.position, step);

		//Vector3 posicion = nucli.transform.position ;
		//minionspawn[0].transform.position = new Vector3 (transform.position,nucli.position, step);
		//minionspawn[0].transform.position = new Vector3(nucli.position.x, 0, nucli.position.z);

	}



	void IAMoveForward(HexInfo SpawnHex){



		dist = Vector3.Distance (minionspawn[0].transform.position, SpawnHex.map.hexLines[SpawnHex.y].columns[SpawnHex.x+1].transform.position);

		float distCover = (Time.time - startTime) * speed;
		float fracjourn = distCover / dist;

		for(int i = 0 ; i<10;i++){
			
			minionspawn [0].transform.position = Vector3.Lerp (minionspawn[0].transform.position, SpawnHex.map.hexLines[SpawnHex.y].columns[SpawnHex.x+1].transform.position, fracjourn);
			SpawnHex = SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x + 1];
		}
	}

}