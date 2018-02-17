using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popups : MonoBehaviour {

	public GameObject window;
	public Text messageField;


	public void Show (string message){

		messageField.text = message;
		window.SetActive (true);
	}

	public void hide(){

		window.SetActive (false);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
