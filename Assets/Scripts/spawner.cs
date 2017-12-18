using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spawner : MonoBehaviour {

	public static GameObject HexSpawn1;
	public static GameObject HexSpawn2;
	public static GameObject HexSpawn3;

	public GameObject[] minionspawn;

	public Texture2D DefaultTexture;

	int MinionSpawnSize = 10;
	int MinionStorage = 0;


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

		if (minionspawn [MinionSpawnCounter] != null) {

			if (MinionSpawnCounter == 3 || MinionSpawnCounter == 5) {
				IAMoveS (SpawnHex, minionspawn, MinionSpawnCounter);
			} else {
				IAMoveForward (SpawnHex, minionspawn, MinionSpawnCounter);
			}
		} 
		else {
			print("LEVEL PASSED!!!");
		}
	}

	void ResetSpawn(HexInfo SpawnHex, int XSpawnPos, int YSpawnPos){
	
		SpawnHex.x = XSpawnPos;
		SpawnHex.y = YSpawnPos;
	}

	//Instantia un Minion en la Position X Y passades com a parametres.
	/*GameObject InstantiateInSpawn(HexInfo SpawnHex, int XSpawnPos, int YSpawnPos, GameObject[] Minion, int i){

		GameObject MinionInstantiat= null;

		if (SpawnHex.x == XSpawnPos && SpawnHex.y == YSpawnPos) {
		
			MinionInstantiat = Instantiate(Minion[i], SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].transform.position, Quaternion.identity) as GameObject;

		}


		return MinionInstantiat;

	}*/
	void densityHigh(HexInfo ActualHex){

		ActualHex.transform.localScale = new Vector3(1, 1, 3 * ActualHex.ColorDensity);
	}

	void SaveHexInArray(GameObject[] Minion, int i, int Size, Color color, string Color){


		for (int a = i; a < Size; a++) {
			if (Minion [a] == null) {

				GameObject MinionChanged = (GameObject)Instantiate (Minion [i], new Vector3 (-4, 0, 2), Quaternion.identity);
				MinionChanged.GetComponent<MeshRenderer> ().material.color = color;
				MinionChanged.name = Color + "Minion";

				Minion [a] = MinionChanged;
				break;

			}
		}
	}


	void IAMoveForward(HexInfo SpawnHex, GameObject[] Minion, int i){
		int LocalSpawnCounter= MinionSpawnCounter;

		if (time > 50) {

			if (SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].Nucli == true) {

				SceneManager.LoadScene ("MainMenu");

			}
			else if (SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].HexColor == Color.white) {

				Minion[i].transform.position = Vector3.Lerp (SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].transform.position, SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x + 1].transform.position, fracjourn); 
				SpawnHex.x++;

			
			} 	

			else {

				ColisionColorDetection (SpawnHex.map.hexLines[SpawnHex.y].columns [SpawnHex.x], Minion, i);

				if (LocalSpawnCounter != MinionSpawnCounter) {
					ResetSpawn (SpawnHex, 0, 2);

				}
			
			}
			time = 0;
		}
	}

	void IAMoveS(HexInfo SpawnHex, GameObject[] Minion, int i){
		int LocalSpawnCounter= MinionSpawnCounter;

		if (time2 > 25) {

			if (SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].Nucli == true) {

				SceneManager.LoadScene ("MainMenu");

			}
			else if (SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].HexColor == Color.white) {

				if (SpawnHex.y % 2 == 0) {

					Minion [i].transform.position = Vector3.Lerp (SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].transform.position, SpawnHex.map.hexLines [SpawnHex.y + 1].columns [SpawnHex.x].transform.position, fracjourn); 
					SpawnHex.y++;

				} else if (SpawnHex.y % 2 == 1) {
					
					Minion [i].transform.position = Vector3.Lerp (SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].transform.position, SpawnHex.map.hexLines [SpawnHex.y - 1].columns [SpawnHex.x + 1].transform.position, fracjourn); 
					SpawnHex.y--;
					SpawnHex.x++;

				}
			} 

			else {
				ColisionColorDetection (SpawnHex.map.hexLines[SpawnHex.y].columns [SpawnHex.x], Minion, i);	

				if (LocalSpawnCounter != MinionSpawnCounter) {
					ResetSpawn (SpawnHex, 0, 2);

				}

			}
			time2 = 0;
		}

	}


	void ResetHexagonColorValues(HexInfo SpawnHex, MeshRenderer HexColor){

		SpawnHex.HexColor = Color.white;
		SpawnHex.transform.localScale = new Vector3 (1, 1, 1);
		HexColor.material.mainTexture = DefaultTexture;
	}

	void ColisionColorDetection(HexInfo SpawnHex, GameObject[] Minion, int i){


		MeshRenderer MinionColor = Minion[i].GetComponent<MeshRenderer> ();
		MeshRenderer HexColor = SpawnHex.GetComponent<MeshRenderer> ();

		if (Minion[i].name == "cyanMinion") {
			
			if (SpawnHex.HexColor == Color.cyan) {
				
				SpawnHex.ColorDensity++;
				densityHigh (SpawnHex);

			} else if (SpawnHex.HexColor == Color.magenta) {


				ResetHexagonColorValues (SpawnHex, HexColor);

				SaveHexInArray(Minion, i, MinionSpawnSize, Color.blue, "blue");



			} else if (SpawnHex.HexColor == Color.yellow) {

				ResetHexagonColorValues (SpawnHex, HexColor);
				SaveHexInArray(Minion, i, MinionSpawnSize, Color.green, "green");

			} else {

				ResetHexagonColorValues (SpawnHex, HexColor);

			}
				
			Destroy (Minion[i]);
			MinionSpawnCounter++;
		}
		
		else if(Minion[i].name == "magentaMinion"){

			if (SpawnHex.HexColor == Color.cyan) {

				ResetHexagonColorValues (SpawnHex, HexColor);
				SaveHexInArray(Minion, i, MinionSpawnSize, Color.blue, "blue");


			} else if (SpawnHex.HexColor == Color.magenta) {

				SpawnHex.ColorDensity++;
				densityHigh (SpawnHex);

			} else if (SpawnHex.HexColor == Color.yellow) {

				ResetHexagonColorValues (SpawnHex, HexColor);
				SaveHexInArray(Minion, i, MinionSpawnSize, Color.red, "red");

			} else {

				ResetHexagonColorValues (SpawnHex, HexColor);

			}

			Destroy (Minion[i]);
			MinionSpawnCounter++;


		}

		else if (Minion[i].name == "yellowMinion"){

			if (SpawnHex.HexColor == Color.cyan) {

				ResetHexagonColorValues (SpawnHex, HexColor);
				SaveHexInArray(Minion, i, MinionSpawnSize, Color.green, "green");

			} else if (SpawnHex.HexColor == Color.magenta) {

				ResetHexagonColorValues (SpawnHex, HexColor);
				SaveHexInArray(Minion, i, MinionSpawnSize, Color.red, "red");

			} else if (SpawnHex.HexColor == Color.yellow) {

				SpawnHex.ColorDensity++;
				densityHigh (SpawnHex);

			} else {

				ResetHexagonColorValues (SpawnHex, HexColor);

			}

			Destroy (Minion[i]);
			MinionSpawnCounter++;

		}

		if (Minion[i].name == "blueMinion") {

			if (SpawnHex.HexColor == Color.cyan) {

				ResetHexagonColorValues (SpawnHex, HexColor);
					
			} else if (SpawnHex.HexColor == Color.magenta) {


				ResetHexagonColorValues (SpawnHex, HexColor);


			} else if (SpawnHex.HexColor == Color.yellow) {

				ResetHexagonColorValues (SpawnHex, HexColor);

			} else if (SpawnHex.HexColor == Color.blue){

				SpawnHex.ColorDensity++;
				MinionSpawnCounter++;
				densityHigh (SpawnHex);
				Destroy (Minion[i]);

			} else if (SpawnHex.HexColor == Color.red){

				ResetHexagonColorValues (SpawnHex, HexColor);
				MinionSpawnCounter++;
				Destroy (Minion[i]);

			} else if (SpawnHex.HexColor == Color.green){

				ResetHexagonColorValues (SpawnHex, HexColor);
				MinionSpawnCounter++;
				Destroy (Minion[i]);

			}
		}

		else if(Minion[i].name == "redMinion"){

			if (SpawnHex.HexColor == Color.cyan) {

				ResetHexagonColorValues (SpawnHex, HexColor);

			} else if (SpawnHex.HexColor == Color.magenta) {

				ResetHexagonColorValues (SpawnHex, HexColor);

			} else if (SpawnHex.HexColor == Color.yellow) {

				ResetHexagonColorValues (SpawnHex, HexColor);

		} 	} else if (SpawnHex.HexColor == Color.blue){

			ResetHexagonColorValues (SpawnHex, HexColor);
			MinionSpawnCounter++;
			Destroy (Minion[i]);

		} else if (SpawnHex.HexColor == Color.red){


			SpawnHex.ColorDensity++;
			MinionSpawnCounter++;
			densityHigh (SpawnHex);
			Destroy (Minion[i]);

		} else if (SpawnHex.HexColor == Color.green){

			ResetHexagonColorValues (SpawnHex, HexColor);
			MinionSpawnCounter++;
			Destroy (Minion[i]);
		}

		else if (Minion[i].name == "greenMinion"){

			if (SpawnHex.HexColor == Color.cyan) {

				ResetHexagonColorValues (SpawnHex, HexColor);

			} else if (SpawnHex.HexColor == Color.magenta) {

				ResetHexagonColorValues (SpawnHex, HexColor);

			} else if (SpawnHex.HexColor == Color.yellow) {

				ResetHexagonColorValues (SpawnHex, HexColor);

		 	} else if (SpawnHex.HexColor == Color.blue){

			ResetHexagonColorValues (SpawnHex, HexColor);
			MinionSpawnCounter++;
			Destroy (Minion[i]);

			} else if (SpawnHex.HexColor == Color.red){


				SpawnHex.ColorDensity++;
				MinionSpawnCounter++;
				densityHigh (SpawnHex);
				Destroy (Minion[i]);

			} else if (SpawnHex.HexColor == Color.green){


				ResetHexagonColorValues (SpawnHex, HexColor);
				MinionSpawnCounter++;
				Destroy (Minion[i]);
			}
		}

	}




}