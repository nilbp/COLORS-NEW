using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoneyManager : MonoBehaviour {

    private static GameObject canvas;

    public static int Pigment ;
    public int startPigment = 40;
	public Text text;

	//COmbo variables
	private static int combo;
	private static float contador=0;

    public GameObject combo1;
    public GameObject combo2;
    public GameObject combo3;
    public GameObject combo4;
    public GameObject combo5;

    public static GameObject combo_1;
    public static GameObject combo_2;
    public static GameObject combo_3;
    public static GameObject combo_4;
    public static GameObject combo_5;

    public GameObject MoneyPopUp;
    private static GameObject MoneyPopUp1;

    void Start()
	{
        Pigment = startPigment;
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        MoneyPopUp1 = MoneyPopUp;

        combo_1 = combo1;
        combo_2 = combo2;
        combo_3 = combo3;
        combo_4 = combo4;
        combo_5 = combo5;
    }

	void Update()
	{
		
		text.text = " " + Pigment;

		if(contador >= 0)
			contador -= Time.deltaTime;
	}

    public static void PopUpText(Transform transform, Color lastColor, int minionValue)
    {
        GameObject instance = Instantiate(MoneyPopUp1);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;

        Text PopUpText = instance.GetComponentInChildren<Text>();
        PopUpText.text = "+ " + minionValue;
        PopUpText.color = lastColor;
    }

	public static void Combo()
	{
		combo++;

        if (contador <= 0)
            combo = 1;

        else if (combo == 3)
        {
            GameObject instance = Instantiate(combo_1);
        }
        else if (combo == 5)
        {
            GameObject instance = Instantiate(combo_1);
        }
        else if (combo == 7)
        {
            GameObject instance = Instantiate(combo_1);
        }
        else if (combo == 9)
        {
            GameObject instance = Instantiate(combo_1);
        }
        else if (combo == 12)
        {
            GameObject instance = Instantiate(combo_1);
        }
        

		Debug.Log (combo + " " + contador);
		contador = 4;
	}


}
