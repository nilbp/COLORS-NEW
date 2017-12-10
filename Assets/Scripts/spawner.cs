	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class spawner : MonoBehaviour {

		public Transform[] spawnLocation;
		public GameObject[] minionprefab;
		public GameObject[] minionspawn;
		public Transform nucli;


		public float speed = 1;
	private float startTime;
	private float dist;


		void Start(){

		spawnminion ();
		startTime = Time.time;
		dist = Vector3.Distance (spawnLocation [1].position, nucli.position);
			
		}


		void spawnminion (){

			minionspawn [0] = Instantiate (minionprefab [0], spawnLocation [0].transform.position, Quaternion.Euler (0, 0, 0)) as GameObject;
			minionspawn [1] = Instantiate (minionprefab [1], spawnLocation [1].transform.position, Quaternion.Euler (0, 0, 0)) as GameObject;
			minionspawn [2] = Instantiate (minionprefab [2], spawnLocation [2].transform.position, Quaternion.Euler (0, 0, 0)) as GameObject;
	}

	void Update(){
	
		//float step = speed * Time.deltaTime;
		//transform.position = Vector3.MoveTowards (minionspawn[0].transform.position, nucli.position, step);

		//Vector3 posicion = nucli.transform.position ;
		//minionspawn[0].transform.position = new Vector3 (transform.position,nucli.position, step);
		//minionspawn[0].transform.position = new Vector3(nucli.position.x, 0, nucli.position.z);
	
		float distCover = (Time.time - startTime) * speed;
		float fracjourn = distCover / dist;
		minionspawn [1].transform.position = Vector3.Lerp (spawnLocation [1].position, nucli.position, fracjourn);

	}

}