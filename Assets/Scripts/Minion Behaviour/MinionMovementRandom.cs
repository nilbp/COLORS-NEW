using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovementRandom : MonoBehaviour {

		public HexInfo ActualHex;
		public HexInfo NextHex;
		
		//EVITA QUE DETECTI COLISIÓ DE COLOR DESPRÉS DE PASSAR PEL HEX
		private float maxDist = 0.7f;
		private float minDist = 0.3f;
		private bool neutralHex = false;

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
		
		//COMPROVAR QUE NO SURTIN DEL MAPA
		public int lastSpawnPoint = 7;
		public int firstSpawnPoint = 0;

		//MÉS GRAN L'ENTER = MENYS POSSIBILITATS QUE CANVII.
		public int chanceToChangeDirection = 10;

		//Valor del mínon en funció de la dificultat de matar-lo
		public int minionValue = 0;

		void Start () {

			Life = 1;
			NextHex = ActualHex.neigbours[3];
			target = NextHex.gameObject.transform;
			minionValue = Life * 10;

		}

		void Update(){

			if (ActualHex.HexColor == 'W' || !neutralHex) 
				MovementRandom ();
			else 
				Colision ();

			LifeManager ();

		}

		void LifeManager(){

			if (Life <= 0) {
				
				MoneyManager.Pigment += minionValue;
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
			
			//EVITA QUE DETECTI COLISIÓ DE COLOR DESPRÉS DE PASSAR PEL HEX
			if (dir.magnitude < maxDist && dir.magnitude > minDist)
				neutralHex = false;
			else if (dir.magnitude < minDist) {
				neutralHex = true;
				ActualHex = NextHex;
			}
			
			if (dir.magnitude <= distanceThisFrame)
			{

				if (RandomInt (0, chanceToChangeDirection) == 0 && ActualHex.y > firstSpawnPoint) {

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
				else if (RandomInt (0, chanceToChangeDirection) == 1 && ActualHex.y < lastSpawnPoint) {
				
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
