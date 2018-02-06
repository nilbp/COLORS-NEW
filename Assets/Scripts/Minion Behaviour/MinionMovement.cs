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

		Life = 1;
		NextHex = ActualHex.neigbours[3];
		target = NextHex.gameObject.transform;


	}

	void Update(){


		if (ActualHex.HexColor == 'W') {
			MovementRecte ();
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

		Size = 1+Life*0.2f;
		transform.localScale = new Vector3(Size,Size,Size);


	}

	void MovementRecte()
	{
		Vector3 dir= target.position - transform.position;
		float distanceThisFrame = speed * Time.fixedDeltaTime;

		if (dir.magnitude <= distanceThisFrame) {

			ActualHex = NextHex;
			NextHex = ActualHex.neigbours [3];

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

			Life++;
			ResetHexagonColorValues (ActualHex);
			//Es transformen en un altre color i life ++ 

		}
	
	
	}


}
