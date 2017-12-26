 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour {

	public GameObject bola;
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
	private MeshRenderer NucliMesh;
	char ColorInHand;

	public GameObject Totem;

	public struct colorCombo{

		public int i;
		public bool IsNeighbour;
	}

	public struct combo{

		public colorCombo Color1;
		public colorCombo Color2;
	}

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

		Nucli = GameObject.Find ("Hex_6_4").GetComponentInChildren<HexInfo> ();
		NucliMesh = Nucli.GetComponentInChildren<MeshRenderer> ();
		RandomPrimaryColorSpawn ();
	
	}


	void Update () {

		if (CurrentPigment < MaxPigment) {
			CurrentPigment+=0.03f;
		}
		//densityHigh ();
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hitInfo;

		if(Physics.Raycast(ray, out hitInfo)){

			GameObject ourHitObject = hitInfo.collider.transform.gameObject;



			if (Input.GetMouseButtonDown (0)) {

				HexInfo HexInfoObject = ourHitObject.GetComponentInChildren<HexInfo> ();

				//IsClickable(HexInfoObject);
					



				if (CurrentPigment > 1) {
					if (HexInfoObject.Clickable == true) {
					
						MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer> ();

						//bola.transform.position = new Vector3 (ourHitObject.transform.position.x, 0.3f ,ourHitObject.transform.position.z);

						if (ColorInHand == 'C') {

							ColorsChangeCyan (mr, HexInfoObject);
							CurrentPigment -= 2;
		

						} else if (ColorInHand == 'M') {
					
							ColorsChangeMagenta (mr, HexInfoObject);
							CurrentPigment -= 2;

						} else if (ColorInHand == 'Y') {
					
							ColorsChangeYellow (mr, HexInfoObject);
							CurrentPigment -= 2;
						}
						ComboTresEnRalla ();
						ComboTresAgrupats (HexInfoObject);
						RandomPrimaryColorSpawn ();
		
					}
				}
			}

		}
		UpdatePigmentBar ();


	}

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
	
	}

	void UpdatePigmentBar(){
	
		PigmentRatio = CurrentPigment / MaxPigment;

		PigmentBar.transform.localScale = new Vector3 (1, PigmentRatio, 1);

	
	}

	void ResetHexagonValues(HexInfo ActualHex){

		ActualHex.GetComponentInChildren<MeshRenderer> ().material.mainTexture = null;
		ActualHex.HexColor = 'W';
		ActualHex.transform.localScale = new Vector3 (1, 1, 1);
		ActualHex.ColorDensity = 0;

	}

	void ComboTriada(HexInfo ActualHex, int i, int j){

		DefenseTotem totem = Totem.GetComponentInChildren<DefenseTotem> ();

		totem.TotemColor1 = ActualHex.HexColor;
		totem.TotemColor1 = ActualHex.neigbours[i].HexColor;
		totem.TotemColor1 = ActualHex.neigbours[j].HexColor;

		ResetHexagonValues (ActualHex.neigbours [i]);
		ResetHexagonValues (ActualHex.neigbours [j]);
			
		ActualHex.ColorDensity = 0;
		ActualHex.transform.localScale = new Vector3 (1, 1, 1);
		ActualHex.GetComponent<MeshRenderer> ().material.color = Color.white;

		Instantiate (Totem, ActualHex.transform.position, Quaternion.identity);
							

		
	}

	void ComboTresAgrupats(HexInfo ActualHex){

		for(int i = 0; i<5;i++){
			for(int j = i+1; j<6;j++){

				if (ActualHex.neigbours [i].ColorDensity == ActualHex.neigbours [j].ColorDensity && ActualHex.neigbours [j].ColorDensity == ActualHex.ColorDensity) {

					if (ActualHex.HexColor == 'C') {

						if (ActualHex.neigbours [i].HexColor == 'M' && ActualHex.neigbours [j].HexColor == 'Y' || ActualHex.neigbours [j].HexColor == 'M' && ActualHex.neigbours [i].HexColor == 'Y') {

							ComboTriada (ActualHex, i, j);

						}


					} else if (ActualHex.HexColor == 'M') {

						if (ActualHex.neigbours [i].HexColor == 'C' && ActualHex.neigbours [j].HexColor == 'Y' || ActualHex.neigbours [j].HexColor == 'M' && ActualHex.neigbours [i].HexColor == 'Y') {

							ComboTriada (ActualHex, i, j);

						}
				
					} else if (ActualHex.HexColor == 'Y') {

						if (ActualHex.neigbours [i].HexColor == 'M' && ActualHex.neigbours [j].HexColor == 'C' || ActualHex.neigbours [j].HexColor == 'M' && ActualHex.neigbours [i].HexColor == 'Y') {

							ComboTriada (ActualHex, i, j);

						}

					} else if (ActualHex.HexColor == 'B') {

						if (ActualHex.neigbours [i].HexColor == 'G' && ActualHex.neigbours [j].HexColor == 'R' || ActualHex.neigbours [j].HexColor == 'M' && ActualHex.neigbours [i].HexColor == 'Y') {

							ComboTriada (ActualHex, i, j);

						}

					} else if (ActualHex.HexColor == 'R') {

						if (ActualHex.neigbours [i].HexColor == 'G' && ActualHex.neigbours [j].HexColor == 'B' || ActualHex.neigbours [j].HexColor == 'M' && ActualHex.neigbours [i].HexColor == 'Y') {

							ComboTriada (ActualHex, i, j);

						}

					} else if (ActualHex.HexColor == 'G') {

						if (ActualHex.neigbours [i].HexColor == 'R' && ActualHex.neigbours [j].HexColor == 'B' || ActualHex.neigbours [j].HexColor == 'M' && ActualHex.neigbours [i].HexColor == 'Y') {

							ComboTriada (ActualHex, i, j);

						}
					
					}
				}
			}
		}
	}



	void ComboTresEnRalla(){


		for (int x = 0; x < Map.width; x++) {
			for (int y = 0; y < Map.height; y++) {
				

				GameObject HexGameObject = GameObject.Find ("Hex_" + x + "_" + y);
				HexInfo ActualHex = HexGameObject.GetComponentInChildren<HexInfo> ();

				int[] ColorMatch= new int[6];



				for (int i = 0; i < 6; i++) {

					if (ActualHex.neigbours [i] != null) {
						if(ActualHex.HexColor != 'W' ){

							Combos(ActualHex, i);

						}
					}
				}



			}
		}
	}


	void Combos(HexInfo Actualhex, int i){
		

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

	}
	
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

