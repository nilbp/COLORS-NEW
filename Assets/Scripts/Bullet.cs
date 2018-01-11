
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;

	public float speed = 5f;

	public GameObject impactEffect;

	public void chase(Transform _target){

		//Aqui dins es pot instanciar particules, set speed of the bulled, damage, etc...
		target = _target;

	}
	void Update () {

		if (target == null) 
		{

			Destroy (gameObject);
			return;
		
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget ();
			return;
		}

		transform.Translate (dir.normalized * distanceThisFrame, Space.World);
	}

	void HitTarget(){

		GameObject effectIns = (GameObject)Instantiate (impactEffect, transform.position, transform.rotation);
		Destroy (effectIns, 2f); 

		Destroy (gameObject);
		target.GetComponent<MinionMovement> ().Life--;

	}
}
