 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

	public GameObject bola;
	int xHexPos;
	int yHexPos;

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

	void ColorsChangeCyan(MeshRenderer mr, HexInfo HexColors){

		if (mr.material.color == Color.white) {
		
			mr.material.color = Color.cyan;
			HexColors.HexColor = Color.cyan;

		}
		else if (mr.material.color == Color.magenta) {

			mr.material.color = Color.blue;
			HexColors.HexColor = Color.blue;

		}
		else if (mr.material.color == Color.yellow) {

			mr.material.color = Color.green;
			HexColors.HexColor = Color.green;

		}

	}
	void ColorsChangeMagenta(MeshRenderer mr , HexInfo HexColors){
	
		if (mr.material.color == Color.white) {

			mr.material.color = Color.magenta;

		}
		else if (mr.material.color == Color.cyan) {

			mr.material.color = Color.blue;

		}
		else if (mr.material.color == Color.yellow) {

			mr.material.color = Color.red;

		}

	
	}
	void ColorsChangeYellow(MeshRenderer mr , HexInfo HexColors){
	
	
		if (mr.material.color == Color.white) {

			mr.material.color = Color.yellow;

		}
		else if (mr.material.color == Color.cyan) {

			mr.material.color = Color.green;

		}
		else if (mr.material.color == Color.magenta) {

			mr.material.color = Color.red;

		}
	
	}



}
