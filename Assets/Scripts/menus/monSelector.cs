using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class monSelector : MonoBehaviour {

	public Button mon2btn;
	public Button mon3btn;
	public bool mon1clear = false;
	public bool mon2clear=false;

	void Start () {

		enable ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void enable(){

		if (mon1clear == true) {
			mon2btn.GetComponent<Button> ().interactable = true;

		} else if (mon2clear == true) {
			mon3btn.GetComponent<Button> ().interactable = true;
		}
	}

}
