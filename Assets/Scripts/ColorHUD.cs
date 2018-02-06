using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorHUD : MonoBehaviour {



	public void CyanButton()
	{
		MouseManager.ColorInHand = 'C';
	}
	public void MagentaButton()
	{
		MouseManager.ColorInHand = 'M';
	}
	public void YellowButton()
	{
		MouseManager.ColorInHand = 'Y';
	}
}
