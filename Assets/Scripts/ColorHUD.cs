using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorHUD : MonoBehaviour {

    public GameObject cyanButton;
    public GameObject magentaButton;
    public GameObject yellowButton;

    public void NoColor()
    {
        MouseManager.ColorInHand = ' ';
    }

	public void CyanButton()
	{
		MouseManager.ColorInHand = 'C';
        cyanButton.SetActive(true);
        magentaButton.SetActive(false);
        yellowButton.SetActive(false);
    }
	public void MagentaButton()
	{
		MouseManager.ColorInHand = 'M';
        cyanButton.SetActive(false);
        magentaButton.SetActive(true);
        yellowButton.SetActive(false);
    }
	public void YellowButton()
	{
		MouseManager.ColorInHand = 'Y';
        cyanButton.SetActive(false);
        magentaButton.SetActive(false);
        yellowButton.SetActive(true);
    }


}
