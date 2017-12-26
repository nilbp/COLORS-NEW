using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseTotem : MonoBehaviour {

	[Header("Atributes")]

	public float range = 1f; //distancia entre hex i hex
	public float FireRatio = 1f; //3 = 3s ? 
	public float FireCountdown = 0f;

	[Header("Unity Setup Fields")]

	public Transform target;
	public string enemyTag = "Enemy";

	public char TotemColor1;
	public char TotemColor2;
	public char TotemColor3;

	void Start(){
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

		if (nearestEnemy != null && shortestDistance <= range ) 
		{
				/*if (nearestEnemy.GetComponent<Minions>().ColorIdentifier == TotemColor1 ||
					nearestEnemy.GetComponent<Minions>().ColorIdentifier == TotemColor2 ||
					nearestEnemy.GetComponent<Minions>().ColorIdentifier == TotemColor3 ) {*/
					
					target = nearestEnemy.transform;

				
		}
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
	
		Destroy (target.gameObject);
	
	}

	void OnDrawGizmosSelected(){
	
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, range);
	
	}
	 
}
