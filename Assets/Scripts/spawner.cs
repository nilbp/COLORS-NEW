	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class spawner : MonoBehaviour {

		public Transform[] spawnLocation;
		public GameObject[] minionprefab;
		public GameObject[] minionspawn;
		public Transform nucli;

		public float speed = 1;



		void Start(){

			spawnminion ();

		}


		void spawnminion (){

			minionspawn [0] = Instantiate (minionprefab [0], spawnLocation [0].transform.position, Quaternion.Euler (0, 0, 0)) as GameObject;
			minionspawn [1] = Instantiate (minionprefab [1], spawnLocation [1].transform.position, Quaternion.Euler (0, 0, 0)) as GameObject;
			minionspawn [2] = Instantiate (minionprefab [2], spawnLocation [2].transform.position, Quaternion.Euler (0, 0, 0)) as GameObject;
	}

	void Update(){
	
		float step = speed * Time.deltaTime;
		minionprefab[0].transform.position = Vector3.MoveTowards (transform.position, nucli.position, step);
	
	}

}