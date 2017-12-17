 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour {

	public GameObject bola;
	int xHexPos;
	int yHexPos;

	public int MaxPigment = 10;
	public float CurrentPigment = 10;
	public float PigmentRatio;
	public Image PigmentBar;

	public Texture2D CyanTex;
	public Texture2D MagentaTex;
	public Texture2D YellowTex;

	public Texture2D RedTex;
	public Texture2D BlueTex;
	public Texture2D GreenTex;

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

	enum TwoStepsNeighbourPosition
	{
		TwoLeft,
		TwoUpLeft,
		TwoUpRight,
		TwoRight,
		TwoDownRight,
		TwoDownLeft,
		TwoNumPositions,

	}
	void Start(){



		HexInfo hex = GetComponentInChildren<HexInfo> ();

	

		}


	void Update () {

		if (CurrentPigment < 10) {
			CurrentPigment+=0.02f;
		}
		//densityHigh ();
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hitInfo;

		if(Physics.Raycast(ray, out hitInfo)){

			GameObject ourHitObject = hitInfo.collider.transform.gameObject;


			if (Input.GetMouseButtonDown (0) && CurrentPigment > 0) {

				HexInfo HexInfoObject = ourHitObject.GetComponentInParent<HexInfo> ();

				IsClickable(HexInfoObject);


				if(HexInfoObject.Clickable == true){
					
					MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer> ();

					//bola.transform.position = new Vector3 (ourHitObject.transform.position.x, 0.3f ,ourHitObject.transform.position.z);

					if (ColorHUD.ColorsSelected == 'C') {

						ColorsChangeCyan (mr, HexInfoObject);
						CurrentPigment--;
						//Debug.Log ("RayCast Hit");

					} else if (ColorHUD.ColorsSelected == 'M') {
					
						ColorsChangeMagenta (mr, HexInfoObject);
						CurrentPigment--;

					} else if (ColorHUD.ColorsSelected == 'Y') {
					
						ColorsChangeYellow (mr, HexInfoObject);
						CurrentPigment--;
					}

					combo1Check (HexInfoObject);
				}
			}
		}
		UpdatePigmentBar ();
	}

	void UpdatePigmentBar(){
	
		PigmentRatio = CurrentPigment / MaxPigment;

		PigmentBar.transform.localScale = new Vector3 (1, PigmentRatio, 1);

	
	}

	void ColorsChangeCyan(MeshRenderer mr, HexInfo hexInfo){

		if (mr.material.mainTexture == null) {

			hexInfo.HexColor = Color.cyan;
			mr.material.mainTexture = CyanTex;
			NeighbourDensityManager (hexInfo);


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

		if (mr.material.mainTexture == null) {

			hexInfo.HexColor = Color.magenta;
			mr.material.mainTexture = MagentaTex;
			NeighbourDensityManager (hexInfo);


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

		if (mr.material.mainTexture == null) {

			hexInfo.HexColor = Color.yellow;
			mr.material.mainTexture = YellowTex;
			NeighbourDensityManager (hexInfo);


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

		for (int i = (int)NeighbourPosition.Left; i < (int)NeighbourPosition.NumPositions; i++) {
			HexInfo neighbour = GetNeighbourByPosition ((NeighbourPosition)i, ActualHex);

			if (neighbour != null) {

				if (neighbour.ColorDensity > 1) {
					neighbour.ColorDensity--;
					densityHigh (neighbour);
					CurrentPigment++;
					break;
				}
			}
		}
	}

	void IsClickable(HexInfo ActualHex){

		for (int i = (int)NeighbourPosition.Left; i < (int)NeighbourPosition.NumPositions; i++) {
			HexInfo neighbour = GetNeighbourByPosition ((NeighbourPosition)i, ActualHex);

			//Debug.Log(x + " " + y + " " + neighbour);

			if (neighbour != null) {

				if (neighbour.Nucli == true || neighbour.ColorDensity > 0) {
					ActualHex.Clickable = true;

				}
			}
		}
	}

	//COMBOS

	void combo1(){
		
	}

	void combo1Check(HexInfo ActualHex){

		if (ActualHex.x > 0 && ActualHex.x < Map.width - 1 && ActualHex.y > 0 && ActualHex.y < Map.height - 1) {

			if (ActualHex.HexColor == GetNeighbourByPosition (NeighbourPosition.Left, ActualHex).HexColor && ActualHex.HexColor == GetNeighbourByPosition (NeighbourPosition.Right, ActualHex).HexColor) {
				combo1 ();
			} else if (ActualHex.HexColor == GetNeighbourByPosition (NeighbourPosition.UpLeft, ActualHex).HexColor && ActualHex.HexColor == GetNeighbourByPosition (NeighbourPosition.DownRight, ActualHex).HexColor) {
				combo1 ();
			} else if (ActualHex.HexColor == GetNeighbourByPosition (NeighbourPosition.DownLeft, ActualHex).HexColor && ActualHex.HexColor == GetNeighbourByPosition (NeighbourPosition.UpRight, ActualHex).HexColor) {
				combo1 ();
			} 
		}

		//FarNeigbours (6 cases)

		else if (ActualHex.x > 1 && ActualHex.HexColor ==  GetNeighbourByPosition (NeighbourPosition.Left,ActualHex).HexColor && ActualHex.HexColor == GetFarNeighbourByPosition (TwoStepsNeighbourPosition.TwoLeft,ActualHex).HexColor) {
			combo1 ();
		} 
		else if (ActualHex.x > 0 && ActualHex.y < Map.height - 2 && ActualHex.HexColor ==  GetNeighbourByPosition (NeighbourPosition.UpLeft,ActualHex).HexColor && ActualHex.HexColor == GetFarNeighbourByPosition (TwoStepsNeighbourPosition.TwoUpLeft,ActualHex).HexColor) {
			combo1 ();
		} 
		else if (ActualHex.x < Map.width - 1 && ActualHex.y < Map.height - 2 && ActualHex.HexColor ==  GetNeighbourByPosition (NeighbourPosition.UpRight,ActualHex).HexColor && ActualHex.HexColor == GetFarNeighbourByPosition (TwoStepsNeighbourPosition.TwoUpRight,ActualHex).HexColor) {
			combo1 ();
		} 
		else if (ActualHex.x > Map.width-2 && ActualHex.HexColor ==  GetNeighbourByPosition (NeighbourPosition.Right,ActualHex).HexColor && ActualHex.HexColor == GetFarNeighbourByPosition (TwoStepsNeighbourPosition.TwoRight,ActualHex).HexColor) {
			combo1 ();
		} 
		else if (ActualHex.x < Map.width - 1 && ActualHex.y > 1 && ActualHex.HexColor ==  GetNeighbourByPosition (NeighbourPosition.DownRight,ActualHex).HexColor && ActualHex.HexColor == GetFarNeighbourByPosition (TwoStepsNeighbourPosition.TwoDownRight,ActualHex).HexColor) {
			combo1 ();
		} 
		else if (ActualHex.x > 0 && ActualHex.y > 1 && ActualHex.HexColor ==  GetNeighbourByPosition (NeighbourPosition.DownLeft,ActualHex).HexColor && ActualHex.HexColor == GetFarNeighbourByPosition (TwoStepsNeighbourPosition.TwoDownLeft,ActualHex).HexColor) {
			combo1 ();
		} 
	}


	HexInfo GetNeighbourByPosition(NeighbourPosition position, HexInfo ActualHex)
	{
		HexInfo retVal = null;

		switch (position)
		{
		case NeighbourPosition.Left:
			retVal = GetLeftNeighbour (ActualHex);
			break;
		case NeighbourPosition.UpLeft:
			retVal = GetUpLeftNeighbour (ActualHex);
			break;
		case NeighbourPosition.UpRight:
			retVal = GetUpRightNeighbour(ActualHex);
			break;
		case NeighbourPosition.Right:
			retVal = GetRightNeighbour (ActualHex);
			break;
		case NeighbourPosition.DownRight:
			retVal = GetDownRightNeighbour(ActualHex);
			break;
		case NeighbourPosition.DownLeft:
			retVal = GetDownLeftNeighbour (ActualHex);
			break;
		default:
			break;
		}

		return retVal;
	}

	HexInfo GetFarNeighbourByPosition(TwoStepsNeighbourPosition position, HexInfo ActualHex)
	{
		HexInfo retVal = null;
	
		switch (position)
		{
		case TwoStepsNeighbourPosition.TwoLeft:
			retVal = Get2LeftNeighbour (ActualHex);
			break;
		case TwoStepsNeighbourPosition.TwoUpLeft:
			retVal = Get2UpLeftNeighbour (ActualHex);
			break;
		case TwoStepsNeighbourPosition.TwoUpRight:
			retVal = Get2UpRightNeighbour(ActualHex);
			break;
		case TwoStepsNeighbourPosition.TwoRight:
			retVal = Get2RightNeighbour (ActualHex);
			break;
		case TwoStepsNeighbourPosition.TwoDownRight:
			retVal = Get2DownRightNeighbour(ActualHex);
			break;
		case TwoStepsNeighbourPosition.TwoDownLeft:
			retVal = Get2DownLeftNeighbour (ActualHex);
			break;
		default:
			break;
		}

		return retVal;
	}

	HexInfo GetLeftNeighbour(HexInfo ActualHex)
	{
		HexInfo retVal = null;

		if (ActualHex.x > 0)
		{
			retVal = ActualHex.map.hexLines[ActualHex.y].columns[ActualHex.x-1];
		}

		return retVal;
	}

	HexInfo GetUpLeftNeighbour(HexInfo ActualHex){

		HexInfo retVal = null;

		if (ActualHex.y % 2 == 0 && ActualHex.x > 0 ) {
			retVal = ActualHex.map.hexLines [ActualHex.y+1].columns [ActualHex.x-1];

		} 
		else if (ActualHex.y % 2 == 1 && ActualHex.y < Map.height -1) {
			retVal = ActualHex.map.hexLines [ActualHex.y+1].columns [ActualHex.x];
		}
		return retVal;
	}

	HexInfo GetUpRightNeighbour(HexInfo ActualHex){

		HexInfo retVal = null;

		if (ActualHex.y % 2 == 0) {

			retVal = ActualHex.map.hexLines [ActualHex.y + 1].columns [ActualHex.x]; 

		} else if (ActualHex.y % 2 == 1 && ActualHex.x < Map.width - 1 && ActualHex.y < Map.height - 1) {
			
			retVal = ActualHex.map.hexLines [ActualHex.y + 1].columns [ActualHex.x + 1];
		}
		return retVal;
	}

	HexInfo GetRightNeighbour(HexInfo ActualHex){

		HexInfo retVal = null;

		if (ActualHex.x < Map.width-1) {

			retVal = ActualHex.map.hexLines [ActualHex.y].columns [ActualHex.x+1];

		}

		return retVal;
	}

	HexInfo GetDownRightNeighbour(HexInfo ActualHex){

		HexInfo retVal=null;

		if (ActualHex.y % 2 == 0 && ActualHex.y > 0) {
			retVal = ActualHex.map.hexLines [ActualHex.y-1].columns [ActualHex.x];
		}
		else if (ActualHex.y % 2 == 1 && ActualHex.x < Map.width-1 ) {

			retVal = ActualHex.map.hexLines[ActualHex.y - 1].columns[ActualHex.x + 1];
		}

		return retVal;
	}
	HexInfo GetDownLeftNeighbour(HexInfo ActualHex){

		HexInfo retVal=null;

		if (ActualHex.y % 2 == 0 && ActualHex.x > 0 && ActualHex.y > 0) {
			retVal = ActualHex.map.hexLines [ActualHex.y - 1].columns [ActualHex.x - 1];
		} 
		else if (ActualHex.y % 2 == 1) {
			retVal = ActualHex.map.hexLines [ActualHex.y-1].columns [ActualHex.x];
		}
		return retVal;
	}

	//FarNeighbours (em sembla que no calen els "if" , ja està en el Combo1Check();)


	HexInfo Get2LeftNeighbour(HexInfo ActualHex){
		HexInfo retVal = null;
		if (ActualHex.x > 1) {
			retVal = ActualHex.map.hexLines [ActualHex.y].columns [ActualHex.x-2];
		}
		return retVal;
	}
	HexInfo Get2UpLeftNeighbour(HexInfo ActualHex){
		HexInfo retVal = null;
		if (ActualHex.x > 0 && ActualHex.y < Map.height - 2) {
			retVal = ActualHex.map.hexLines [ActualHex.y + 2].columns [ActualHex.x - 1];
		}
		return retVal;
	}
	HexInfo Get2UpRightNeighbour(HexInfo ActualHex){
		HexInfo retVal = null;
		if (ActualHex.x < Map.width - 1 && ActualHex.y < Map.height - 2) {
			retVal = ActualHex.map.hexLines [ActualHex.y + 2].columns [ActualHex.x + 1];
		}
		return retVal;
	}
	HexInfo Get2RightNeighbour(HexInfo ActualHex){
		HexInfo retVal = null;
		if (ActualHex.x > Map.width-2) {
			retVal = ActualHex.map.hexLines [ActualHex.y].columns [ActualHex.x + 2];
		}
		return retVal;
	}
	HexInfo Get2DownRightNeighbour(HexInfo ActualHex){
		HexInfo retVal = null;
		if (ActualHex.x < Map.width - 1 && ActualHex.y > 1) {
			retVal = ActualHex.map.hexLines [ActualHex.y - 2].columns [ActualHex.x + 1];
		}
		return retVal;
	}
	HexInfo Get2DownLeftNeighbour(HexInfo ActualHex){
		HexInfo retVal = null;
		if (ActualHex.x > 0 && ActualHex.y > 1) {
			retVal = ActualHex.map.hexLines [ActualHex.y - 2].columns [ActualHex.x - 1];
		}
		return retVal;
	}



}
