 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour {

	int xHexPos;
	int yHexPos;

	public int MaxPigment = 6;
	public float CurrentPigment = 6;
	public float PigmentRatio;
	public Image PigmentBar;

	public Texture2D DefaultText;

	public Texture2D CyanTex;
	public Texture2D MagentaTex;
	public Texture2D YellowTex;

	public Texture2D RedTex;
	public Texture2D BlueTex;
	public Texture2D GreenTex;

	private HexInfo Nucli;
	public static char ColorInHand;

	public GameObject CyanTotem;
	public GameObject MagentaTotem;
	public GameObject YellowTotem;

	public GameObject CyanTubPinturaPrefab;
	public GameObject MagentaTubPinturaPrefab;
	public GameObject YellowTubPinturaPrefab;

	public Vector3 SpraiPositionOffset;
	public Vector3 TubOfset;

	/*public struct colorCombo{

		public int i;
		public bool IsNeighbour;
	}

	public struct combo{

		public colorCombo Color1;
		public colorCombo Color2;
	}*/

	enum NeighbourPosition
	{
		Left,
		UpLeft,
		UpRight,
		Right,
		DownRight,
		DownLeft,
		NumPositions,
	}
		

	void Start(){

		Nucli = GameObject.Find ("Hex_7_7").GetComponentInChildren<HexInfo> ();
		//RandomPrimaryColorSpawn ();

		SpraiPositionOffset = new Vector3 (0, 0.485f, 0);
	
	}


	void Update () {

		if (CurrentPigment < MaxPigment) {
			CurrentPigment+=0.1f;
		}
		//densityHigh ();
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hitInfo;

		if(Physics.Raycast(ray, out hitInfo)){

			GameObject ourHitObject = hitInfo.collider.transform.gameObject;



			if (Input.GetMouseButtonDown (0)) {

				HexInfo HexInfoObject = ourHitObject.GetComponentInChildren<HexInfo> ();

				if (MoneyManager.Pigment > 0) {
					if (HexInfoObject.Clickable == true) {
					
						MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer> ();

						//bola.transform.position = new Vector3 (ourHitObject.transform.position.x, 0.3f ,ourHitObject.transform.position.z);

						if (ColorInHand == 'C') {

							ColorsChangeCyan (mr, HexInfoObject);
							MoneyManager.Pigment -= 5;
		

						} else if (ColorInHand == 'M') {
					
							ColorsChangeMagenta (mr, HexInfoObject);
							MoneyManager.Pigment -= 5;

						} else if (ColorInHand == 'Y') {
					
							ColorsChangeYellow (mr, HexInfoObject);
							MoneyManager.Pigment -= 5;
						}

						ComboTresEnRalla (HexInfoObject);
						CreateSprai (HexInfoObject);
						//RandomPrimaryColorSpawn ();
		
					}
				}
			}

		}
		UpdatePigmentBar ();


	}


	/* GENERADOR DE COLORS RANDOM EN EL NULCI
	void RandomPrimaryColorSpawn(){

		//Random rnd = new Random();
		int Rand = Random.Range(0, 3);


		switch (Rand) {

		case 0:
			ColorInHand = 'C';
			NucliMesh.material.mainTexture = CyanTex;
			break;
		case 1: 
			ColorInHand = 'M';
			NucliMesh.material.mainTexture = MagentaTex;
			break;
		case 2:
			ColorInHand = 'Y';
			NucliMesh.material.mainTexture = YellowTex;
			break;
		}
	
	}*/

	void UpdatePigmentBar(){
	
		PigmentRatio = CurrentPigment / MaxPigment;

		PigmentBar.transform.localScale = new Vector3 (1, PigmentRatio, 1);

	
	}

	void ResetHexagonValues(HexInfo ActualHex){

		ActualHex.GetComponentInChildren<MeshRenderer> ().material.mainTexture = null;
		ActualHex.HexColor = 'W';
		ActualHex.GetComponent<MeshRenderer> ().material.color = Color.white;
		ActualHex.transform.localScale = new Vector3 (1, 1, 1);
		ActualHex.ColorDensity = 0;

	}



	void CreateSprai(HexInfo ActualHex){

		for(int i = 0; i<5;i++){
			for(int j = i+1; j<6;j++){

				if (ActualHex.neigbours [i].ColorDensity == ActualHex.neigbours [j].ColorDensity && ActualHex.neigbours [j].ColorDensity == ActualHex.ColorDensity) {

					if (ActualHex.HexColor == 'C') {
						if (ActualHex.neigbours [i].HexColor == 'C') {
							if (ActualHex.neigbours [j].HexColor == 'C') {
								
								ResetHexagonValues (ActualHex.neigbours [i]);
								ResetHexagonValues (ActualHex.neigbours [j]);
								ResetHexagonValues (ActualHex);

								DefenseTotem totemScript = CyanTotem.GetComponent<DefenseTotem> ();
								totemScript.TotemColor = 'C';
								totemScript.actualHex = ActualHex;
								Instantiate (CyanTotem, ActualHex.transform.position + SpraiPositionOffset, CyanTotem.transform.rotation);
							}
						}
					}
				}


				if (ActualHex.neigbours [i].ColorDensity == ActualHex.neigbours [j].ColorDensity && ActualHex.neigbours [j].ColorDensity == ActualHex.ColorDensity) {

					if (ActualHex.HexColor == 'M') {

						if (ActualHex.neigbours [i].HexColor == 'M') {
							if (ActualHex.neigbours [j].HexColor == 'M') {

								ResetHexagonValues (ActualHex.neigbours [i]);
								ResetHexagonValues (ActualHex.neigbours [j]);
								ResetHexagonValues (ActualHex);
								Instantiate (MagentaTotem, ActualHex.transform.position + SpraiPositionOffset, CyanTotem.transform.rotation);
								MagentaTotem.GetComponent<DefenseTotem> ().TotemColor = 'M';
							}
						}
					}
				}

				if (ActualHex.neigbours [i].ColorDensity == ActualHex.neigbours [j].ColorDensity && ActualHex.neigbours [j].ColorDensity == ActualHex.ColorDensity) {

					if (ActualHex.HexColor == 'Y') {

						if (ActualHex.neigbours [i].HexColor == 'Y') {
							if (ActualHex.neigbours [j].HexColor == 'Y') {

								ResetHexagonValues (ActualHex.neigbours [i]);
								ResetHexagonValues (ActualHex.neigbours [j]);
								ResetHexagonValues (ActualHex);
								Instantiate (YellowTotem, ActualHex.transform.position + SpraiPositionOffset, CyanTotem.transform.rotation);
								YellowTotem.GetComponent<DefenseTotem> ().TotemColor = 'Y';
							}
						}
					}
				}
			}
		}
	}



	void ComboTresEnRalla(HexInfo LastHexPressed){


		for (int x = 0; x < Map.width; x++) {
			for (int y = 0; y < Map.height; y++) {
				

				GameObject HexGameObject = GameObject.Find ("Hex_" + x + "_" + y);
				HexInfo ActualHex = HexGameObject.GetComponentInChildren<HexInfo> ();

				if (ActualHex.HexColor == 'C') {

					if (ActualHex.neigbours [0].HexColor == 'C' && ActualHex.neigbours [3].HexColor == 'C') {

						TresEnRallaInstantiateTub (CyanTubPinturaPrefab , ActualHex, LastHexPressed, 0, 3);

					}
					else if(ActualHex.neigbours [1].HexColor == 'C' && ActualHex.neigbours [4].HexColor == 'C'){

						TresEnRallaInstantiateTub (CyanTubPinturaPrefab ,ActualHex, LastHexPressed, 1, 4);

					}
					else if(ActualHex.neigbours [2].HexColor == 'C' && ActualHex.neigbours [5].HexColor == 'C') {

						TresEnRallaInstantiateTub (CyanTubPinturaPrefab ,ActualHex, LastHexPressed, 2, 5);

					}
				}

				if (ActualHex.HexColor == 'M') {

					if (ActualHex.neigbours [0].HexColor == 'M' && ActualHex.neigbours [3].HexColor == 'M') {

						TresEnRallaInstantiateTub (MagentaTubPinturaPrefab ,ActualHex, LastHexPressed, 0, 3);

					}
					else if(ActualHex.neigbours [1].HexColor == 'M' && ActualHex.neigbours [4].HexColor == 'M'){

						TresEnRallaInstantiateTub (MagentaTubPinturaPrefab, ActualHex, LastHexPressed, 1, 4);

					}
					else if(ActualHex.neigbours [2].HexColor == 'M' && ActualHex.neigbours [5].HexColor == 'M') {

						TresEnRallaInstantiateTub (MagentaTubPinturaPrefab, ActualHex, LastHexPressed, 2, 5);

					}
				}

				if (ActualHex.HexColor == 'Y') {

					if (ActualHex.neigbours [0].HexColor == 'Y' && ActualHex.neigbours [3].HexColor == 'Y') {

						TresEnRallaInstantiateTub (YellowTubPinturaPrefab, ActualHex, LastHexPressed, 0, 3);

					}
					else if(ActualHex.neigbours [1].HexColor == 'Y' && ActualHex.neigbours [4].HexColor == 'Y'){

						TresEnRallaInstantiateTub (YellowTubPinturaPrefab, ActualHex, LastHexPressed, 1, 4);

					}
					else if(ActualHex.neigbours [2].HexColor == 'Y' && ActualHex.neigbours [5].HexColor == 'Y') {

						TresEnRallaInstantiateTub (YellowTubPinturaPrefab, ActualHex, LastHexPressed, 2, 5);

					}
				}
			}
		}
	}

	void TresEnRallaInstantiateTub(GameObject Tub, HexInfo ActualHex, HexInfo lastHexPressed, int i, int j){
	
		ResetHexagonValues (ActualHex.neigbours [i]);
		ResetHexagonValues (ActualHex.neigbours [j]);
		ResetHexagonValues (ActualHex);

		Tub.GetComponent<TubDePintura> ().actualHex = lastHexPressed;
		Instantiate (Tub, lastHexPressed.transform.position, Quaternion.identity);



	}
	/*void Combos(HexInfo Actualhex, int i){
		

		combo ComboTriadic;
		ComboTriadic.Color1.IsNeighbour = false;
		ComboTriadic.Color2.IsNeighbour = false;


		if (Actualhex.HexColor == 'C') {
		
			if (Actualhex.neigbours [i].HexColor == 'M' ) {

				ComboTriadic.Color1.IsNeighbour = true;
				ComboTriadic.Color1.i = i;


			} else if (Actualhex.neigbours [i].HexColor == 'Y') {

				ComboTriadic.Color2.IsNeighbour = true;
				ComboTriadic.Color2.i = i;
			}

		} else if (Actualhex.HexColor == 'M') {
		
		} else if (Actualhex.HexColor == 'Y') {

		}

		else if (Actualhex.HexColor == 'R') {

		} 
		else if (Actualhex.HexColor == 'B') {

		}
		else if (Actualhex.HexColor == 'G') {

		} 

		if (ComboTriadic.Color1.IsNeighbour == true && ComboTriadic.Color2.IsNeighbour == true) {

			print ("GOS");

		}

	}*/
	
	void IsClickable(HexInfo ActualHex){

		for (int i = 0 ; i < 6 ; i++) {
			

			if (ActualHex.neigbours[i] != null) {

				if (ActualHex.neigbours[i].Nucli == true) {
					ActualHex.Clickable = true;

				}
			}
		}
	}
	

	void ColorsChangeCyan(MeshRenderer mr, HexInfo hexInfo){

		if (mr.material.mainTexture == DefaultText || mr.material.mainTexture == null) {

			hexInfo.HexColor = 'C';
			mr.material.mainTexture = CyanTex;
			hexInfo.ColorDensity++;
			//NeighbourDensityManager (hexInfo);


		} else if (mr.material.mainTexture == CyanTex) {

			hexInfo.ColorDensity++;
			densityHigh (hexInfo);
			CurrentPigment--;
		}
		else if (mr.material.mainTexture == MagentaTex) {

			hexInfo.HexColor = 'B';
			mr.material.mainTexture = BlueTex;

		}
		else if (mr.material.mainTexture == YellowTex) {

			hexInfo.HexColor = 'G';
			mr.material.mainTexture = GreenTex;

		}

	}

	void ColorsChangeMagenta(MeshRenderer mr, HexInfo hexInfo){

		if (mr.material.mainTexture == DefaultText || mr.material.mainTexture == null) {

			hexInfo.HexColor = 'M';
			mr.material.mainTexture = MagentaTex;
			hexInfo.ColorDensity++;
			//NeighbourDensityManager (hexInfo);


		} else if (mr.material.mainTexture == MagentaTex) {

			hexInfo.ColorDensity++;
			densityHigh (hexInfo);
		}
		else if (mr.material.mainTexture == CyanTex) {

			hexInfo.HexColor = 'B';
			mr.material.mainTexture = BlueTex;

		}
		else if (mr.material.mainTexture == YellowTex) {

			hexInfo.HexColor = 'R';
			mr.material.mainTexture = RedTex;

		}

	}
	

	void ColorsChangeYellow(MeshRenderer mr, HexInfo hexInfo){

		if (mr.material.mainTexture == DefaultText || mr.material.mainTexture == null) {

			hexInfo.HexColor = 'Y';
			mr.material.mainTexture = YellowTex;
			hexInfo.ColorDensity++;
			//NeighbourDensityManager (hexInfo);


		} else if (mr.material.mainTexture == YellowTex) {

			hexInfo.ColorDensity++;
			densityHigh (hexInfo);

		}
		else if (mr.material.mainTexture == CyanTex) {

			hexInfo.HexColor = 'G';
			mr.material.mainTexture = GreenTex;

		}
		else if (mr.material.mainTexture == MagentaTex) {

			hexInfo.HexColor = 'B';
			mr.material.mainTexture = BlueTex;

		}

	}

	void densityHigh(HexInfo ActualHex){
	
		ActualHex.transform.localScale = new Vector3(1, 1, 3 * ActualHex.ColorDensity);
	}

	void NeighbourDensityManager(HexInfo ActualHex){

		for (int i = 0; i < 6; i++) {
			

			if (ActualHex.neigbours[i] != null) {

				if (ActualHex.neigbours[i].ColorDensity > 1) {
					ActualHex.neigbours[i].ColorDensity--;
					densityHigh (ActualHex.neigbours[i]);
					CurrentPigment++;
					break;
				}
			}
		}
	}
}

