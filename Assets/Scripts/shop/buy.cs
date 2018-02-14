using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buy : MonoBehaviour {

	public int money = 100;

	public Button srg1btn;
	public Button srg2btn;
	public Button srg3;
	public Button srg4;
	public Button srg5;

	public bool SRG1 = false;
	public bool SRG2 = false;
	public bool SRG3 = false;
	public bool SRG4 = false;
	public bool SRG5 = false;

	public bool SRT1 = false;
	public bool SRT2 = false;
	public bool SRT3 = false;
	public bool SRT4 = false;
	public bool SRT5 = false;

	public bool SDG1 = false;
	public bool SDG2 = false;
	public bool SDG3 = false;
	public bool SDG4 = false;
	public bool SDG5 = false;

	public bool TRG1 = false;
	public bool TRG2 = false;
	public bool TRG3 = false;
	public bool TRG4 = false;
	public bool TRG5 = false;

	public bool TRT1 = false;
	public bool TRT2 = false;
	public bool TRT3 = false;
	public bool TRT4 = false;
	public bool TRT5 = false;

	public bool TDG1 = false;
	public bool TDG2 = false;
	public bool TDG3 = false;
	public bool TDG4 = false;
	public bool TDG5 = false;


	void Start () {
		
		srg1btn.interactable = false;
		srg2btn.interactable = false;

	}

	void Update () {

		buttons ();
	/*	if (money < 20) {
			srg1.interactable = false;
		} 
		else if (money >= 20) {
			srg1.interactable = true;
		} 
		else if (money >= 40 && SRG1 == true) {
			srg2.interactable = true;
		} 
		else if (money >= 40 && SRG1 == false) {
			srg2.interactable = false;
		}*/
	}
		/*else if (name == "SRG3") {
			if (money >= 60  && SRG2==true) {

				buttonUnlocked ();
			} else
				buttonLocked ();
		}

		else if (name == "SRG4") {
			if (money >= 80  && SRG3==true) {

				buttonUnlocked ();
			} else
				buttonLocked ();
		}

		else if (name == "SRG5") {
			if (money >= 100  && SRG4==true) {

				buttonUnlocked ();
			} else
				buttonLocked ();
		}*/

	


	public void cobrar(int value){

		if (value == 1) {
			money -= 20;
			SRG1 = true;
		}
		if (value == 2 && SRG1) {
			money -= 40;
			SRG2 = true;
		}


	}

	void buttons(){

		if (money >= 20) {
			srg1btn.interactable = true;
		} else if (money>=40) {
			srg2btn.interactable = true;

		}
	}





}

