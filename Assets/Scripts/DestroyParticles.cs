using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InvokeRepeating("Destroy",1f,5f);
	}

    void Destroy()
    {
        GameObject particle = GameObject.FindGameObjectWithTag("Particle");

        if(particle != null)
            Destroy(particle);
    }
}
