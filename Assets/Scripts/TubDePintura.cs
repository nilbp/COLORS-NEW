using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubDePintura : MonoBehaviour {

	[Header("Atributes")]

	public int hexRange = 3;

	public float range = 1.2f;
	public float FireRatio = 1f; //3 = 3s ? 
	public float FireCountdown = 0f;

	[Header("Unity Setup Fields")]

	public HexInfo actualHex;

	public Transform target;
	public string enemyTag = "Enemy";

	public char TotemColor;

	public GameObject bulletPrefab;
	public Transform firePoint;

	//per saber el rango cap a endavant en funció el numero de hexes 
	private HexInfo[] ListOfHexesInRange; 

	void Start(){

		SetRange ();
		InvokeRepeating ("UpdateTarget", 0f, 0.5f);


	}

	void UpdateTarget(){

		GameObject[] enemies = GameObject.FindGameObjectsWithTag (enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;

		foreach (GameObject enemy in enemies) 
		{
			float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance) 
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy; 
			}
		}

		if (nearestEnemy != null /*podriem comprovar si està en range per no fer
									un get component de tots els minions del camp
									&& distanceToEnemy < range*/) {

			MinionMovement minion = nearestEnemy.GetComponentInParent<MinionMovement> ();

			if (IsInHexRange (minion) == true && minion.ColorIdentifier == TotemColor) {


					target = nearestEnemy.transform;

			}
		}
	}

	void SetRange(){

		ListOfHexesInRange = new HexInfo[hexRange];
		HexInfo newHex = actualHex;

		for (int i = 0; i < hexRange; i++) {

			if (newHex.neigbours [0] == null)
				return;

			ListOfHexesInRange[i] = newHex.neigbours [0];
			newHex = newHex.neigbours [0];

			Debug.Log (ListOfHexesInRange [i].x);
		}

	}

	bool IsInHexRange(MinionMovement minion){

		for(int i = 0; i< hexRange; i++){
			if (ListOfHexesInRange [i] == minion.ActualHex) 
				return true;	
		}
		return false;

	}

	void Update(){

		if (target == null) 
			return;
		if (FireCountdown <= 0f) 
		{
			Shoot ();
			FireCountdown = 1f / FireRatio;
		}

		FireCountdown -= Time.deltaTime;

	}

	void Shoot(){

		GameObject bulletGO = (GameObject)Instantiate (bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet> ();

		if (bullet != null) {

			bullet.chase (target);

		}

		//Destroy (target.gameObject);

	}

	void OnDrawGizmosSelected(){

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, range);

	}

}
