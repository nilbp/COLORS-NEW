using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoneyManager : MonoBehaviour {

	public static int Pigment ;
    public int startPigment = 40;
	public Text text;

	void Start()
	{
        Pigment = startPigment;

	}

	void Update()
	{
		text.text = " " + Pigment;
	}

}
