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
						ComboCheck ();
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

	void ComboCheck(){


		for (int x = 0; x < Map.width; x++) {
			for (int y = 0; y < Map.height; y++) {
				

				GameObject HexGameObject = GameObject.Find ("Hex_" + x + "_" + y);
				HexInfo ActualHex = HexGameObject.GetComponentInChildren<HexInfo> ();

				int[] ColorMatch= new int[6];

				for (int i = 0; i < 6; i++) {

					if (ActualHex.neigbours [i] != null) {

						if (ActualHex.HexColor != Color.white && ActualHex.HexColor == ActualHex.neigbours [i].HexColor) {
							ColorMatch [i] = 1;
						}
					}
				}

				Combo1 (ColorMatch, ActualHex);
				totems (ActualHex);
			}
		}
	}

	void totems(HexInfo ActualHex){

		if (ActualHex.ColorDensity > 4) {

			ActualHex.ColorDensity = 0;
			ActualHex.transform.localScale = new Vector3 (1, 1, 1);
			Instantiate (Totem, ActualHex.transform.position, Quaternion.identity);
		}

	}

	void Combo1(int[] ColorMatch, HexInfo Actualhex){

		if (ColorMatch [0] == 1 && ColorMatch [3] == 1 || ColorMatch [1] == 1 && ColorMatch [4] == 1 || ColorMatch [2] == 1 && ColorMatch [5] == 1) {
			print ("Combo One Biach");
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

			hexInfo.HexColor = Color.cyan;
			mr.material.mainTexture = CyanTex;
			//NeighbourDensityManager (hexInfo);


		} else if (mr.material.mainTexture == CyanTex) {

			hexInfo.ColorDensity++;
			densityHigh (hexInfo);
			CurrentPigment--;
		}
		else if (mr.material.mainTexture == MagentaTex) {

			hexInfo.HexColor = Color.cyan;
			mr.material.mainTexture = BlueTex;

		}
		else if (mr.material.mainTexture == YellowTex) {

			hexInfo.HexColor = Color.green;
			mr.material.mainTexture = GreenTex;

		}

	}

	void ColorsChangeMagenta(MeshRenderer mr, HexInfo hexInfo){

		if (mr.material.mainTexture == DefaultText || mr.material.mainTexture == null) {

			hexInfo.HexColor = Color.magenta;
			mr.material.mainTexture = MagentaTex;
			//NeighbourDensityManager (hexInfo);


		} else if (mr.material.mainTexture == MagentaTex) {

			hexInfo.ColorDensity++;
			densityHigh (hexInfo);
		}
		else if (mr.material.mainTexture == CyanTex) {

			hexInfo.HexColor = Color.blue;
			mr.material.mainTexture = BlueTex;

		}
		else if (mr.material.mainTexture == YellowTex) {

			hexInfo.HexColor = Color.red;
			mr.material.mainTexture = RedTex;

		}

	}
	

	void ColorsChangeYellow(MeshRenderer mr, HexInfo hexInfo){

		if (mr.material.mainTexture == DefaultText || mr.material.mainTexture == null) {

			hexInfo.HexColor = Color.yellow;
			mr.material.mainTexture = YellowTex;
			//NeighbourDensityManager (hexInfo);


		} else if (mr.material.mainTexture == YellowTex) {

			hexInfo.ColorDensity++;
			densityHigh (hexInfo);

		}
		else if (mr.material.mainTexture == CyanTex) {

			hexInfo.HexColor = Color.green;
			mr.material.mainTexture = GreenTex;

		}
		else if (mr.material.mainTexture == MagentaTex) {

			hexInfo.HexColor = Color.blue;
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
