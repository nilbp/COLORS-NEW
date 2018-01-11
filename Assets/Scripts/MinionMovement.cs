using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : MonoBehaviour {

	public HexInfo ActualHex;
	public HexInfo NextHex;
	public HexInfo Nucli;

	public Texture DefaultTexture;

	public int Life;
	private float Size;
	public char ColorIdentifier;

	Transform target;

	public float speed = 1;

	private bool rotated;

	void Start () {

		Life = 5;
		NextHex = ActualHex.neigbours[3];
		target = NextHex.gameObject.transform;


	}

	void Update(){


		if (ActualHex.HexColor == 'W') {
			Movement ();
		} 
		else {
			Colision ();
		}

		LifeManager ();

	}

	void LifeManager(){

		if (Life <= 0) {

			Destroy (gameObject);
			return;
		} 

		Size = Life*0.4f;
		transform.localScale = new Vector3(Size,Size,Size);


	}
	bool RandomDecrese(){
	
		int Rand = Random.Range(0, 2);

		if (Rand == 0)
			return true;
		else
			return false;
		
	}

	void Movement(){


		Vector3 dir= target.position - transform.position;

		float distanceThisFrame = speed * Time.fixedDeltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{

			ActualHex = NextHex;

			if (ActualHex.y > Nucli.y && RandomDecrese() == true || ActualHex.y > Nucli.y && ActualHex.x >= Nucli.x-2) {

				NextHex = ActualHex.neigbours [4];

				if (rotated == false) {

					transform.Rotate(1, 60, 1);
					rotated = true;
				}

			} 
			else {

				if (rotated) {
					transform.Rotate (1, -60, 1);
					rotated = false;
				}
				
				NextHex = ActualHex.neigbours [3];

			}

			if (NextHex == null) {
				Destroy (gameObject);
				return;
			}
			
			target = NextHex.gameObject.transform;

		}

	
			transform.Translate (dir.normalized * distanceThisFrame, Space.World);
	

	}

	void ResetHexagonColorValues(HexInfo ActualHex){

		ActualHex.HexColor = 'W';
		ActualHex.transform.localScale = new Vector3 (1, 1, 1);
		ActualHex.GetComponent<Renderer>().material.mainTexture = DefaultTexture;
		ActualHex.ColorDensity = 0;
	}

	void Colision(){

		if (ActualHex.HexColor == ColorIdentifier) {

			Life--;
			ResetHexagonColorValues (ActualHex);


		} 
		else {
			
			//Es transformen en un altre color?
		}
	
	
	}


}
