 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour {

	int xHexPos;
	int yHexPos;

	public Texture2D DefaultText;

	public Texture2D CyanTex;
	public Texture2D MagentaTex;
	public Texture2D YellowTex;

	public static char ColorInHand;

	public GameObject CyanTubPinturaPrefab;
	public GameObject MagentaTubPinturaPrefab;
	public GameObject YellowTubPinturaPrefab;

	public Vector3 SpraiPositionOffset;
	public Vector3 TubOfset;

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

		SpraiPositionOffset = new Vector3 (0, 0.485f, 0);
	
	}


	void Update () {

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hitInfo;

		if(Physics.Raycast(ray, out hitInfo)){

			GameObject ourHitObject = hitInfo.collider.transform.gameObject;



			if (Input.GetMouseButtonDown (0)) {

				HexInfo hexInfoObject = ourHitObject.GetComponentInChildren<HexInfo> ();

				if (MoneyManager.Pigment > 0) {
					if (hexInfoObject.Clickable == true) {
					
						MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer> ();

						if (ColorInHand == 'C') {

                            hexInfoObject.HexColor = 'C';
                            mr.material.mainTexture = CyanTex;
                            MoneyManager.Pigment -= 5;
		

						} else if (ColorInHand == 'M') {

                            hexInfoObject.HexColor = 'M';
                            mr.material.mainTexture = MagentaTex;
                            MoneyManager.Pigment -= 5;

						} else if (ColorInHand == 'Y') {

                            hexInfoObject.HexColor = 'Y';
                            mr.material.mainTexture = YellowTex;
                            MoneyManager.Pigment -= 5;
						}
					}
				}
			}
		}
	}

	

}

