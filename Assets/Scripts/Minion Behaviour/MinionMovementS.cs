using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovementS : MonoBehaviour {

	public HexInfo ActualHex;
	public HexInfo NextHex;
	public HexInfo Nucli;

	public Texture DefaultTexture;

	public int Life;
	private float Size;
	public char ColorIdentifier;

	private int counter = 0;

	Transform target;

	public float speed = 1;

	void Start () {

		Life = 1;
		NextHex = ActualHex.neigbours[3];
		target = NextHex.gameObject.transform;


	}

	void Update(){


		if (ActualHex.HexColor == 'W') {
			MovementS ();
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

	void MovementS(){

		Vector3 dir= target.position - transform.position;
		float distanceThisFrame = speed * Time.fixedDeltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			ActualHex = NextHex;

			if (counter == 0) {
				
				NextHex = ActualHex.neigbours [4];
				transform.Rotate (1, 60, 1);
				counter = 1;
			} 
			else if (counter == 1) 
			{
				
				NextHex = ActualHex.neigbours [3];
				transform.Rotate (1, -60, 1);
				counter = 2;	
			}
			else if(counter == 2)
			{
				NextHex = ActualHex.neigbours [2];
				transform.Rotate (1, -60, 1);
				counter = 3;
			}
			else  
			{
				NextHex = ActualHex.neigbours [3];
				transform.Rotate (1, 60, 1);
				counter = 0;	
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

			Life++;
			ResetHexagonColorValues (ActualHex);
			//Es transformen en un altre color i life ++ 

		}


	}


}
