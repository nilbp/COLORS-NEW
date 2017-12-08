 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

	public GameObject bola;
	int xHexPos;
	int yHexPos;

	public Texture2D CyanTex;
	public Texture2D MagentaTex;
	public Texture2D YellowTex;

	public Texture2D RedTex;
	public Texture2D BlueTex;
	public Texture2D GreenTex;

	void Update () {

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hitInfo;

		if(Physics.Raycast(ray, out hitInfo)){

			GameObject ourHitObject = hitInfo.collider.transform.gameObject;


			if (Input.GetMouseButtonDown (0) && Pigment.pigment > 0) {

				HexInfo HexInfoObject = ourHitObject.GetComponentInParent<HexInfo> ();

				if(HexInfoObject.Clickable == true){
					
					MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer> ();

					//bola.transform.position = new Vector3 (ourHitObject.transform.position.x, 0.3f ,ourHitObject.transform.position.z);

					if (ColorHUD.ColorsSelected == 'C') {
					
						ColorsChangeCyan (mr, HexInfoObject);
						Pigment.pigment--;

					} else if (ColorHUD.ColorsSelected == 'M') {
					
						ColorsChangeMagenta (mr, HexInfoObject);
						Pigment.pigment--;

					} else if (ColorHUD.ColorsSelected == 'Y') {
					
						ColorsChangeYellow (mr, HexInfoObject);
						Pigment.pigment--;
					}

				
				}
			}
		}
	}


	void ColorsChangeCyan(MeshRenderer mr, HexInfo hexInfo){

		if (mr.material.mainTexture == null) {

			hexInfo.HexColor = Color.cyan;
			mr.material.mainTexture = CyanTex;


		} else if (mr.material.mainTexture == CyanTex) {

			hexInfo.ColorDensity++;
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


		} else if (mr.material.mainTexture == MagentaTex) {

			hexInfo.ColorDensity++;
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


		} else if (mr.material.mainTexture == YellowTex) {

			hexInfo.ColorDensity++;
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



}
