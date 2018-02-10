using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoneyManager : MonoBehaviour {

	public static int Pigment = 50;
	private Text text;

	void Start()
	{
		text = GetComponent<Text> ();
	}

	void Update()
	{
		text.text = " " + Pigment + " ";
	}

}
