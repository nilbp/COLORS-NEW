using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorHUD : MonoBehaviour {

	public Button Cyan;
	public Button Magenta;
	public Button Yellow;

	public static char ColorsSelected;
	public char Hola;

	void Update(){
	
		Cyan.onClick.AddListener (ButtonHUDCyan);
		Magenta.onClick.AddListener (ButtonHUDMagenta);
		Yellow.onClick.AddListener (ButtonHUDYellow);
		Hola = ColorsSelected;
	}

	void ButtonHUDCyan(){
	
		ColorsSelected = 'C';

	}
	void ButtonHUDMagenta(){
	
		ColorsSelected = 'M';
	}
	void ButtonHUDYellow(){
		
		ColorsSelected = 'Y';
	
	}

}
