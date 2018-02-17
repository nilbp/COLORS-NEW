using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoneyManager : MonoBehaviour {

	public static int Pigment ;
    public int startPigment = 100;
	private Text text;

	void Start()
	{
        Pigment = startPigment;
		text = GetComponent<Text> ();
	}

	void Update()
	{
		//text.text = " " + Pigment + " ";
	}

}
