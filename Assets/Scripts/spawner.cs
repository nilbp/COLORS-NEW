using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {

	public static GameObject HexSpawn1;
	public static GameObject HexSpawn2;
	public static GameObject HexSpawn3;

	public GameObject[] minionspawn;

	public Transform nucli;

	HexInfo SpawnHex;
	HexInfo SpawnHex2;
	HexInfo SpawnHex3;

	int time2=0;
	int time=0;

	public float spawnTime = 5f;     

	public float speed = 1;
	private float startTime;
	private float dist;

	float distCover;
	float fracjourn;

	void Start()
	{
		//yield return new WaitForSeconds (1f);
		//InvokeRepeating ("clicki", spawnTime, spawnTime);

		startTime = Time.time;

		SpawnHex = HexSpawn1.GetComponentInChildren<HexInfo> ();
		SpawnHex2 = HexSpawn2.GetComponentInChildren<HexInfo> ();
		//SpawnHex3 = HexSpawn3.GetComponentInChildren<HexInfo> ();



		dist = Vector3.Distance (minionspawn[0].transform.position, SpawnHex.map.hexLines[SpawnHex.y].columns[SpawnHex.x+1].transform.position);

		distCover = (Time.time - startTime) * speed;
		fracjourn = distCover / dist;

	}


	/*void spawnminion (){

		minionspawn [0] = Instantiate (minionprefab [0], HexSpawn1.transform.position, Quaternion.Euler (0, 0, 0)) as GameObject;
		minionspawn [1] = Instantiate (minionprefab [1], HexSpawn2.transform.position, Quaternion.Euler (0, 0, 0)) as GameObject;
		//minionspawn [2] = Instantiate (minionprefab [2], spawnLocation [2].transform.position, Quaternion.Euler (0, 0, 0)) as GameObject;
	}*/

/*	void clicki(){



		MinionsSpawejats[a] = Instantiate (minionspawn[a], SpawnHex2.transform.position, SpawnHex2.transform.rotation ) as GameObject;
		IAMoveS (SpawnHex2 ,MinionsSpawejats, a);
		a++;
		}*/


	void Update(){

		//clicki ();
		time2++;
		time ++;


		IAMoveForward (SpawnHex);
		IAMoveS (SpawnHex2);

		//float step = speed * Time.deltaTime;
		//transform.position = Vector3.MoveTowards (minionspawn[0].transform.position, nucli.position, step);

		//Vector3 posicion = nucli.transform.position ;
		//minionspawn[0].transform.position = new Vector3 (transform.position,nucli.position, step);
		//minionspawn[0].transform.position = new Vector3(nucli.position.x, 0, nucli.position.z);

	}



	void IAMoveForward(HexInfo SpawnHex){


	if (SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].HexColor == Color.white) {

			if (time > 50 && time < 100) {
				minionspawn [0].transform.position = Vector3.Lerp (SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].transform.position, SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x + 1].transform.position, fracjourn); 
				//yield return new WaitForSeconds (2f);
				SpawnHex.x++;
				time = 0;
			}
		} 

	else if (SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].HexColor == Color.cyan) {
			
			Destroy (minionspawn [0]);
			SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].GetComponent<MeshRenderer> ().material.mainTexture = null;
			SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].transform.localScale = new Vector3(1, 1, 1);

		}

	}

	void IAMoveS(HexInfo SpawnHex){



		if (time2 > 100 && time2< 150 || time%100==0) {
			if (SpawnHex.y%2==0) {


				minionspawn[1].transform.position = Vector3.Lerp (SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].transform.position, SpawnHex.map.hexLines [SpawnHex.y+1].columns [SpawnHex.x].transform.position, fracjourn); 
				//yield return new WaitForSeconds (2f);
				SpawnHex.y++;
				time2 = 50;
			}
		
			else if (SpawnHex.y%2==1) {


				minionspawn[1].transform.position = Vector3.Lerp (SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].transform.position, SpawnHex.map.hexLines [SpawnHex.y-1].columns [SpawnHex.x+1].transform.position, fracjourn); 
				//yield return new WaitForSeconds (2f);
				SpawnHex.y--;
				SpawnHex.x++;
				time2 = 50;
			}
		}

	}


}