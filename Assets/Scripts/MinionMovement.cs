using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : MonoBehaviour {

	public HexInfo ActualHex;
	public HexInfo NextHex;

	public HexInfo Nucli;

	public char ColorIdentifier;

	Transform target;

	public float speed = 1;

	void Start () {

		NextHex = ActualHex.neigbours[3];
		target = NextHex.gameObject.transform;


	}

	void Update(){

		Movement ();

	}

	void Movement(){


		Vector3 dir= target.position - transform.position;

		float distanceThisFrame = speed * Time.fixedDeltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			

			if (ActualHex.x == 2) {

				ActualHex = NextHex;
				NextHex = ActualHex.neigbours [4];

				transform.Rotate(0,0, transform.rotation.z+60);
			} 
			else {
				
				if (ActualHex.x == 3) {
					transform.Rotate(0,0, transform.rotation.z-60);
				}

				ActualHex = NextHex;
				NextHex = ActualHex.neigbours [3];

			}

			target = NextHex.gameObject.transform;

		}

	
			transform.Translate (dir.normalized * distanceThisFrame, Space.World);
	

	}
}
