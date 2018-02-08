using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovementRandom : MonoBehaviour {

		public HexInfo ActualHex;
		public HexInfo NextHex;
		public HexInfo Nucli;
		public Texture DefaultTexture;
		Transform target;
		private bool facingNordEast;
		private bool facingSouthEast;
		private float Size;
		
	   	// CARACTERISTIQUES
		public int Life;
		public char ColorIdentifier;
		public float speed = 1;
		
		//MÉS GRAN L'ENTER = MENYS POSSIBILITATS QUE CANVII.
		public int chanceToChangeDirection = 10;

		void Start () {

			Life = 1;
			NextHex = ActualHex.neigbours[3];
			target = NextHex.gameObject.transform;


		}

		void Update(){


			if (ActualHex.HexColor == 'W') {
				MovementRandom ();
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
		int RandomInt(int from, int to){
			int Rand = Random.Range(from, to);
			return Rand;
		}


		void MovementRandom(){
			Vector3 dir= target.position - transform.position;
			float distanceThisFrame = speed * Time.fixedDeltaTime;

			if (dir.magnitude <= distanceThisFrame)
			{
				ActualHex = NextHex;

				if (RandomInt (0, chanceToChangeDirection) == 0) {

					NextHex = ActualHex.neigbours [4];
					
					if (facingNordEast == true) {
						transform.Rotate (1, 120, 1);
						facingSouthEast = true;
						
					}
					else if (facingSouthEast == false) {

						transform.Rotate (1, 60, 1);
						facingSouthEast = true;
					} 
					facingNordEast = false;
				} 
				else if (RandomInt (0, chanceToChangeDirection) == 1) {
				
					NextHex = ActualHex.neigbours [2];

					if (facingSouthEast == true) {

						transform.Rotate (1, -120, 1);
						facingNordEast = true;
						
					} 
					else if (facingNordEast == false) {
						transform.Rotate (1, -60, 1);
						facingNordEast = true;
					}
					facingSouthEast = false;
				}
				else {

					NextHex = ActualHex.neigbours [3];
					
					if (facingNordEast == true) {
						transform.Rotate (1, 60, 1);
						facingNordEast = false;
					} 
					else if (facingSouthEast == true) {
						transform.Rotate (1, -60, 1);
						facingSouthEast = false;
					}
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
