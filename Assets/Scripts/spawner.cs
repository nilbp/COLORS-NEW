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
	   
	public float speed = 1;
	private float startTime;
	private float dist;

	float distCover;
	float fracjourn;

	int MinionSpawnCounter=0;

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
		
	void Update(){

		SpawnManager ();

	}
	
	void SpawnManager(){


		time2++;
		time ++;

		switch(MinionSpawnCounter){
			case 0:
				IAMoveForward (SpawnHex, minionspawn, 0);
				break;
			case 1:
				IAMoveForward (SpawnHex, minionspawn, 1);
				break;
			case 2:
				IAMoveForward (SpawnHex, minionspawn, 2);
				break;
			case 3:
				IAMoveForward (SpawnHex, minionspawn, 3);
				break;
		}

	}

	void ResetSpawn(HexInfo SpawnHex, int XSpawnPos, int YSpawnPos){
	
		SpawnHex.x = XSpawnPos;
		SpawnHex.y = YSpawnPos;
	}

	//Instantia un Minion en la Position X Y passades com a parametres.
	GameObject InstantiateInSpawn(HexInfo SpawnHex, int XSpawnPos, int YSpawnPos, GameObject[] Minion, int i){

		GameObject MinionInstantiat= null;

		if (SpawnHex.x == XSpawnPos && SpawnHex.y == YSpawnPos) {
		
			MinionInstantiat = Instantiate(Minion[i], SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].transform.position, Quaternion.identity) as GameObject;

		}
		Debug.Log (SpawnHex.x + " " + SpawnHex.y);

		return MinionInstantiat;

	}

	void IAMoveForward(HexInfo SpawnHex, GameObject[] Minion, int i){


		Debug.Log (SpawnHex.x + " " + SpawnHex.y);
		if (time > 50) {

			GameObject MinionInstantiat = InstantiateInSpawn (SpawnHex, 0, 2, Minion, i);

			if (SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].HexColor == Color.white) {

				MinionInstantiat.transform.position = Vector3.Lerp (SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].transform.position, SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x + 1].transform.position, fracjourn); 
				SpawnHex.x++;

			
			} else {

				ColisionColorDetection (SpawnHex.map.hexLines[SpawnHex.y].columns [SpawnHex.x], MinionInstantiat, i);
			
				ResetSpawn (SpawnHex, 0, 2);
				MinionSpawnCounter++;
				Destroy (MinionInstantiat);
			}
			time = 0;
		}
	}

	void ColisionColorDetection(HexInfo SpawnHex, GameObject[] Minion, int i){
	
		MeshRenderer MinionColor = Minion[i].GetComponent<MeshRenderer> ();
		MeshRenderer HexColor = SpawnHex.GetComponent<MeshRenderer> ();

		if (Minion[i].name == "CyanMinion") {

	
			if (SpawnHex.HexColor == Color.cyan) {
				
				SpawnHex.ColorDensity++;


			} else if (SpawnHex.HexColor == Color.magenta) {

				SpawnHex.HexColor = Color.white;
				SpawnHex.transform.localScale = new Vector3 (1, 1, 1);
				HexColor.material.mainTexture = null;


			} else if (SpawnHex.HexColor == Color.yellow) {

				SpawnHex.HexColor = Color.white;
				SpawnHex.transform.localScale = new Vector3 (1, 1, 1);
				HexColor.material.mainTexture = null;


			} else {

				SpawnHex.HexColor = Color.white;
				SpawnHex.transform.localScale = new Vector3 (1, 1, 1);
				HexColor.material.mainTexture = null;

			}
		}
		
		else if(Minion[i].name == "MagentaMinion"){

			if (SpawnHex.HexColor == Color.cyan) {

				SpawnHex.HexColor = Color.white;
				SpawnHex.transform.localScale = new Vector3 (1, 1, 1);
				HexColor.material.mainTexture = null;


			} else if (SpawnHex.HexColor == Color.magenta) {

				SpawnHex.ColorDensity++;


			} else if (SpawnHex.HexColor == Color.yellow) {

				SpawnHex.HexColor = Color.white;
				SpawnHex.transform.localScale = new Vector3 (1, 1, 1);
				HexColor.material.mainTexture = null;


			} else {

				SpawnHex.HexColor = Color.white;
				SpawnHex.transform.localScale = new Vector3 (1, 1, 1);
				HexColor.material.mainTexture = null;

			}
		}

		else if (Minion[i].name == "YellowMinion"){

			if (SpawnHex.HexColor == Color.cyan) {

				SpawnHex.HexColor = Color.white;
				SpawnHex.transform.localScale = new Vector3 (1, 1, 1);
				HexColor.material.mainTexture = null;


			} else if (SpawnHex.HexColor == Color.magenta) {

				SpawnHex.HexColor = Color.white;
				SpawnHex.transform.localScale = new Vector3 (1, 1, 1);
				HexColor.material.mainTexture = null;


			} else if (SpawnHex.HexColor == Color.yellow) {

				SpawnHex.ColorDensity++;


			} else {

				SpawnHex.HexColor = Color.white;
				SpawnHex.transform.localScale = new Vector3 (1, 1, 1);
				HexColor.material.mainTexture = null;

			}
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