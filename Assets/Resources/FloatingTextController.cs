using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour {

	public static FloatingText popUp;
	private static GameObject canvas;

	public static void Initialize(){

		canvas = GameObject.Find ("Canvas");
		//if(!popUp)
		//popUp = Resources.Load<FloatingText> ("Prefabs/popUpManager");
	}

	public static void CreateFloatingText(string text, Transform location){

		FloatingText instance = Instantiate (popUp);
		instance.transform.SetParent (canvas.transform, false);
		instance.SetText (text);
	}
}
