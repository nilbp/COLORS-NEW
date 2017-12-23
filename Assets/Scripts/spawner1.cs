using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spawner1 : MonoBehaviour {


	public static GameObject HexSpawn2;


	public GameObject[] minionspawn2;

	public Texture2D DefaultTexture;

	int MinionSpawnSize = 10;
	int MinionStorage = 0;

	HexInfo SpawnHex2;

	int time4=0;
	int time3=0;

	   
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

		SpawnHex2 = HexSpawn2.GetComponentInChildren<HexInfo> ();



		dist = Vector3.Distance (minionspawn2[0].transform.position, SpawnHex2.map.hexLines[SpawnHex2.y].columns[SpawnHex2.x+1].transform.position);

		distCover = (Time.time - startTime) * speed;
		fracjourn = distCover / dist;

	}
		
	void Update(){

		SpawnManager ();
	}
	
	void SpawnManager(){


		time4++;
		time3++;


		if (minionspawn2 [MinionSpawnCounter] != null) {

			if (MinionSpawnCounter == 3 || MinionSpawnCounter == 5) {
				IAMoveS2 (SpawnHex2, minionspawn2, MinionSpawnCounter);
			} 
			else {
				IAMoveForward2 (SpawnHex2, minionspawn2, MinionSpawnCounter);
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
				MinionChanged.name = color + "Minion";

				Minion [a] = MinionChanged;
				break;

			}
		}
	}
		
	void IAMoveS2(HexInfo SpawnHex, GameObject[] Minion, int i){
		int LocalSpawnCounter= MinionSpawnCounter;


		if (time3 > 50) {


			//GameObject MinionInstantiat = InstantiateInSpawn (SpawnHex, 0, 2, Minion, i);

			if (SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].Nucli == true) {

				SceneManager.LoadScene ("MainMenu");

			}


			else if (SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].HexColor == 'W') {


				Minion[i].transform.position = Vector3.Lerp (SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].transform.position, SpawnHex.map.hexLines [SpawnHex.y-1].columns [SpawnHex.x].transform.position, fracjourn); 
				SpawnHex.y--;


			} 	

			else {

				ColisionColorDetection (SpawnHex.map.hexLines[SpawnHex.y].columns [SpawnHex.x], Minion, i);

				if (LocalSpawnCounter != MinionSpawnCounter) {
					ResetSpawn (SpawnHex, 7, 14);
				}
			}
			time3 = 0;
		}
	}

	void IAMoveForward2(HexInfo SpawnHex, GameObject[] Minion, int i){
		int LocalSpawnCounter= MinionSpawnCounter;

		if (time4 > 60) {

			if (SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].Nucli == true) {

				SceneManager.LoadScene ("MainMenu");

			}
			else if (SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].HexColor == 'W') {


					Minion [i].transform.position = Vector3.Lerp (SpawnHex.map.hexLines [SpawnHex.y].columns [SpawnHex.x].transform.position, SpawnHex.map.hexLines [SpawnHex.y - 2].columns [SpawnHex.x].transform.position, fracjourn); 
					SpawnHex.y-= 2;


			} 

			else {
				ColisionColorDetection (SpawnHex.map.hexLines[SpawnHex.y].columns [SpawnHex.x], Minion, i);	

				if (LocalSpawnCounter != MinionSpawnCounter) {
					ResetSpawn (SpawnHex, 7, 14);

				}

			}
			time4 = 0;
		}

	}


	void ResetHexagonColorValues(HexInfo SpawnHex, MeshRenderer HexColor){

		SpawnHex.HexColor = 'W';
		SpawnHex.transform.localScale = new Vector3 (1, 1, 1);
		HexColor.material.mainTexture = null;
	}

	void ColisionColorDetection(HexInfo SpawnHex, GameObject[] Minion, int i){


		MeshRenderer MinionColor = Minion[i].GetComponent<MeshRenderer> ();
		MeshRenderer HexColor = SpawnHex.GetComponent<MeshRenderer> ();

		if (Minion[i].name == "cyanMinion") {
			
			if (SpawnHex.HexColor == 'C') {
				
				SpawnHex.ColorDensity++;
				densityHigh (SpawnHex);

			}
			else if (SpawnHex.HexColor == 'M') {


				ResetHexagonColorValues (SpawnHex, HexColor);

				SaveHexInArray(Minion, i, MinionSpawnSize, Color.blue, "blue");



			} else if (SpawnHex.HexColor == 'Y') {

				ResetHexagonColorValues (SpawnHex, HexColor);
				SaveHexInArray(Minion, i, MinionSpawnSize, Color.green, "green");

			} else {

				ResetHexagonColorValues (SpawnHex, HexColor);

			}
				
			Destroy (Minion[i]);
			MinionSpawnCounter++;
		}
		
		else if(Minion[i].name == "magentaMinion"){

			if (SpawnHex.HexColor == 'C') {

				ResetHexagonColorValues (SpawnHex, HexColor);
				SaveHexInArray(Minion, i, MinionSpawnSize, Color.blue, "blue");


			} else if (SpawnHex.HexColor == 'M') {

				SpawnHex.ColorDensity++;
				densityHigh (SpawnHex);


			} else if (SpawnHex.HexColor == 'Y') {

				ResetHexagonColorValues (SpawnHex, HexColor);
				SaveHexInArray(Minion, i, MinionSpawnSize, Color.red, "red");

			} else {

				ResetHexagonColorValues (SpawnHex, HexColor);

			}

			Destroy (Minion[i]);
			MinionSpawnCounter++;
		}

		else if (Minion[i].name == "yellowMinion"){

			if (SpawnHex.HexColor == 'C') {

				ResetHexagonColorValues (SpawnHex, HexColor);
				SaveHexInArray(Minion, i, MinionSpawnSize, Color.green, "green");

			} else if (SpawnHex.HexColor == 'M') {

				ResetHexagonColorValues (SpawnHex, HexColor);
				SaveHexInArray(Minion, i, MinionSpawnSize, Color.red, "red");

			} else if (SpawnHex.HexColor == 'Y') {

				SpawnHex.ColorDensity++;
				densityHigh (SpawnHex);

			} else {

				ResetHexagonColorValues (SpawnHex, HexColor);

			}

			Destroy (Minion[i]);
			MinionSpawnCounter++;

		}

		if (Minion [i].name == "blueMinion") {

			if (SpawnHex.HexColor == 'C') {

				ResetHexagonColorValues (SpawnHex, HexColor);
					
			} else if (SpawnHex.HexColor == 'M') {


				ResetHexagonColorValues (SpawnHex, HexColor);


			} else if (SpawnHex.HexColor == 'Y') {

				ResetHexagonColorValues (SpawnHex, HexColor);

			} else if (SpawnHex.HexColor == 'B') {

				SpawnHex.ColorDensity++;
				MinionSpawnCounter++;
				densityHigh (SpawnHex);
				Destroy (Minion[i]);


			} else if (SpawnHex.HexColor == 'R') {

				ResetHexagonColorValues (SpawnHex, HexColor);
				MinionSpawnCounter++;
				Destroy (Minion [i]);

			} else if (SpawnHex.HexColor == 'G') {

				ResetHexagonColorValues (SpawnHex, HexColor);
				MinionSpawnCounter++;
				Destroy (Minion [i]);

			}
		} else if (Minion [i].name == "redMinion") {

			if (SpawnHex.HexColor == 'C') {

				ResetHexagonColorValues (SpawnHex, HexColor);

			} else if (SpawnHex.HexColor == 'M') {

				ResetHexagonColorValues (SpawnHex, HexColor);

			} else if (SpawnHex.HexColor == 'Y') {

				ResetHexagonColorValues (SpawnHex, HexColor);

			} else if (SpawnHex.HexColor == 'B') {

				ResetHexagonColorValues (SpawnHex, HexColor);
				MinionSpawnCounter++;
				Destroy (Minion [i]);

			} else if (SpawnHex.HexColor == 'R') {


				SpawnHex.ColorDensity++;
				MinionSpawnCounter++;
				densityHigh (SpawnHex);
				Destroy (Minion [i]);

			} else if (SpawnHex.HexColor == 'G') {

				ResetHexagonColorValues (SpawnHex, HexColor);
				MinionSpawnCounter++;
				Destroy (Minion [i]);
			}
		}

		else if (Minion[i].name == "greenMinion"){

			if (SpawnHex.HexColor == 'C') {

				ResetHexagonColorValues (SpawnHex, HexColor);

			} else if (SpawnHex.HexColor == 'M') {

				ResetHexagonColorValues (SpawnHex, HexColor);

			} else if (SpawnHex.HexColor == 'Y') {

				ResetHexagonColorValues (SpawnHex, HexColor);

		 	} else if (SpawnHex.HexColor == 'B'){

			ResetHexagonColorValues (SpawnHex, HexColor);
			MinionSpawnCounter++;
			Destroy (Minion[i]);

			} else if (SpawnHex.HexColor == 'R'){


				SpawnHex.ColorDensity++;
				MinionSpawnCounter++;
				densityHigh (SpawnHex);
				Destroy (Minion[i]);

			} else if (SpawnHex.HexColor == 'G'){


				ResetHexagonColorValues (SpawnHex, HexColor);
				MinionSpawnCounter++;
				Destroy (Minion[i]);
			}
		}

	}

}