using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoneyManager : MonoBehaviour {


	public static int Pigment ;
    public int startPigment = 40;
	public Text text;

	//COmbo variables
	private static int combo;
	private static float contador=0;

	void Start()
	{
        Pigment = startPigment;

	}

	void Update()
	{
		
		text.text = " " + Pigment;

		if(contador >= 0)
			contador -= Time.deltaTime;
	}

	public static void Combo()
	{
		combo++;

		if (contador<=0) 
			combo = 1;

		else if (combo == 3) {
			Debug.Log("Combo!");
		}
		else if (combo == 5) {
			Debug.Log("Super Combo!");
		}
		else if (combo == 7) {
			Debug.Log("Hiper Combo!");
		}
		else if (combo == 9) {
			Debug.Log("Insanity Combo!");
		}
		else if (combo == 12) {
			Debug.Log("2 pavas al lavabo Combo!");
		}

		Debug.Log (combo + " " + contador);
		contador = 4;
	}


}
